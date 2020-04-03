using Microsoft.AspNetCore.Authorization;
using OnMonitor.Monitor;
using OnMonitor.MonitorRepair;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor
{



    public class ReportFormsAppService : ApplicationService, IReportFormsAppService

    {
        IRepository<DVR> _dvrrepository;
        IRepository<Camera> _camerarepository;
        ICameraRepairAppService _cameraRepairAppService;
        IDVRCheckInfoAppService _dVRCheckInfoAppService;
        public ReportFormsAppService(IRepository<DVR> dvrrepository, IRepository<Camera> camerarepository, ICameraRepairAppService cameraRepairAppService,IDVRCheckInfoAppService dVRCheckInfoAppService)
        {
            _camerarepository = camerarepository;
            _dvrrepository = dvrrepository;
            _cameraRepairAppService = cameraRepairAppService;
            _dVRCheckInfoAppService = dVRCheckInfoAppService;
        }

        #region 获取镜头主机Total

        /// <summary>
        /// 获取主机/镜头/异常镜头按监控室分类数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReportFormsDto>> GetDVRCameraTotalAsync()

        {
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
            PagedAndSortedResultRequestDto resultRequestDto = new PagedAndSortedResultRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = null };
            QueryCondition queryCondition = new QueryCondition() { RepairState = false };
            var DVRRooms = _dvrrepository.Select(i => new { Monitoring_room = i.Monitoring_room }).Distinct();
            //获取未维修数据待修正
            var dataCameraRepair = _cameraRepairAppService.GetRepairsListByCondition(queryCondition,resultRequestDto).Result.Items;
            var data = from a in _dvrrepository
                       join b in _camerarepository on a.DVR_ID equals b.DVR_ID
                       select new DVRCameraDto
                       {
                           Factory = a.Factory,
                           Monitoring_room = a.Monitoring_room,
                           Build = b.Build,
                           floor = b.floor,
                           DVR_ID = a.DVR_ID,
                           CameraID = b.Camera_ID,
                       };
            var data1 = data.ToList();


            //获取各监控室DVR在线数量

            var dataDvrcheckInfo =  _dVRCheckInfoAppService.GetListAsync(resultRequestDto).Result.Items;
            var listDvrcheckInfo = dataDvrcheckInfo.Where(u => u.DVR_Online == true);
            List<DVR> dataDvrOnline = new List<DVR>(); 


            foreach (var item in listDvrcheckInfo)
            {
               
                dataDvrOnline .Add(_dvrrepository.Where(u => u.DVR_ID == item.DVR_ID).FirstOrDefault());

            }



           // var listDvrOnline = dataDvrOnline.ToList();



            foreach (var item in DVRRooms)
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.DVRRoom = item.Monitoring_room;
                //加载主机棕色
                var totalDVR = data1.Where(u => u.Monitoring_room == item.Monitoring_room).Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
                var totalCamera = data1.Where(u => u.Monitoring_room == item.Monitoring_room).Select(i => new { CameraID = i.CameraID }).Distinct().Count();
                 //加载维修数据
                var totalCameraRepair= dataCameraRepair.Where(u => u.DVR_Room == item.Monitoring_room).Select(i => new { CameraID = i.Camera_ID }).Distinct().Count();
                //加载在线主机数
                var totalDVROnline= dataDvrOnline.Where(u => u.Monitoring_room == item.Monitoring_room).Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
                formsDto.CameraAnomaly = totalCameraRepair;
                formsDto.DVROnLine = totalDVROnline;
                formsDto.CameraTotal = totalCamera;
                formsDto.DVRTotal = totalDVR;
                formsDto.DVRAnomaly = totalDVR - totalDVROnline;
                listreportForms.Add(formsDto);
            }
            ReportFormsDto formsDto1 = new ReportFormsDto();
            formsDto1.DVRRoom ="Total";
            formsDto1.CameraAnomaly = dataCameraRepair.Select(i => new { CameraID = i.Camera_ID }).Distinct().Count();
            formsDto1.DVROnLine = dataDvrOnline.Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count(); ;
            formsDto1.CameraTotal = data1.Select(i => new { CameraID = i.CameraID }).Distinct().Count();
            formsDto1.DVRTotal = data1.Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
            formsDto1.DVRAnomaly = formsDto1.DVRTotal - formsDto1.DVROnLine;
            listreportForms.Add(formsDto1);




            return listreportForms;
        }
        #endregion

        #region 获取在线DVR数量--未完成
        /// <summary>
        /// 获取在线DVR数量
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReportFormsDto>> GetDVROnlineTotal()
        {
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
            List<DVRDto> listdvrs = new List<DVRDto>();
            var DVRRooms = _dvrrepository.Select(i => new { Monitoring_room = i.Monitoring_room }).Distinct().ToList();
            string url = "http://172.30.116.49/api/DVRInfo?";
            foreach (var item in DVRRooms)
            {
                var data = _dvrrepository.Where(u => u.Monitoring_room == item.Monitoring_room).ToList();

                foreach (var tem in data)
                {

                    url = $"{url}IP=10.10.10.10&name={tem.DVR_usre}&password={tem.DVR_possword}";


                    using (HttpClient client = new HttpClient())
                    {

                        HttpResponseMessage response = client.GetAsync(url).Result;

                        var results = response.Content.ReadAsStringAsync().Result;
                        if (results != "[]")
                        {
                            ReportFormsDto report = new ReportFormsDto() { DVROnLine = 100 };
                            listreportForms.Add(report);
                            return listreportForms;
                        }
                    }

                }

            }


            return null;







        } 
        #endregion



    }

}





   

