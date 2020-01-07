using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using OnMonitor.Monitor;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace OnMonitor.Controllers
{

    [Authorize(Roles = "admin")]
    [Route("api/Camera")]
    public class DVRInfoController : OnMonitorController
    {

        public ICameraAppService _cameraAppService;
        public IDVRAppService _dVRAppService;
        static public HttpClient _httpClient;
        public IConfiguration _configuration;
        public DVRInfoController(ICameraAppService cameraAppService, IDVRAppService dVRAppService, IConfiguration configuration)
        {
            _cameraAppService = cameraAppService;
            _dVRAppService = dVRAppService;
            _configuration = configuration;
          
            if (_httpClient==null)
            {
                _httpClient = new HttpClient();
            }
        }

        #region DVR通道操作
        /// <summary>
        /// 获取通道截图/传入镜头编号
        /// </summary>
        /// <param name="Camera_ID"></param>
        /// <returns></returns>
        [Authorize("CCTV_VideoViewing")]
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
            var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;

            string url = $"{dvrurl}/api/DVRClannel/GetChannelPicture?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.GetAsync(url).Result;
            var dt = response.Content.ReadAsByteArrayAsync().Result;
            var type = response.Content.Headers.ContentType.ToString();
            return File(dt, type);

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
            string url = $"{dvrurl}/api/DVRClannel/Post?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID - 1}&ChannelName={chammelname}";
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

            var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
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

            var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
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

            var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
            string url = $"{dvrurl}/api/DVRInfo/SetDVRTime?IP={dvrdata.DVR_IP}&name={dvrdata.DVR_usre}&password={dvrdata.DVR_possword}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.PostAsync(url, null).Result;
            var dt = response.Content.ReadAsStringAsync().Result;
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(dt);
            return Json(data);

        } 
        #endregion







    }
}

