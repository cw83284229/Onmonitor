using AutoMapper.Configuration;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Job
{
    public class DVRInfoCheckService :ApplicationService
    // CrudAppService<
    // DVRCheckInfo,//定义实体
    //DVRCheckInfoDto,//定义DTO
    // Int32, //实体的主键
    // PagedAndSortedResultRequestDto, //获取分页排序
    // UpdateDVRCheckInfoDto, //用于创建实体
    // UpdateDVRCheckInfoDto> //用于更新实体
    {

        IRepository<DVRCheckInfo, int> _dVRCheckInforepository;
        IRepository<DVR, int> _dVRrepository;
        static public HttpClient _httpClient;
        public IConfiguration _configuration;


        public DVRInfoCheckService(IRepository<DVRCheckInfo,int> DVRCheckInforepository,IRepository<DVR, int> DVRrepository, IConfiguration configuration) 
        {
            _dVRCheckInforepository=DVRCheckInforepository;
            _dVRrepository = DVRrepository;
            _configuration = configuration;

            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }


        /// <summary>
        /// 条件筛选，获取主机自动比对数据
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>

        public async Task<List<DVRCheckInfoDto>> GetDVRInfoCheck()
            {
           // var configurationSection = _configuration.GetSection("IdentityServer:Clients");
              var dvrurl = "http://172.30.116.49:8000";
               var dvrdata =await _dVRrepository.GetListAsync(); ;
              
               List<DVRCheckInfoDto> listdVRCheckInfo = new List<DVRCheckInfoDto>();

                foreach (var item in dvrdata)
                {
                    string url = $"{dvrurl}/api/DVRInfo/Get?IP={item.DVR_IP}&name={item.DVR_usre}&password={item.DVR_possword}";
                    var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
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
                    if (nuber==0)
                {
                   var DD= await _dVRCheckInforepository.InsertAsync(dVRCheckInfo);
                }
                else
                {
                    var id = _dVRCheckInforepository.Where(u => u.DVR_ID == item.DVR_ID).FirstOrDefault().Id;
                    await _dVRCheckInforepository.UpdateAsync(dVRCheckInfo);
                }
                Console.WriteLine($"DVR_ID+{DateTime.Now}+写入成功");
                }

                return listdVRCheckInfo;

            }


        }
}
