using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OnMonitor.Job;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.IO;
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
        IRepository<DVRChannelInfo, int> _dVRChannelInforepository;
        IRepository<Camera, int> _camerarepository;
        static public HttpClient _httpClient;
        public DVRInfoCheckJob(IRepository<DVRCheckInfo, int> DVRCheckInforepository, IRepository<DVR, int> DVRrepository, IRepository<DVRChannelInfo, int> dVRChannelInforepository, IRepository<Camera, int> camerarepository)
        {
            _dVRCheckInforepository = DVRCheckInforepository;
            _dVRrepository = DVRrepository;
            _dVRChannelInforepository = dVRChannelInforepository;
            _camerarepository = _camerarepository;
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
                while (!stoppingToken.IsCancellationRequested)
                {
                    var msg = $"{DateTime.Now},testok";

                // var data = await GetDVRInfoCheck();
                 var reqst = await GetDVRChannelInfo();

                    await Task.Delay(86400000, stoppingToken);

                }       
        }


        /// <summary>
        /// 定时任务，自动对比主机数据，每天2:00启动一次
        /// </summary>

        public async Task<List<DVRCheckInfoDto>> GetDVRInfoCheck()
        {
            var configuration = BuildConfiguration();
            
            var dvrurl = configuration.GetSection("DVRInfourl:url").Value;
            var dvrdata = await _dVRrepository.GetListAsync(); 

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
                    dVRCheckInfo.DiskTotal = 0;
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
                if (servertime.Second + 5 >= dvrtime.Second && dvrtime.Second >= servertime.Second - 5)
                {
                    dVRCheckInfo.DVRTime = data.DVR_DateTine;
                    dVRCheckInfo.TimeInfoChenk = true;
                }
                else
                {
                    dVRCheckInfo.TimeInfoChenk = false;
                    dVRCheckInfo.DVRTime = data.DVR_DateTine;
                }

                  var dvrcheckdata = _dVRCheckInforepository.GetListAsync().Result;
                 int nuber=dvrcheckdata.Where(u => u.DVR_ID == item.DVR_ID).Count();
                if (nuber == 0)
                {
                    var DD = await _dVRCheckInforepository.InsertAsync(dVRCheckInfo);
                }
                else
                {
                    var id = dvrcheckdata.Where(u => u.DVR_ID == item.DVR_ID).FirstOrDefault().Id;
                    await _dVRCheckInforepository.DeleteAsync(id);
                    var DD = await _dVRCheckInforepository.InsertAsync(dVRCheckInfo);
                }

                Console.WriteLine($"{item.DVR_ID}+{DateTime.Now}+写入成功");
            }

            return listdVRCheckInfo;

        }
        /// <summary>
        /// 定时任务，对比镜头数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<DVRChannelInfo>> GetDVRChannelInfo()
        {
            var configuration = BuildConfiguration();
            var dvrurl = configuration.GetSection("DVRInfourl:url").Value;
            var dvrdata =  _dVRrepository.ToList();
            List<DVRCheckInfoDto> listdVRCheckInfo = new List<DVRCheckInfoDto>();
            List<DVRChannelInfo> listdVRChannelInfo = new List<DVRChannelInfo>();
            foreach (var item in dvrdata)
            {

                if (dvrdata != null)
                {

                    string url = $"{dvrurl}/api/DVRInfo/Get?IP={item.DVR_IP}&name={item.DVR_usre}&password={item.DVR_possword}";
                    var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
                    var response = _httpClient.GetAsync(url).Result;
                    var dt = response.Content.ReadAsStringAsync().Result;
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<DVRInfoDto>(dt);
                    //检查通道信息
                    var cameraData = _camerarepository.Where(u=>u.DVR_ID==item.DVR_ID);
                  
                    //检查通道信息存储到数据库
                    foreach (var tem in data.Channelname)
                    {
                        DVRChannelInfo dVRChannelInfo = new DVRChannelInfo();
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

                        var requst =await _dVRChannelInforepository.FindAsync(u => u.Camera_ID == channldata.Camera_ID);

                        if (requst==null)
                        {
                            var EE = await _dVRChannelInforepository.InsertAsync(dVRChannelInfo);
                            listdVRChannelInfo.Add(EE);
                        }
                        else
                        {
                            var DD = await _dVRChannelInforepository.UpdateAsync(dVRChannelInfo,true);
                            listdVRChannelInfo.Add(DD);
                        }




                    

                    }

                  
                }
                else
                {
                    return null;
                }
                
            }





            return listdVRChannelInfo;


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
