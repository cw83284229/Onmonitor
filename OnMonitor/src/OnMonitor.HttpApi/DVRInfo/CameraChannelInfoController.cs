using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using System.Linq;
using OnMonitor.Models;
using Volo.Abp.MultiTenancy;
using System.Drawing;
using System.IO;

namespace OnMonitor.Controllers
{


    // [Authorize(Roles = "admin")]
    [Route("api/CameraChannelInfo")]
    public class CameraChannelInfoController : OnMonitorController
    {
        public ICameraAppService _cameraAppService;
        public IDVRAppService _dVRAppService;
        static public HttpClient _httpClient;
        public IConfiguration _configuration;
        public IDVRChannelInfoAppService _dVRChannelInfoAppService;
        public CameraChannelInfoController(ICameraAppService cameraAppService, IDVRAppService dVRAppService, IDVRChannelInfoAppService dVRChannelInfoAppService, IConfiguration configuration)
        {
            _cameraAppService = cameraAppService;
            _dVRAppService = dVRAppService;
            _configuration = configuration;
            _dVRChannelInfoAppService = dVRChannelInfoAppService;

            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }


        /// <summary>
        /// 依据主机号获取比对数据
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDVRChannelInfoByDVR_ID")]
        public async Task<List<UpdateDVRChannelInfoDto>> GetDVRCheckInfoByDVR_IDAsync(string DVR_ID)
        {

            var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
            var dvrdata = _dVRAppService.GetListByCondition(null, null, null, DVR_ID).Result.Items.FirstOrDefault();
            List<DVRCheckInfoDto> listdVRCheckInfo = new List<DVRCheckInfoDto>();
            if (dvrdata != null)
            {

                string url = $"{dvrurl}/api/DVRInfo/Get?IP={dvrdata.DVR_IP}&name={dvrdata.DVR_usre}&password={dvrdata.DVR_possword}";
                var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
                var response = _httpClient.GetAsync(url).Result;
                var dt = response.Content.ReadAsStringAsync().Result;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<DVRInfoDto>(dt);
                //检查通道信息
                var cameraData = _cameraAppService.GetListByDVRID(dvrdata.DVR_ID).Result;
                List<UpdateDVRChannelInfoDto> listdVRChannelInfo = new List<UpdateDVRChannelInfoDto>();
                //检查通道信息
                foreach (var tem in data.Channelname)
                {
                    UpdateDVRChannelInfoDto dVRChannelInfo = new UpdateDVRChannelInfoDto();
                    var channldata = cameraData.Where(u => u.channel_ID == tem.Number).FirstOrDefault();
                    dVRChannelInfo.DVRChannelName = tem.Name;
                    dVRChannelInfo.channel_ID = tem.Number;
                    dVRChannelInfo.Camera_ID = channldata.Camera_ID;
                    dVRChannelInfo.DVR_ID = channldata.DVR_ID;
                    if (channldata != null)
                    {
                        string dataName = $"{channldata.Camera_ID} {channldata.Build}-{channldata.floor} {channldata.Direction}{channldata.Location}";
                        dVRChannelInfo.DataChannelName = dataName;
                        string DVRname = tem.Name.Replace(" ", "");
                        if (dataName.Replace(" ", "") == DVRname)
                        {
                            dVRChannelInfo.ChannelNameCheck = true;
                        }
                        else
                        {
                            dVRChannelInfo.ChannelNameCheck = false;
                        }
                    }
                    else
                    {
                        dVRChannelInfo.DataChannelName = "无";
                    }



                    //获取设备截图并比对结果
                    //string url2 = $"{dvrurl}/api/DVRClannel/GetChannelPicture?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={tem.Number}";

                    //var handler2 = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
                    //var response2 =await _httpClient.GetStreamAsync(url2);
                 
                    //Image image = Image.FromStream(response2);
                    //image.Save("JFDKJ.JPG");
                    
                   

                  
                   

                  








                    listdVRChannelInfo.Add(dVRChannelInfo);

                }


             

                return listdVRChannelInfo;
            }
            else
            {
                return null;
            }


        }
    }
}
   

