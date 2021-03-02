using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using OnMonitor.Monitor;
using OnMonitor.Monitor.Alarm;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Controllers
{

   // [Authorize(Roles = "admin")]
    [Route("api/Camera")]
    public class DVRInfoController : OnMonitorController
    {

        public ICameraAppService _cameraAppService;
        public IDVRAppService _dVRAppService;
        public IAlarmAppService _alarmAppService;
        static public HttpClient _httpClient;
        public IConfiguration _configuration;
        string dvrurl;
        public DVRInfoController(ICameraAppService cameraAppService, IDVRAppService dVRAppService, IConfiguration configuration, IAlarmAppService alarmAppService)
        {
            _cameraAppService = cameraAppService;
            _dVRAppService = dVRAppService;
            _configuration = configuration;
            _alarmAppService = alarmAppService;
            if (_httpClient==null)
            {
                _httpClient = new HttpClient();
            }
            dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
        }

        #region DVR通道操作
        /// <summary>
        /// 获取通道截图/传入镜头编号
        /// </summary>
        /// <param name="Camera_ID"></param>
        /// <returns></returns>
        [Authorize(Roles ="videoCheck")]
        [HttpGet]
        [Route("GetChannelPicture")]
        public IActionResult GetChannelPicture(string Camera_ID)
        {


            var cameradata = _cameraAppService.GetListByCameraID(Camera_ID).Result.ToList().FirstOrDefault();


            if (cameradata == null)
            {
                return Json("无此镜头");
            }

            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, cameradata.DVR_ID).Result.Items.FirstOrDefault();
           

            string url = $"{dvrurl}/api/DVRClannel/GetChannelPictureLocal?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.GetAsync(url).Result;
            var dt = response.Content.ReadAsByteArrayAsync().Result;
            var type = response.Content.Headers.ContentType.ToString();

            return File(dt, type);

        }

        /// <summary>
        /// 获取通道截图/传入镜头编号
        /// </summary>
        /// <param name="Alarm_ID"></param>
        /// <returns></returns>
        [Authorize(Roles = "videoCheck")]
        [HttpGet]
        [Route("GetChannelPictureByAlarmID")]
        public IActionResult GetChannelPictureAlarmID(string Alarm_ID)
        {


            var Camera_ID = _alarmAppService.GetAlarmDto(Alarm_ID).Camera_ID;
            var cameradata = _cameraAppService.GetListByCameraID(Camera_ID).Result.ToList().FirstOrDefault();

            if (cameradata == null)
            {
                return Json("无此镜头");
            }

            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, cameradata.DVR_ID).Result.Items.FirstOrDefault();


            string url = $"{dvrurl}/api/DVRClannel/GetChannelPictureLocal?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.GetAsync(url).Result;
            var dt = response.Content.ReadAsByteArrayAsync().Result;
            var type = response.Content.Headers.ContentType.ToString();

            return File(dt, type);

        }

        /// <summary>
        /// 按文件名称下载录像
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [Authorize(Roles = "videoCheck")]
        [HttpGet]
        [Route("DownloadVideoByFileName")]
        public IActionResult DownloadVideoByFileName(string fileName)
        {
            string url = $"{dvrurl}/api/DVRClannel/DownloadVideoFile?fileName={fileName}";

            var response = _httpClient.GetAsync(url).Result;
            var dt = response.Content.ReadAsByteArrayAsync().Result;
            var type = response.Content.Headers.ContentType.ToString();

            return File(dt, type, fileName);

        }
        /// <summary>
        /// 获取全部备份录像文件
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "videoCheck")]
        [HttpGet]
        [Route("GetVideoFileName")]
        public string GetVideoFileName()
        {
            string url = $"{dvrurl}/api/DVRClannel/GetVideoFiles";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }

  

        /// <summary>
        /// 按时间/镜头编号备份视频文件
        /// </summary>
        /// <returns></returns>
      //  [Authorize(Roles = "videoCheck")]
        [HttpGet]
        [Route("BackupsVideoByTime")]
        public string BackupsVideoByTime(string Camera_ID, string startTime, string endTime,string username,string password)
        {
            var cameradata = _cameraAppService.GetListByCameraID(Camera_ID).Result.ToList().FirstOrDefault();

            if (cameradata == null)
            {
                return "无此镜头";
            }
            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, cameradata.DVR_ID).Result.Items.FirstOrDefault();

            string url = $"{dvrurl}/api/DVRClannel/GetVideoData?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}&startTime={startTime}&endTime={endTime}";

            if (!string.IsNullOrEmpty(username)||!string.IsNullOrEmpty(password))
            {
                  url = $"{dvrurl}/api/DVRClannel/GetVideoData?DVR_IP={dvrdata.DVR_IP}&DVR_Name={username}&DVR_PassWord={password}&ChannelID={cameradata.channel_ID}&startTime={startTime}&endTime={endTime}";
            }
            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }


        /// <summary>
        /// 下载进度状态查询
        /// </summary>
        /// <param name="DownloadID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DownloadVideoFilePlan")]
        public string DownloadVideoFilePlan(string DownloadID)
        {

          //  var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;

            string url = $"{dvrurl}/api/DVRClannel/DownloadVideoFilePlan?DownloadID={DownloadID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }
        /// <summary>
        /// 停止失败备份
        /// </summary>
        /// <param name="DownloadID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("StopDownloadVideo")]
        public string StopDownloadVideo(string DownloadID)
        {

           

            string url = $"{dvrurl}/api/DVRClannel/StopDownloadVideo?DownloadID={DownloadID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }

        /// <summary>
        /// 按名称删除录像文件
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "videoCheck")]
        [HttpPost]
        [Route("DeleteVideoFile")]
        public string DeleteVideoFile(string fileName)
        {

        

            string url = $"{dvrurl}/api/DVRClannel/DeleteVideoFile?fileName={fileName}";

            var handler = new HttpClientHandler();
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "fileName", fileName } };
            var content = new FormUrlEncodedContent(dic);
            var response = _httpClient.PostAsync(url,content).Result;
            return response.Content.ReadAsStringAsync().Result;

        }

        /// <summary>
        /// 获取通道名称
        /// </summary>
        /// <param name="CameraID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChannelName")]
        public string GetChannelName(string CameraID)
        {

            var cameradata = _cameraAppService.GetListByCameraID(CameraID).Result.ToList().FirstOrDefault();
            if (cameradata == null)
            {
                return "无此镜头";
            }
            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, cameradata.DVR_ID).Result.Items.FirstOrDefault();
              var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
          

            string url = $"{dvrurl}/api/DVRClannel/Get?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

           
        }
        /// <summary>
        /// 自动设定通道名称与数据库同步
        /// </summary>
        /// <param name="CameraID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetChannelName")]
        public string SetChannelName(string CameraID)
        {

            var cameradata = _cameraAppService.GetListByCameraID(CameraID).Result.ToList().FirstOrDefault();
            if (cameradata == null)
            {
                return "无此镜头";
            }
            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, cameradata.DVR_ID).Result.Items.FirstOrDefault();
            if (cameradata.Direction != null)
            {   //繁体转简体
                cameradata.Direction = ChineseConverter.Convert(cameradata.Direction, ChineseConversionDirection.TraditionalToSimplified);
            }

            if (cameradata.Location != null)
            {
                cameradata.Location = ChineseConverter.Convert(cameradata.Location, ChineseConversionDirection.TraditionalToSimplified);
            }


            string chammelname = $"{cameradata.Camera_ID} {cameradata.Build}-{cameradata.floor} {cameradata.Direction}{cameradata.Location}";
            var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
            string url = $"{dvrurl}/api/DVRClannel/Post?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}&ChannelName={chammelname}";
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"DVR_IP",dvrdata.DVR_IP },
                {"DVR_Name",dvrdata.DVR_usre},
                {"DVR_PassWord",dvrdata.DVR_possword},
                {"ChannelID",dvrdata.DVR_Channel.ToString() },
                {"ChannelName",chammelname }
            };

            var content = new FormUrlEncodedContent(dic);
            var response = _httpClient.PostAsync(url, content).Result;

            return response.Content.ReadAsStringAsync().Result;
        }


        /// <summary>
        /// 获取镜头编码信息
        /// </summary>
        /// <returns></returns>
        // [Authorize(Roles = "videoCheck")]
        [HttpGet]
        [Route("GetChannelInfo")]
        public string GetChannelInfo(string Camera_ID)
        {
            var cameradata = _cameraAppService.GetListByCameraID(Camera_ID).Result.ToList().FirstOrDefault();

            if (cameradata == null)
            {
                return "无此镜头";
            }

            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, cameradata.DVR_ID).Result.Items.FirstOrDefault();



            // string dvrurl = _configuration.GetSection("DVRInfourl:url").Value;

            string url = $"{dvrurl}/api/DVRClannel/GetChannelInfo?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }

        #endregion

        #region 主机信息操作
        /// <summary>
        /// 获取主机信息
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDVRInfo")]
        public IActionResult GetDVRInfo(string DVR_ID)
        {


            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, DVR_ID).Result.Items.FirstOrDefault();

          
            string url = $"{dvrurl}/api/DVRInfo/Get?IP={dvrdata.DVR_IP}&name={dvrdata.DVR_usre}&password={dvrdata.DVR_possword}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.GetAsync(url).Result;
            var dt = response.Content.ReadAsStringAsync().Result;
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(dt);
            var type = response.Content.Headers.ContentType.ToString();
            return Json(data);

        }
        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDVRTime")]
        public IActionResult GetDVRTime(string DVR_ID)
        {


            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, DVR_ID).Result.Items.FirstOrDefault();

           
            string url = $"{dvrurl}/api/DVRInfo/GetTime?IP={dvrdata.DVR_IP}&name={dvrdata.DVR_usre}&password={dvrdata.DVR_possword}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.GetAsync(url).Result;
            var dt = response.Content.ReadAsStringAsync().Result;
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(dt);
            return Json(data);

        }
        /// <summary>
        /// 同步时间
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostDVRTime")]
        public IActionResult PostDVRTime(string DVR_ID)
        {


            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, DVR_ID).Result.Items.FirstOrDefault();

           
            string url = $"{dvrurl}/api/DVRInfo/SetDVRTime?IP={dvrdata.DVR_IP}&name={dvrdata.DVR_usre}&password={dvrdata.DVR_possword}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.PostAsync(url, null).Result;
            var dt = response.Content.ReadAsStringAsync().Result;
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(dt);
            return Json(data);

        }
        #endregion


        #region 测试文件
        ///// <summary>
        ///// 大文件下载，传入文件名称分段下载
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        [HttpGet]
        [Route("DownloadBigFileName")]
        public async Task<IActionResult> DownloadBigFileName(string fileName)
        {
            string url = $"{dvrurl}/api/DVRClannel/DownloadVideoFile?fileName={fileName}";

            var response = _httpClient.GetAsync(url).Result;
            var dt = response.Content.ReadAsByteArrayAsync().Result;
            var type = response.Content.Headers.ContentType.ToString();

            int bufferSize = 1024;//这就是ASP.NET Core循环读取下载文件的缓存大小，这里我们设置为了1024字节，也就是说ASP.NET Core每次会从下载文件中读取1024字节的内容到服务器内存中，然后发送到客户端浏览器，这样避免了一次将整个下载文件都加载到服务器内存中，导致服务器崩溃

            Response.ContentType = type;//由于我们下载的是一个Excel文件，所以设置ContentType为application/vnd.ms-excel

            //  var contentDisposition = "attachment;" + "filename=" + fileName;//在Response的Header中设置下载文件的文件名，这样客户端浏览器才能正确显示下载的文件名，注意这里要用HttpUtility.UrlEncode编码文件名，否则有些浏览器可能会显示乱码文件名
            //  Response.Headers.Add("Content-Disposition", new string[] { contentDisposition });

            Stream fs = null;
            using ( fs = new MemoryStream(dt))
            {
                using (Response.Body)//调用Response.Body.Dispose()并不会关闭客户端浏览器到ASP.NET Core服务器的连接，之后还可以继续往Response.Body中写入数据
                {
                    long contentLength = fs.Length;//获取下载文件的大小
                    Response.ContentLength = contentLength;//在Response的Header中设置下载文件的大小，这样客户端浏览器才能正确显示下载的进度

                    byte[] buffer;
                    long hasRead = 0;//变量hasRead用于记录已经发送了多少字节的数据到客户端浏览器

                    //如果hasRead小于contentLength，说明下载文件还没读取完毕，继续循环读取下载文件的内容，并发送到客户端浏览器
                    while (hasRead < contentLength)
                    {
                        //HttpContext.RequestAborted.IsCancellationRequested可用于检测客户端浏览器和ASP.NET Core服务器之间的连接状态，如果HttpContext.RequestAborted.IsCancellationRequested返回true，说明客户端浏览器中断了连接
                        if (HttpContext.RequestAborted.IsCancellationRequested)
                        {
                            //如果客户端浏览器中断了到ASP.NET Core服务器的连接，这里应该立刻break，取消下载文件的读取和发送，避免服务器耗费资源
                            break;
                        }

                        buffer = new byte[bufferSize];

                        int currentRead = fs.Read(buffer, 0, bufferSize);//从下载文件中读取bufferSize(1024字节)大小的内容到服务器内存中

                        await Response.Body.WriteAsync(buffer, 0, currentRead);//发送读取的内容数据到客户端浏览器
                        await Response.Body.FlushAsync();//注意每次Write后，要及时调用Flush方法，及时释放服务器内存空间

                        hasRead += currentRead;//更新已经发送到客户端浏览器的字节数
                    }
                }
            }

            return File(fs,type);
        }
        #endregion




    }
}




