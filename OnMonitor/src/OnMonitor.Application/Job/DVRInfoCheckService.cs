using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Job
{
    public class DVRInfoCheckService :ApplicationService
  
    {

       IRepository<DVRCheckInfo, int> _dVRCheckInforepository;
        IRepository<DVR, int> _dVRrepository;
        static public HttpClient _httpClient;
      


        public DVRInfoCheckService(IRepository<DVRCheckInfo,int> DVRCheckInforepository,IRepository<DVR, int> DVRrepository)
        {
            _dVRCheckInforepository = DVRCheckInforepository;
            _dVRrepository = DVRrepository;
           

            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }


        /// <summary>
        /// 定时任务，自动对比数据，每天2:00启动一次
        /// </summary>

        public async Task<List<DVRCheckInfoDto>> GetDVRInfoCheck()
            {

            var configuration = BuildConfiguration();

            var dvrurl = configuration.GetSection("DVRInfourl:url").Value;
            var dvrdata =await _dVRrepository.GetListAsync(); ;
              
               List<DVRCheckInfoDto> listdVRCheckInfo = new List<DVRCheckInfoDto>();

            foreach (var item in dvrdata)
            {
                string url = $"{dvrurl}/api/DVRInfo/Get?IP={item.DVR_IP}&name={item.DVR_usre}&password={item.DVR_possword}";
                var handler = new HttpClientHandler();
                var response = _httpClient.GetAsync(url).Result;
                var dt = response.Content.ReadAsStringAsync().Result;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<DVRInfoDto>(dt);

                DVRCheckInfo dVRCheckInfo = new DVRCheckInfo();

                //硬盘检查
                int dvrhard = (int)(item.Hard_drive * 0.91 / 1000);
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
                if (item.DVR_SN != null)
                {
                    dVRCheckInfo.DVR_SN = data.DVR_SN;
                    dVRCheckInfo.DVR_ID = item.DVR_ID;
                    dVRCheckInfo.DVR_Channel = data.ChannelTotal;
                   
                    if (item.DVR_SN == data.DVR_SN)
                    {
                        dVRCheckInfo.SNChenk = true;
                        dVRCheckInfo.DVR_Online = true;
                    }
                    else
                    {
                        dVRCheckInfo.SNChenk = false;
                        dVRCheckInfo.DVR_Online = false;
                    }
                }
                else
                {
                    dVRCheckInfo.DVR_Online = false;
                }
                dVRCheckInfo.LastModificationTime = DateTime.Now;
                //时间检查验证
                var servertime = DateTime.Now;
                DateTime dvrtime = Convert.ToDateTime(data.DVR_DateTine);
                if (servertime.Second + 2 >= dvrtime.Second && dvrtime.Second >= servertime.Second - 2)
                {
                    dVRCheckInfo.DVRTime = data.DVR_DateTine;
                    dVRCheckInfo.TimeInfoChenk = true;
                }
                else
                {
                    dVRCheckInfo.TimeInfoChenk = false;
                    dVRCheckInfo.DVRTime = data.DVR_DateTine;
                }

                int nuber = _dVRCheckInforepository.Where(u => u.DVR_ID == item.DVR_ID).Count();
                if (nuber == 0)
                {
                    var DD = await _dVRCheckInforepository.InsertAsync(dVRCheckInfo);
                }
                else
                {
                    var id = _dVRCheckInforepository.Where(u => u.DVR_ID == item.DVR_ID).FirstOrDefault().Id;
                    await _dVRCheckInforepository.DeleteAsync(id);
                   var DD= await _dVRCheckInforepository.InsertAsync(dVRCheckInfo);
                }
              
                Console.WriteLine($"{item.DVR_ID}+{DateTime.Now}+写入成功");
            }

            return listdVRCheckInfo;

            }


        //配置获取Appsettings.json
        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }


    }
}
