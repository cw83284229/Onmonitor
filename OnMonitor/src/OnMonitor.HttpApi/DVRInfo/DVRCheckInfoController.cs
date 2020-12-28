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

namespace OnMonitor.Controllers
{


  //   [Authorize(Roles = "admin")]
    [Route("api/DVRCheckInfo")]
    public class DVRCheckInfoController : OnMonitorController
    {
        public ICameraAppService _cameraAppService;
        public IDVRAppService _dVRAppService;
        static public HttpClient _httpClient;
        public IConfiguration _configuration;
        public DVRCheckInfoController(ICameraAppService cameraAppService, IDVRAppService dVRAppService, IConfiguration configuration)
        {
            _cameraAppService = cameraAppService;
            _dVRAppService = dVRAppService;
            _configuration = configuration;

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
        [Route("GetDVRCheckInfoByDVR_ID")]
        public DVRcheckinfoModel GetDVRCheckInfoByDVR_ID(string DVR_ID)
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

                DVRcheckinfoModel dVRCheckInfo = new DVRcheckinfoModel();
                //时间检查验证
                var servertime = DateTime.Now;
                dVRCheckInfo.SystemTime = servertime.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dvrtime = Convert.ToDateTime(data.DVR_DateTine);
                dVRCheckInfo.DVRTime = data.DVR_DateTine;
                if (DateTime.Compare(servertime.AddSeconds(-10),dvrtime)<0&& DateTime.Compare(servertime.AddSeconds(10), dvrtime)>0)
                {
                    dVRCheckInfo.TimeInfoChenk = true;
                }
                else
                {
                    dVRCheckInfo.TimeInfoChenk = false;
                }
                //硬盘检查装配
                int dvrhard = (int)(dvrdata.Hard_drive * 0.91);
                List<DVRDisk> listdvrdisk = new List<DVRDisk>();
               
                 //装配硬盘
                foreach (var item in data.DVRDisk)
                {
                    DVRDisk dVRDisk = new DVRDisk();
                    dVRDisk.Number = item.Number;
                    dVRDisk.Disk = item.Disk/1000;//四舍五入取值法
                     listdvrdisk.Add(dVRDisk);
                }

             
                dVRCheckInfo.DVRDISK = listdvrdisk;
                dVRCheckInfo.DataDiskTotal = dvrhard;

                if (dvrhard == data.HardDrive)
                {
                    dVRCheckInfo.DiskTotal = data.HardDrive;
                    dVRCheckInfo.DiskChenk = true;
                }
                else
                {
                    dVRCheckInfo.DiskTotal = data.HardDrive;
                    dVRCheckInfo.DiskChenk = false;
                }
                //在线及sn检查
                dVRCheckInfo.DataDVR_SN = dvrdata.DVR_SN;
                dVRCheckInfo.DVR_ID = dvrdata.DVR_ID;
              
                if (data.DVR_SN != null)
                {
                    dVRCheckInfo.DVR_SN = data.DVR_SN;
                    dVRCheckInfo.DVR_ChannelTotal = data.ChannelTotal;
                    dVRCheckInfo.DVR_Online = true;
                    if (dvrdata.DVR_SN == data.DVR_SN)
                    {
                        dVRCheckInfo.SNChenk = true;
                    }
                    else
                    {
                        dVRCheckInfo.SNChenk = false;
                    }
                }
                else
                {
                    dVRCheckInfo.DVR_Online = false;
                }

               
                //检查通道信息
                var cameraData = _cameraAppService.GetListByDVRID(dvrdata.DVR_ID).Result;
                dVRCheckInfo.DataChannelTotal = cameraData.Count();
                List<DVRChannelInfoModel> listdVRChannelInfo = new List<DVRChannelInfoModel>();
                foreach (var tem in data.Channelname)
                {
                    DVRChannelInfoModel dVRChannelInfo = new DVRChannelInfoModel();
                    var channldata = cameraData.Where(u => u.channel_ID == tem.Number).FirstOrDefault();
                    dVRChannelInfo.DVRChannelName = tem.Name;
                    dVRChannelInfo.Channelid = tem.Number;
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
                        dVRChannelInfo.ChannelNameCheck = true;
                    }

                    listdVRChannelInfo.Add(dVRChannelInfo);

                }


                dVRCheckInfo.DVRChannelInfo = listdVRChannelInfo;

                return dVRCheckInfo;
            }
            else
            {
                return null;
            }


        }
    }
}
   

