using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using OnMonitor.Monitor;
using OnMonitor.MonitorRepair;
using SimilarImages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utility.Common.ImageHelper;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Job
{
    public class DVRInfoCheckService :ApplicationService
  
    {
        IRepository<DVRCheckInfo, int> _dVRCheckInforepository;
        IRepository<DVR, int> _dVRrepository;
        IRepository<DVRChannelInfo, int> _dVRChannelInforepository;
        IRepository<Camera, int> _camerarepository;
        IRepository<CameraRepair, int> _cameraRepairrepository;
        static public HttpClient _httpClient;



        public DVRInfoCheckService(IRepository<DVRCheckInfo, int> DVRCheckInforepository, IRepository<DVR, int> DVRrepository, IRepository<DVRChannelInfo, int> dVRChannelInforepository, IRepository<Camera, int> camerarepository, IRepository<CameraRepair, int> cameraRepairrepository)
        {
            _dVRCheckInforepository = DVRCheckInforepository;
            _dVRrepository = DVRrepository;
            _dVRChannelInforepository = dVRChannelInforepository;
            _camerarepository = camerarepository;
            _cameraRepairrepository = cameraRepairrepository;
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }
        /// <summary>
        /// 定时任务，自动对比主机数据，每天2:00启动一次
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
                dVRCheckInfo.DVR_ID = item.DVR_ID;
                dVRCheckInfo.LastModificationTime = DateTime.Now;
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
                if (!string.IsNullOrEmpty( data.DVR_SN))
                {
                    dVRCheckInfo.DVR_SN = data.DVR_SN;
                  
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
             
                //时间检查验证
                var servertime = DateTime.Now;
                DateTime dvrtime = Convert.ToDateTime(data.DVR_DateTine);
                dVRCheckInfo.DVRTime = data.DVR_DateTine;
                if (servertime.Second + 5 >= dvrtime.Second && dvrtime.Second >= servertime.Second - 5)
                {
                   
                    dVRCheckInfo.TimeInfoChenk = true;
                }
                else
                {
                    dVRCheckInfo.TimeInfoChenk = false;
                   
                }
                //90天存储检查

                String startTime = DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd hh:mm:ss");
                String endTime = DateTime.Now.AddDays(-90).AddHours(1).ToString("yyyy-MM-dd hh:mm:ss"); ;
                string url2 = $"{dvrurl}/api/DVRInfo/QueryVideoFileByTime?IP={item.DVR_IP}&name={item.DVR_usre}&password={item.DVR_possword}&startTimestr={startTime}&endTimestr={endTime}";
                var handler2 = new HttpClientHandler();
                var response2 = _httpClient.GetAsync(url2).Result;
                var dt2 = response2.Content.ReadAsStringAsync().Result;
                var data2 = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(dt2);

                if (data2==-1)
                {
                    response2 = _httpClient.GetAsync(url2).Result;
                    dt2 = response2.Content.ReadAsStringAsync().Result;
                   data2 = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(dt2);
                }
                
                
                if (data2>0)
                {
                    dVRCheckInfo.VideoCheck90Day = true;
                }
                if (data2==0)
                {
                    dVRCheckInfo.VideoCheck90Day = false;
                }

                int nuber = _dVRCheckInforepository.Where(u => u.DVR_ID == item.DVR_ID).Count();
                if (nuber == 0)
                {
                    var DD = await _dVRCheckInforepository.InsertAsync(dVRCheckInfo,true);
                }
                else
                {
                    var id = _dVRCheckInforepository.Where(u => u.DVR_ID == item.DVR_ID).FirstOrDefault().Id;
                    await _dVRCheckInforepository.DeleteAsync(id);
                   var DD= await _dVRCheckInforepository.InsertAsync(dVRCheckInfo,true);
                }
              
                Console.WriteLine($"{item.DVR_ID}+{DateTime.Now}+写入成功");
            }

            return listdVRCheckInfo;

            }


        /// <summary>
        /// 定时任务，对比镜头数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<DVRChannelInfo>> GetDVRChannelInfo(string DVRRoom)
        {
            var configuration = BuildConfiguration();
            var dvrurl = configuration.GetSection("DVRInfourl:url").Value;
            var dvrdata = _dVRrepository.Where(u=>u.Monitoring_room==DVRRoom).ToList();
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
                    try
                    {
                        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<DVRInfoDto>(dt);
                        //检查通道信息
                        var cameraData = _camerarepository.Where(u => u.DVR_ID == item.DVR_ID);
                        if (data!=null )
                        {
                            //检查通道信息存储到数据库
                            foreach (var tem in data.Channelname)
                            {
                                DVRChannelInfo dVRChannelInfo = new DVRChannelInfo();
                                var channldata = cameraData.Where(u => u.channel_ID == tem.Number).FirstOrDefault();
                                dVRChannelInfo.DVRChannelName = tem.Name;
                                dVRChannelInfo.channel_ID = tem.Number;


                                if (channldata != null)
                                {
                                    dVRChannelInfo.DVR_ID = channldata.DVR_ID;
                                    dVRChannelInfo.Camera_ID = channldata.Camera_ID;
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



                                // 获取设备截图并比对结果
                                string url2 = $"{dvrurl}/api/DVRClannel/GetChannelPicture?DVR_IP={item.DVR_IP}&DVR_Name={item.DVR_usre}&DVR_PassWord={item.DVR_possword}&ChannelID={tem.Number}";
                                try
                                {

                                    ImageHelp2 imageHelp = new ImageHelp2();
                                    var handler2 = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
                                    var response2 = await _httpClient.GetStreamAsync(url2);
                                    Image image = Image.FromStream(response2);
                                    Image image2 = Image.FromFile(Path.Combine(AppContext.BaseDirectory, "yichang.jpg"));
                                    Bitmap bitmap1 = imageHelp.Resize(image);
                                    Bitmap bitmap2 = imageHelp.Resize(image2);
                                    var reqst = ImageHash.GetSimilarity(bitmap1, bitmap2, ImageHash.HashEnum.Difference);
                                    if (reqst > 0.9)
                                    {
                                        dVRChannelInfo.ImageCheck = false;
                                    }
                                    else
                                    {
                                        dVRChannelInfo.ImageCheck = true;
                                    }




                                }
                                catch (Exception)
                                {

                                    dVRChannelInfo.ImageCheck = null;
                                }



                                var requst = await _dVRChannelInforepository.FindAsync(u => u.DVRChannelName == tem.Name);
                                dVRChannelInfo.LastUpdateTime = DateTime.Now.ToString();
                                if (requst == null)
                                {
                                    var EE = await _dVRChannelInforepository.InsertAsync(dVRChannelInfo, true);
                                    listdVRChannelInfo.Add(EE);
                                }
                                else
                                {

                                    var F = _dVRChannelInforepository.DeleteAsync(requst);
                                    var DD = await _dVRChannelInforepository.InsertAsync(dVRChannelInfo, true);
                                    listdVRChannelInfo.Add(DD);
                                }

                            }
                        }
                    

                    }
                    catch (Exception)
                    {

                       
                    }
                  

                }
                else
                {
                    return null;
                }

            }





            return listdVRChannelInfo;


        }
        /// <summary>
        /// 定时任务，自动比对异常表信号确认
        /// </summary>
        /// <returns></returns>
        public async Task<List<CameraRepairDto>> GetCameraRepairImageCheck()
        {
            var configuration = BuildConfiguration();
            var dvrurl = configuration.GetSection("DVRInfourl:url").Value;
            var cameradata =await _camerarepository.GetListAsync();
            var dvrdata =await _dVRrepository.GetListAsync();
            var Repairdata = await _cameraRepairrepository.GetListAsync();
            var cameraRepairdata= Repairdata.Where(u => u.RepairState == false);
           
            foreach (var item in cameraRepairdata)
            {
                var camera = cameradata.Where(u => u.Camera_ID == item.Camera_ID).FirstOrDefault();
                if (camera!=null)
                {
                    var dvr = dvrdata.Where(u => u.DVR_ID == camera.DVR_ID).FirstOrDefault();
                    if (dvr != null)
                    {
                        // 获取设备截图并比对结果
                        string url2 = $"{dvrurl}/api/DVRClannel/GetChannelPicture?DVR_IP={dvr.DVR_IP}&DVR_Name={dvr.DVR_usre}&DVR_PassWord={dvr.DVR_possword}&ChannelID={camera.channel_ID}";
                        try
                        {

                            ImageHelp2 imageHelp = new ImageHelp2();
                            var handler2 = new HttpClientHandler();
                            var response2 = await _httpClient.GetStreamAsync(url2);
                            Image image = Image.FromStream(response2);
                            Image image2 = Image.FromFile(Path.Combine(AppContext.BaseDirectory, "yichang.jpg"));
                            Bitmap bitmap1 = imageHelp.Resize(image);
                            Bitmap bitmap2 = imageHelp.Resize(image2);
                            var reqst = ImageHash.GetSimilarity(bitmap1, bitmap2, ImageHash.HashEnum.Difference);
                            if (reqst > 0.9)
                            {
                                item.NoSignal = false;
                            }
                            else
                            {
                                item.NoSignal = true;
                            }




                        }
                        catch (Exception)
                        {

                            item.NoSignal = null;
                        }
                    }
                }
              
             

              // var deletedata=  _cameraRepairrepository.DeleteAsync(item);
                var repairrequst = await _cameraRepairrepository.UpdateAsync(item, true);

            }




            return null;

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
