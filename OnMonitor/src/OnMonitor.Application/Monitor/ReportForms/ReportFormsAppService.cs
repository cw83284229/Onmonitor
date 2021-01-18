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
        IRepository<DVRCheckInfo, Int32> _dvrchenkrepository;

        List<DVRCameraDto> listdVRCamera;
        List<CameraRepairDto> listcameraRepair;
        IQueryable<DVRCameraRepairDto> listDVRCameraRepair;
        IQueryable<RequstDVRCheckInfoDto> dvrChanklist;
        List<DVRCheckInfoDto> dVRCheckOnlines;

        public ReportFormsAppService(IRepository<DVR> dvrrepository, IRepository<Camera> camerarepository, IRepository<CameraRepair> cameraRepairrepository, IRepository<MonitorRoom> monitorRommrepository, IRepository<DVRCheckInfo, Int32> dvrcheckrepository)
        {
            _camerarepository = camerarepository;
            _dvrrepository = dvrrepository;
            _cameraRepairrepository = cameraRepairrepository;
            _monitorRommrepository = monitorRommrepository;
            _dvrchenkrepository = dvrcheckrepository;
        }

        /// <summary>
        /// 获取镜头维修表数据
        /// </summary>
        private void dVRCameraRepairlist()
        {


            listDVRCameraRepair  = from a in _cameraRepairrepository
                                  join b in _camerarepository on a.Camera_ID equals b.Camera_ID into ab
                                  from abi in ab.DefaultIfEmpty()

                                  join c in _dvrrepository on abi.DVR_ID equals c.DVR_ID into abc
                                  from abci in abc.DefaultIfEmpty()

                                  join d in _monitorRommrepository on abci.Monitoring_room equals d.RoomLocation into abcd
                                  from abcdi in abcd.DefaultIfEmpty()

                                  select new DVRCameraRepairDto
                                  {
                                      Factory = abcdi.Factory,
                                      DVR_Room = abcdi.RoomLocation,

                                      DVR_ID = abci.DVR_ID,
                                      channel_ID = abi.channel_ID,
                                      Camera_ID = abi.Camera_ID,
                                      Build = abi.Build,
                                      floor = abi.floor,
                                      Direction = abi.Direction,
                                      Location = abi.Location,
                                      department = abi.department,
                                      Camera_Tpye = abi.Camera_Tpye,
                                      install_time = abi.install_time,
                                      manufacturer = abi.manufacturer,

                                      AnomalyTime = a.AnomalyTime,
                                      CollectTime = a.CollectTime,
                                      AnomalyType = a.AnomalyType,
                                      AnomalyGrade = a.AnomalyGrade,
                                      Registrar = a.Registrar,
                                      RepairState = a.RepairState,
                                      RepairedTime = a.RepairedTime,
                                      Accendant = a.Accendant,
                                      RepairDetails = a.RepairDetails,
                                      RepairFirm = a.RepairFirm,
                                      Supervisor = a.Supervisor,
                                      ReplacePart = a.ReplacePart,
                                      ProjectAnomaly = a.ProjectAnomaly,
                                      Id = a.Id
                                  };



            //镜头资料加载
           var listdVRCameraDTOS = from a in _dvrrepository
                                 join b in _camerarepository on a.DVR_ID equals b.DVR_ID
                                 select new DVRCameraDto
                                 {
                                     DVR_ID = a.DVR_ID,
                                     Monitoring_room = a.Monitoring_room,
                                     Factory=a.Factory,

                                     CameraID = b.Camera_ID,
                                     Build = b.Build,
                                     floor = b.floor,
                                     Direction = b.Direction,
                                     Location = b.Location,
                                     Department=b.department
                                     
                            
                            };


            listdVRCamera = listdVRCameraDTOS.ToList();


        }
        #region 按监控室获取报表信息

        /// <summary>
        /// 按监控室分类数据
        /// </summary>
        /// <returns></returns>
        public List<ReportFormsDto> GetReportFormsByMonitorRoom()

        {
            dVRCameraRepairlist();
            var listroom = listdVRCamera.Select(u => new { Monitoring_room = u.Monitoring_room }).Distinct();
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
            foreach (var item in listroom)
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.DVRRoom = item.Monitoring_room;
    
                //镜头总数
                formsDto.CameraTotal =listdVRCamera.Where(u =>u.Monitoring_room == item.Monitoring_room).Distinct().Count();
               

                //加载异常数量
                formsDto.CameraAnomaly = listDVRCameraRepair.Where(u => u.DVR_Room == item.Monitoring_room).Where(i => i.RepairState == false).Count();
               //加载维修数据
                formsDto.RepairTotal=listDVRCameraRepair.Where(u => u.DVR_Room == item.Monitoring_room).Where(i => i.RepairState == true).Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                //异常比例
                if (formsDto.CameraTotal!=0)
                {
                    formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal; ;
                }
              

                listreportForms.Add(formsDto);
            }
         

            return listreportForms;
        }

        /// <summary>
        /// 按监控室分类数据,指定时间及异常等级/类别
        /// </summary>
        /// <returns></returns>
        public List<ReportFormsDto> GetReportFormsByMonitorRoomorAnomalyCondition(string StartTime,string EndTime,string AnomalyGrade,string AnomalyType)

        {
            dVRCameraRepairlist();
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
            var DVRRooms = _monitorRommrepository.ToList();
            listDVRCameraRepair = listDVRCameraRepair.Where(u => u.AnomalyTime != null);
            if (!string.IsNullOrEmpty(StartTime)&&!string.IsNullOrEmpty(EndTime))
            {
                listDVRCameraRepair = listDVRCameraRepair.Where(r => r.AnomalyTime != null).Where(u => string.Compare(u.AnomalyTime, StartTime) >= 0 && string.Compare(u.AnomalyTime, EndTime) <= 0);
            }
            if (!string.IsNullOrEmpty(AnomalyGrade))
            {
                listDVRCameraRepair = listDVRCameraRepair.Where(r=>r.AnomalyGrade!=null).Where(u => u.AnomalyGrade.Contains(AnomalyGrade));
            }
            if (!string.IsNullOrEmpty(AnomalyType))
            {
                listDVRCameraRepair = listDVRCameraRepair.Where(r => r != null).Where(u => u.AnomalyType == AnomalyGrade);
            }

            foreach (var item in DVRRooms)
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.DVRRoom = item.RoomLocation;
                //加载主机总数
                var data = _dvrrepository.Where(u => u.Monitoring_room == item.RoomLocation).Distinct();
                formsDto.DVRTotal = data.Count();

                //镜头总数
                formsDto.CameraTotal = listdVRCamera.Where(u => u.Monitoring_room == item.RoomLocation).Distinct().Count();


                //加载异常数量
                formsDto.CameraAnomaly = listDVRCameraRepair.Where(u => u.DVR_Room == item.RoomLocation).Where(i => i.RepairState == false).Count();
                //加载维修数据
                formsDto.RepairTotal = listDVRCameraRepair.Where(u => u.DVR_Room == item.RoomLocation).Where(i => i.RepairState == true).Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                //异常比例
                if (formsDto.CameraTotal != 0)
                {
                    formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal; ;
                }


                listreportForms.Add(formsDto);
            }


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
            var builds = listdVRCamera.Select(i => new { build = i.Build}).Distinct();

            foreach (var item in builds)
            {
                if (!String.IsNullOrEmpty(item.build))
                {
                    ReportFormsDto formsDto = new ReportFormsDto();
                    formsDto.Camera_build = item.build;
                    ////主机总数
                    //formsDto。 = listDVRCameraRepair.Where(u => u.Build == item.build).Select(t => new { DVR_ID = t.DVR_ID }).Distinct().Count();

                    //镜头总数
                    formsDto.CameraTotal = listDVRCameraRepair.Where(u => u.Build == item.build).Select(t=>new { DVR_ID=t.DVR_ID}).Distinct().Count();
                    //加载异常数量
                    formsDto.CameraAnomaly = listDVRCameraRepair.Where(r => !string.IsNullOrEmpty(r.Build)).Where(u => u.Build == item.build).Where(i => i.RepairState == false).Count();
                    //加载维修数据
                    formsDto.RepairTotal = listDVRCameraRepair.Where(r => !string.IsNullOrEmpty(r.Build)).Where(u => u.Build == item.build).Where(i => i.RepairState == true).Count();
                    //异常+维修总数
                    formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                    //异常比例
                    if (formsDto.CameraTotal != 0)
                    {
                        formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
                    }


                    listreportForms.Add(formsDto);
                }
              
            }
         

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
            var departments = listdVRCamera.Select(i => new { department = i.Department }).Distinct();

            foreach (var item in departments)
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.department = item.department;
              
                //镜头总数
                formsDto.CameraTotal = listDVRCameraRepair.Where(u => u.department == item.department).Distinct().Count();
                //加载异常数量
                formsDto.CameraAnomaly = listDVRCameraRepair.Where(r=>!string.IsNullOrEmpty(r.department)).Where(u => u.department == item.department).Where(i => i.RepairState == false).Count();
                //加载维修数据
                formsDto.RepairTotal = listDVRCameraRepair.Where(r => !string.IsNullOrEmpty(r.department)).Where(u => u.department == item.department).Where(i => i.RepairState == true).Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                //异常比例
                if (formsDto.CameraTotal != 0)
                {
                    formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
                }
                listreportForms.Add(formsDto);
            }

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
            var yeartimes = _camerarepository.Select(i => new { YearTime = i.install_time }).Distinct().DefaultIfEmpty().ToList();
            List<string> yearlist =new List<string>() ;
            Dictionary<int,string> dic=new Dictionary<int, string>();
            foreach (var item in yeartimes)
            {

                if (item.YearTime != null && item.YearTime != "")
                {
                    yearlist.Add(item.YearTime.Substring(0, 4));
                }
            }
            yearlist = yearlist.Distinct().ToList();
            
           

            foreach (var item in yearlist)
            
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.install_time = item;
              
                //镜头总数
                formsDto.CameraTotal = listDVRCameraRepair.Where(u => u.install_time.Contains(item)).Distinct().ToList().Count();
                //加载异常数量
                
                formsDto.CameraAnomaly = listDVRCameraRepair.Where(i => i.RepairState == false).Where(u => u.install_time!=null).Where(r => r.install_time.Contains(item)).Count();
                //加载维修数据
                formsDto.RepairTotal = listDVRCameraRepair.Where(i => i.RepairState == true).Where(u => u.install_time!=null).Where(r=>r.install_time.Contains(item)).Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                //异常比例
                if (formsDto.CameraTotal != 0)
                {
                    formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
                }


                listreportForms.Add(formsDto);
            }

            return listreportForms;
        }
        #endregion

       

        #region 获取在线DVR数量
        /// <summary>
        /// 获取在线DVR数量
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReportFormsDto>> GetDVROnlineTotal()
        {

            var dvrlist = _dvrrepository.Join(_dvrchenkrepository, b => b.DVR_ID,p=>p.DVR_ID,(b,p)  => new RequstDVRCheckInfoDto
            {
                Monitoring_room=b.Monitoring_room,
                DVR_ID=b.DVR_ID,
                DVR_Online=p.DVR_Online,
                SNChenk=p.SNChenk,
                LastModificationTime=p.LastModificationTime,
                DiskChenk=p.DiskChenk,
                Id=p.Id
                


            });
          
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
           
            var Chanklist = dvrlist.Where(u => u.LastModificationTime > DateTime.Now.AddDays(-1)).Where(i => i.LastModificationTime < DateTime.Now.AddDays(+1)).ToList();

          
            
            foreach (var item in _monitorRommrepository)
            {
                
                    ReportFormsDto formsDto = new ReportFormsDto();
                    formsDto.DVRRoom = item.RoomLocation;
                    //加载主机总数
                    formsDto.DVRTotal = Chanklist.Where(u => u.Monitoring_room == item.RoomLocation).Distinct().Count();

                    formsDto.DVRAnomaly = Chanklist.Where(r=>r.DiskChenk!=null).Where(u => u.DiskChenk == false).Count();

                    formsDto.DVROnLine = Chanklist.Where(r => r.DVR_Online != null).Where(u => u.DVR_Online == false).Count();

                    listreportForms.Add(formsDto);
            }

            return listreportForms;


        }
        #endregion

        #region 按年份获取报表信息

        /// <summary>
        /// 按时间分类数据
        /// </summary>
        /// <returns></returns>
        public List<ReportFormsDto> GetReportFormsByTime(string StartTime, string EndTime, string AnomalyGrade, string AnomalyType,string MonitorRoom)

        {
            dVRCameraRepairlist();
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();

            List<DateTime> timelist = new List<DateTime>();

            DateTime dateTimeStart = DateTime.Parse(StartTime);
            DateTime dateTimeEnd = DateTime.Parse(EndTime);
            for (int i = 0; dateTimeStart.Day+i < dateTimeEnd.Day; i++)
            {
               
                timelist.Add(dateTimeStart.AddDays(i));
            }

            if (!string.IsNullOrEmpty(AnomalyGrade))
            {
              listDVRCameraRepair=  listDVRCameraRepair.Where(u => u.AnomalyGrade.Contains(AnomalyGrade));
            }
            if (!string.IsNullOrEmpty(AnomalyType))
            {
                listDVRCameraRepair = listDVRCameraRepair.Where(u => u.AnomalyType.Contains(AnomalyType));
            }
            if (!string.IsNullOrEmpty(MonitorRoom))
            {
                listDVRCameraRepair = listDVRCameraRepair.Where(r => r.DVR_Room!=null).Where(u => u.DVR_Room.Contains(MonitorRoom));
            }



            foreach (var item in timelist)

            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.install_time = item.ToString("yyyy-MM-dd");
              
                //加载异常数量
                formsDto.CameraAnomaly = listDVRCameraRepair.Where(u=>u.CollectTime.Contains(item.ToString("yyyy-MM-dd"))).Count();
                //加载维修数据
                formsDto.RepairTotal = listDVRCameraRepair.Where(u => u.RepairedTime.Contains(item.ToString("yyyy-MM-dd"))).Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;

                formsDto.CameraTotal = _camerarepository.Count();


                listreportForms.Add(formsDto);
            }

            return listreportForms;
        }
        #endregion
    }

}





   

