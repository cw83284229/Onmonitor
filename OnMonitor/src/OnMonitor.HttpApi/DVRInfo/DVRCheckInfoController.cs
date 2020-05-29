using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace OnMonitor.Controllers
{


    [Authorize(Roles = "admin")]
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


        ///// <summary>
        ///// 条件筛选，获取主机自动比对数据
        ///// </summary>
        ///// <param name="DVR_ID"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetDVRCheckInfoByCondition")]
        //public async Task<List<DVRCheckInfoDto>>  GetDVRCheckInfoByCondition(string Monitoring_room, string Build, string Floor, string DVR_ID)
        //{

        //    var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
        //    var dvrdata = _dVRAppService.GetListByCondition(Monitoring_room, Build,Floor, DVR_ID).Result.Items;
        //    List<DVRCheckInfoDto> listdVRCheckInfo = new List<DVRCheckInfoDto>();

        //    foreach (var item in dvrdata)
        //    {
        //        string url = $"{dvrurl}/api/DVRInfo/Get?IP={item.DVR_IP}&name={item.DVR_usre}&password={item.DVR_possword}";
        //        var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
        //        var response = _httpClient.GetAsync(url).Result;
        //        var dt = response.Content.ReadAsStringAsync().Result;
        //        var data =Newtonsoft.Json.JsonConvert.DeserializeObject<DVRInfoDto>(dt);
              
        //        DVRCheckInfoDto dVRCheckInfo = new DVRCheckInfoDto();

        //        //硬盘检查
        //        int dvrhard = (int)(item.Hard_drive * 0.91/1000);
        //        if (dvrhard==data.HardDrive)
        //        {
        //            dVRCheckInfo.DiskTotal = data.HardDrive;
        //            dVRCheckInfo.DiskChenk = true;
        //        }
        //        else
        //        {
        //            dVRCheckInfo.DiskTotal = data.HardDrive;
        //            dVRCheckInfo.DiskChenk = false;
        //        }
        //        //在线及sn检查
        //        if (item.DVR_SN!=null)
        //        {
        //            dVRCheckInfo.DVR_SN = data.DVR_SN;
        //            dVRCheckInfo.DVR_ID = item.DVR_ID;
        //            dVRCheckInfo.DVR_Channel = data.ChannelTotal;
        //            dVRCheckInfo.DVR_Online = true;
        //            if (item.DVR_SN==data.DVR_SN)
        //            {
        //                dVRCheckInfo.SNChenk = true;
        //            }
        //            else
        //            {
        //                dVRCheckInfo.SNChenk = false;
        //            }
        //        }
        //        else
        //        {
        //            dVRCheckInfo.DVR_Online = false;
        //        }
                
        //        //时间检查验证
        //        var servertime = DateTime.Now;
        //        DateTime dvrtime = Convert.ToDateTime(data.DVR_DateTine);
        //        if (servertime.Second + 2 >= dvrtime.Second && dvrtime.Second >= servertime.Second - 2)
        //        {
        //            dVRCheckInfo.DVRTime = data.DVR_DateTine;
        //            dVRCheckInfo.TimeInfoChenk = true;
        //        }
        //        else
        //        {
        //            dVRCheckInfo.TimeInfoChenk = false;
        //            dVRCheckInfo.DVRTime = data.DVR_DateTine;
        //        }
        //        listdVRCheckInfo.Add(dVRCheckInfo);

        //    }
          
        //    return listdVRCheckInfo;

        }







    }

