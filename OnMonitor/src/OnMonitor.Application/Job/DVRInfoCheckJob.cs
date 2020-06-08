using Microsoft.Extensions.Hosting;
using OnMonitor.Job;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace TimedTask.Host.Job
{
    public class DVRInfoCheckJob : BackgroundService
    {
        IRepository<DVRCheckInfo, int> _dVRCheckInforepository;
        IRepository<DVR, int> _dVRrepository;
        static public HttpClient _httpClient;
        public DVRInfoCheckJob(IRepository<DVRCheckInfo, int> DVRCheckInforepository, IRepository<DVR, int> DVRrepository)
        {
            _dVRCheckInforepository = DVRCheckInforepository;
            _dVRrepository = DVRrepository;
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var msg = $"{DateTime.Now},testok";



                   var data = await GetDVRInfoCheck();


                    await Task.Delay(800000, stoppingToken);

                }
            }
            catch (Exception)
            {

               
            }
            
          

        
        }



        /// <summary>
        /// 条件筛选，获取主机自动比对数据
        /// </summary>
        public async Task<List<DVRCheckInfoDto>> GetDVRInfoCheck()
        {

            var dvrurl = "http://172.30.116.49:8000";
            var dvrdata = await _dVRrepository.GetListAsync(); ;

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
                    dVRCheckInfo.DVR_Online = true;
                    if (item.DVR_SN == data.DVR_SN)
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
                    var DD = await _dVRCheckInforepository.InsertAsync(dVRCheckInfo);
                }

                Console.WriteLine($"{item.DVR_ID}+{DateTime.Now}+写入成功");
            }

            return listdVRCheckInfo;

        }



    }
}
