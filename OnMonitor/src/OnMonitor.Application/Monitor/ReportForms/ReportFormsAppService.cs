using OnMonitor.MonitorRepair;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
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
        IRepository<CameraRepair> _cameraRepairrepository;
        IRepository<MonitorRoom> _monitorRommrepository;
        IDVRCheckInfoAppService _dvrCheckInfoAppService;
        List<DVRCameraDto> listdVRCamera;
        List<CameraRepairDto> listcameraRepair;
        List<DVRCameraRepairDto> listDVRCameraRepair;
        List<DVRCheckInfoDto> dVRCheckInfos;
        List<DVRCheckInfoDto> dVRCheckOnlines;

        public ReportFormsAppService(IRepository<DVR> dvrrepository, IRepository<Camera> camerarepository, IRepository<CameraRepair> cameraRepairrepository, IRepository<MonitorRoom> monitorRommrepository, IDVRCheckInfoAppService dvrCheckInfoAppService)
        {
            _camerarepository = camerarepository;
            _dvrrepository = dvrrepository;
            _cameraRepairrepository = cameraRepairrepository;
           _dvrCheckInfoAppService=dvrCheckInfoAppService;
            _monitorRommrepository = monitorRommrepository;

        }

        /// <summary>
        /// 获取镜头维修表数据
        /// </summary>
        private void dVRCameraRepairlist()
        {


           var dVRCameraRepairDtos = from a in _camerarepository
                                  join q in _cameraRepairrepository on a.Camera_ID equals q.Camera_ID into q_join
                                  from b in q_join.DefaultIfEmpty()
                                  join c in _dvrrepository on a.DVR_ID equals c.DVR_ID into c_join
                                  from v in c_join.DefaultIfEmpty()
                                  select new DVRCameraRepairDto
                                  {
                                      Factory = v.Factory,
                                      DVR_Room = v.Monitoring_room,
                                      DVR_ID = a.DVR_ID,
                                      channel_ID = a.channel_ID,
                                      Camera_ID = a.Camera_ID,
                                      Build = a.Build,
                                      floor = a.floor,
                                      Direction = a.Direction,
                                      Location = a.Location,
                                      department = a.department,
                                      Camera_Tpye = a.Camera_Tpye,
                                      install_time = a.install_time,
                                      manufacturer = a.manufacturer,
                                      AnomalyTime = b.AnomalyTime,
                                      CollectTime = b.CollectTime,
                                      AnomalyType = b.AnomalyType,
                                      AnomalyGrade = b.AnomalyGrade,
                                      Registrar = b.Registrar,
                                      RepairState = b.RepairState,
                                      RepairedTime = b.RepairedTime,
                                      Accendant = b.Accendant,
                                      RepairDetails = b.RepairDetails,
                                      RepairFirm = b.RepairFirm,
                                      Supervisor = b.Supervisor,
                                      ReplacePart = b.ReplacePart,
                                      ProjectAnomaly = b.ProjectAnomaly,
                                      Id = a.Id

                                  };

             listDVRCameraRepair = dVRCameraRepairDtos.ToList();

           dVRCheckInfos = _dvrCheckInfoAppService.GetDVRInfoCheckFalseByDVRroom(null);
            PagedSortedRequestDto input = new PagedSortedRequestDto() { SkipCount = 0, MaxResultCount = 999 };
            dVRCheckOnlines = _dvrCheckInfoAppService.GetDVRInfoByCondition(null, null,true, null, null,null,input).Result.Items.ToList();

        }


        #region 按监控室获取报表信息

        /// <summary>
        /// 按监控室分类数据
        /// </summary>
        /// <returns></returns>
        public List<ReportFormsDto> GetReportFormsByMonitorRoom()

        {
            dVRCameraRepairlist();
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
            var DVRRooms = _monitorRommrepository.ToList();

         

            foreach (var item in DVRRooms)
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.DVRRoom = item.RoomLocation;
                //加载主机总数
                var data = listDVRCameraRepair.Where(u => u.DVR_Room == item.RoomLocation).Select(i => new { DVR_ID = i.DVR_ID }).Distinct();
                formsDto.DVRTotal  = data.Count();
                //加载主机异常数
                List<DVRCheckInfoDto> data2 = new List<DVRCheckInfoDto>();
                foreach (var tem in data)
                {
                    data2.Add(dVRCheckInfos.Where(u => u.DVR_ID == tem.DVR_ID).FirstOrDefault());
                }
                data2 = data2.Distinct().DefaultIfEmpty().ToList();
                formsDto.DVRAnomaly = data2.Count;
                //加载主机在线数据
                List<DVRCheckInfoDto> data8 = new List<DVRCheckInfoDto>();
                foreach (var tem in data)
                {
                    data8.Add(dVRCheckOnlines.Where(u => u.DVR_ID == tem.DVR_ID).FirstOrDefault());
                }
                data8 = data8.Distinct().DefaultIfEmpty().ToList();
                formsDto.DVROnLine = data8.Count;

                //镜头总数
                formsDto.CameraTotal = listDVRCameraRepair.Where(u => u.DVR_Room == item.RoomLocation).Select(i => new { CameraID = i.Camera_ID }).Distinct().Count();
                //加载异常数量
                formsDto.CameraAnomaly = listDVRCameraRepair.Where(u => u.DVR_Room == item.RoomLocation).Where(i=>i.RepairState==false).Distinct().Count();
               //加载维修数据
                formsDto.RepairTotal=listDVRCameraRepair.Where(u => u.DVR_Room == item.RoomLocation).Where(i => i.RepairState == true).Distinct().Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                //异常比例
                if (formsDto.CameraTotal!=0)
                {
                    formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal; ;
                }
              

                listreportForms.Add(formsDto);
            }
            //加载总数
            ReportFormsDto formsDto1 = new ReportFormsDto();
            formsDto1.DVRRoom = "Total";
            formsDto1.CameraAnomaly = listDVRCameraRepair.Where(u=>u.RepairState==false).Distinct().Count();
            formsDto1.RepairTotal= listDVRCameraRepair.Where(u => u.RepairState == true).Distinct().Count();

            formsDto1.CameraTotal = listDVRCameraRepair.Select(i => new { CameraID = i.Camera_ID}).Distinct().Count();
            formsDto1.DVRTotal = listDVRCameraRepair.Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
            //异常+维修总数
            formsDto1.CameraAnomalyRepair = formsDto1.CameraAnomaly + formsDto1.RepairTotal;
            //异常比例
            formsDto1.AnomalyProportion = (float)formsDto1.CameraAnomaly / (float)formsDto1.CameraTotal;
           
            listreportForms.Add(formsDto1);




            return listreportForms;
        }
        #endregion

        #region 按楼栋获取报表信息

        /// <summary>
        /// 按楼栋分类数据
        /// </summary>
        /// <returns></returns>
        public List<ReportFormsDto> GetReportFormsByBuild()

        {
            dVRCameraRepairlist();
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
            var builds = _camerarepository.Select(i => new { build = i.Build}).Distinct();

            foreach (var item in builds)
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.Camera_build = item.build;
                //加载主机总数
                formsDto.DVRTotal = listDVRCameraRepair.Where(u => u.Build == item.build).Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
                //镜头总数
                formsDto.CameraTotal = listDVRCameraRepair.Where(u => u.Build == item.build).Select(i => new { CameraID = i.Camera_ID }).Distinct().Count();
                //加载异常数量
                formsDto.CameraAnomaly = listDVRCameraRepair.Where(u => u.Build == item.build).Where(i => i.RepairState == false).Distinct().Count();
                //加载维修数据
                formsDto.RepairTotal = listDVRCameraRepair.Where(u => u.Build == item.build).Where(i => i.RepairState == true).Distinct().Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                //异常比例
                if (formsDto.CameraTotal != 0)
                {
                    formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
                }


                listreportForms.Add(formsDto);
            }
            //加载总数
            ReportFormsDto formsDto1 = new ReportFormsDto();
            formsDto1.DVRRoom = "Total";
            formsDto1.CameraAnomaly = listDVRCameraRepair.Where(u => u.RepairState == false).Distinct().Count();
            formsDto1.RepairTotal = listDVRCameraRepair.Where(u => u.RepairState == true).Distinct().Count();

            formsDto1.CameraTotal = listDVRCameraRepair.Select(i => new { CameraID = i.Camera_ID }).Distinct().Count();
            formsDto1.DVRTotal = listDVRCameraRepair.Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
            //异常+维修总数
            formsDto1.CameraAnomalyRepair = formsDto1.CameraAnomaly + formsDto1.RepairTotal;
            //异常比例
             formsDto1.AnomalyProportion = (float)formsDto1.CameraAnomaly / (float)formsDto1.CameraTotal;
           
            listreportForms.Add(formsDto1);




            return listreportForms;
        }
        #endregion

        #region 按部门获取报表信息

        /// <summary>
        /// 按部门分类数据
        /// </summary>
        /// <returns></returns>
        public List<ReportFormsDto> GetReportFormsBydepartment()

        {
            dVRCameraRepairlist();
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
            var departments = _camerarepository.Select(i => new { department = i.department }).Distinct();

            foreach (var item in departments)
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.Camera_build = item.department;
                //加载主机总数
                formsDto.DVRTotal = listDVRCameraRepair.Where(u => u.department == item.department).Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
                //镜头总数
                formsDto.CameraTotal = listDVRCameraRepair.Where(u => u.department == item.department).Select(i => new { CameraID = i.Camera_ID }).Distinct().Count();
                //加载异常数量
                formsDto.CameraAnomaly = listDVRCameraRepair.Where(u => u.department == item.department).Where(i => i.RepairState == false).Distinct().Count();
                //加载维修数据
                formsDto.RepairTotal = listDVRCameraRepair.Where(u => u.department == item.department).Where(i => i.RepairState == true).Distinct().Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                //异常比例
                if (formsDto.CameraTotal != 0)
                {
                    formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
                }


                listreportForms.Add(formsDto);
            }
            //加载总数
            ReportFormsDto formsDto1 = new ReportFormsDto();
            formsDto1.DVRRoom = "Total";
            formsDto1.CameraAnomaly = listDVRCameraRepair.Where(u => u.RepairState == false).Distinct().Count();
            formsDto1.RepairTotal = listDVRCameraRepair.Where(u => u.RepairState == true).Distinct().Count();

            formsDto1.CameraTotal = listDVRCameraRepair.Select(i => new { CameraID = i.Camera_ID }).Distinct().Count();
            formsDto1.DVRTotal = listDVRCameraRepair.Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
            //异常+维修总数
            formsDto1.CameraAnomalyRepair = formsDto1.CameraAnomaly + formsDto1.RepairTotal;
            //异常比例
            formsDto1.AnomalyProportion = (float)formsDto1.CameraAnomaly / (float)formsDto1.CameraTotal;

            listreportForms.Add(formsDto1);




            return listreportForms;
        }
        #endregion

        #region 按年份获取报表信息

        /// <summary>
        /// 按年份分类数据
        /// </summary>
        /// <returns></returns>
        public List<ReportFormsDto> GetReportFormsByYear()

        {
            dVRCameraRepairlist();
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
            //var yeartimes = _camerarepository.Select(i => new { YearTime = i.install_time }).Distinct().DefaultIfEmpty().ToList();
            //List<string> yearlist =new List<string>() ;
            //foreach (var item in yeartimes)
            //{

            //    if (item.YearTime!=null&& item.YearTime != "")
            //    {
            //        yearlist.Add(item.YearTime.Substring(0, 4));
            //    }



            //}
            //yearlist = yearlist.Distinct().ToList();


            for (int i = 2007; i < DateTime.Now.Year; i++)
            
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.install_time = i.ToString();
                //加载主机总数
                formsDto.DVRTotal = listDVRCameraRepair.Where(u => u.install_time.Contains(i.ToString())).Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
                //镜头总数
                formsDto.CameraTotal = listDVRCameraRepair.Where(u => u.install_time.Contains(i.ToString())).Select(i => new { CameraID = i.Camera_ID }).Distinct().Count();
                //加载异常数量
                formsDto.CameraAnomaly = listDVRCameraRepair.Where(u => u.install_time.Contains(i.ToString())).Where(i => i.RepairState == false).Distinct().Count();
                //加载维修数据
                formsDto.RepairTotal = listDVRCameraRepair.Where(u => u.install_time.Contains(i.ToString())).Where(i => i.RepairState == true).Distinct().Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                //异常比例
                if (formsDto.CameraTotal != 0)
                {
                    formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
                }


                listreportForms.Add(formsDto);
            }
            //加载总数
            ReportFormsDto formsDto1 = new ReportFormsDto();
            formsDto1.DVRRoom = "Total";
            formsDto1.CameraAnomaly = listDVRCameraRepair.Where(u => u.RepairState == false).Distinct().Count();
            formsDto1.RepairTotal = listDVRCameraRepair.Where(u => u.RepairState == true).Distinct().Count();

            formsDto1.CameraTotal = listDVRCameraRepair.Select(i => new { CameraID = i.Camera_ID }).Distinct().Count();
            formsDto1.DVRTotal = listDVRCameraRepair.Select(i => new { DVR_ID = i.DVR_ID }).Distinct().Count();
            //异常+维修总数
            formsDto1.CameraAnomalyRepair = formsDto1.CameraAnomaly + formsDto1.RepairTotal;
            //异常比例
            formsDto1.AnomalyProportion = (float)formsDto1.CameraAnomaly / (float)formsDto1.CameraTotal;

            listreportForms.Add(formsDto1);




            return listreportForms;
        }
        #endregion

        #region 获取监控维修年份分析数据
        /// <summary>
        /// 获取监控镜头维修年份分析数据
        /// </summary>
        /// <returns></returns>
       
        public async Task<Dictionary<string, int>> GetVintageAnalysisAsync()
        {
            dVRCameraRepairlist();
            int time = DateTime.Now.Year;

           // var data = _cameraRepairrepository.GetListAsync().Result.ToList();
            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            for (int i = 0; i < 15; i++)
            {
                string year = (time - i).ToString();
                var data1 =listDVRCameraRepair.Where(u => u.install_time.Contains(year));

                keyValues.Add(year, data1.Count());

            }
            return keyValues;
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





   

