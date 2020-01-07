using Microsoft.AspNetCore.Mvc;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Service
{
   public class DVRInfoService: ApplicationService, IApplicationService
    {
        readonly IRepository<DVR, Int32> _dvrrepository;
        readonly IRepository<Camera, Int32> _camerarepository;
        public DVRInfoService(IRepository<DVR, Int32> dvrrepository, IRepository<Camera, Int32> camerarepository) 
        {

            _camerarepository = camerarepository;
            _dvrrepository = dvrrepository;
        }


        /// <summary>
        /// 异步获取通道名称ces
        /// </summary>
        /// <param name="CameraID"></param>
        /// <returns></returns>
        public string GetChannelName(string CameraID)
        {

            var cameradata = _camerarepository.GetList().Where(u => u.Camera_ID == CameraID).FirstOrDefault();

            var dvrdata = _dvrrepository.GetList().Where(u => u.DVR_ID == cameradata.DVR_ID).FirstOrDefault();


            string url = $"http://172.30.116.49:8000/api/DVRClannel/Get?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID-1}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };

            using (var http = new HttpClient(handler))
            {
                var response = http.GetAsync(url).Result;//拿到异步结果
                                                         //  Console.WriteLine(response.StatusCode); //确保HTTP成功状态值
                                                         //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                var data = response.Content.ReadAsStringAsync().Result;

                return data;
            }

        }







    }
}
