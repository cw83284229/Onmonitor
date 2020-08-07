using Microsoft.AspNetCore.Authorization;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using OnMonitor.Monitor;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
namespace OnMonitor.MonitorRepair
{

 //  [Authorize(Roles = "admin")]
    public class CameraRepairAppService ://ApplicationService
    CrudAppService<
    CameraRepair,//定义实体
    CameraRepairDto,//定义DTO
    Int32, //实体的主键
    PagedAndSortedResultRequestDto, //获取分页排序
    UpdateCameraRepairDto, //用于创建实体
    UpdateCameraRepairDto> //用于更新实体
    , ICameraRepairAppService

    {
        IRepository<CameraRepair, Int32> _cameraRepairrepository;
        IRepository<Camera, Int32> _camerarepository;
      //  List<CameraRepair> cameraRepairDtos;

        public CameraRepairAppService(IRepository<CameraRepair, Int32> cameraRepairrepository, IRepository<Camera, Int32> camerarepository) : base(cameraRepairrepository)
        {
            _cameraRepairrepository = cameraRepairrepository;
            _camerarepository = camerarepository;
        }
        #region 分页查询
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  PagedResultDto<RequstCameraRepairDto> GetRepairsList(PagedSortedRequestDto input)
        {

          //  var cameras = await _camerarepository.GetListAsync();
          //  var cameraRepair = await _cameraRepairrepository.GetListAsync();
            //加载CameraDto
            var data = from a in _camerarepository
                       join b in _cameraRepairrepository on a.Camera_ID equals b.Camera_ID
                       select new RequstCameraRepairDto
                       {
                           DVR_Room = a.Monitoring_room,
                           DVR_ID = a.DVR_ID,
                           channel_ID = a.channel_ID,
                           Camera_ID = a.Camera_ID,
                           Build = a.Build,
                           floor = a.floor,
                           Direction = a.Direction,
                           Location=a.Location,
                           department=a.department,
                           Camera_Tpye=a.Camera_Tpye,
                           install_time=a.install_time,
                           manufacturer=a.manufacturer,
                           AnomalyTime=b.AnomalyTime,
                           CollectTime=b.CollectTime,
                           AnomalyType=b.AnomalyType,
                           AnomalyGrade=b.AnomalyGrade,
                           Registrar=b.Registrar,
                           RepairState=b.RepairState,
                           RepairedTime=b.RepairedTime,
                           Accendant=b.Accendant,
                           RepairDetails=b.RepairDetails,
                           RepairFirm=b.RepairFirm,
                           Supervisor=b.Supervisor,
                           ReplacePart=b.ReplacePart,
                           ProjectAnomaly=b.ProjectAnomaly,
                           NoSignal=b.NoSignal,
                           Remark=b.Remark,
                           Id=b.Id,
                           CreatorId=b.CreatorId,
                           CreationTime=b.CreationTime,
                           LastModificationTime=b.LastModificationTime,
                           LastModifierId=b.LastModifierId,
                       };
            //分页排序查询加载
            List<RequstCameraRepairDto> cameraRepairDtos;
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                cameraRepairDtos = data.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount).ToList();
            }
            else
            {
                cameraRepairDtos = data.OrderBy(d=>d.Id).PageBy(input.SkipCount, input.MaxResultCount).ToList();
            }




            return new PagedResultDto<RequstCameraRepairDto>()

            {
                TotalCount = data.Count(),
                Items = cameraRepairDtos
            };
        }

        #endregion

        #region 条件筛选
        /// <summary>
        /// 按条件筛选
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="input">分页</param>
        /// <returns></returns>
        public  PagedResultDto<RequstCameraRepairDto> GetRepairsListByCondition(QueryCondition condition, PagedSortedRequestDto input)
        {

          //  var cameras = await _camerarepository.GetListAsync();
          //  var cameraRepair = await _cameraRepairrepository.GetListAsync();
            //加载CameraDto
            var data = from a in _camerarepository
                       join b in _cameraRepairrepository on a.Camera_ID equals b.Camera_ID
                       select new RequstCameraRepairDto
                       {
                           DVR_Room = a.Monitoring_room,
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
                           NoSignal = b.NoSignal,
                           Remark = b.Remark,
                            Id = b.Id,
                           CreatorId = b.CreatorId,
                           CreationTime = b.CreationTime,
                           LastModificationTime = b.LastModificationTime,
                           LastModifierId = b.LastModifierId,
                       };

            #region 维修状态筛选
            if (!string.IsNullOrEmpty(condition.AnomalyTime))
            {
                data = data.Where(u => u.AnomalyTime.Contains(condition.AnomalyTime));
            }
            if (!string.IsNullOrEmpty(condition.CollectTime))
            {
                data = data.Where(u => u.AnomalyTime.Contains(condition.CollectTime));
            }
            if (!string.IsNullOrEmpty(condition.AnomalyType))
            {
                data = data.Where(u => u.AnomalyType == condition.AnomalyType);
            }
            if (!string.IsNullOrEmpty(condition.AnomalyGrade))
            {
                data = data.Where(u => u.AnomalyGrade == condition.AnomalyGrade);
            }
            if (!string.IsNullOrEmpty(condition.Registrar))
            {
                data = data.Where(u => u.Registrar == condition.Registrar);
            }
            if (!string.IsNullOrEmpty(condition.Accendant))
            {
                data = data.Where(u => u.Accendant == condition.Accendant);
            }
            if (!string.IsNullOrEmpty(condition.RepairedTime))
            {
                data = data.Where(u => u.RepairedTime.Contains(condition.RepairedTime));

            }
            if (!string.IsNullOrEmpty(condition.RepairFirm))
            {
                data = data.Where(u => u.RepairFirm == condition.RepairFirm);
            }
            if (!string.IsNullOrEmpty(condition.Supervisor))
            {
                data = data.Where(u => u.Supervisor == condition.Supervisor);

            }
            if (condition.RepairState != null)
            {
                data = data.Where(u => u.RepairState == condition.RepairState);

            }
            if (condition.NoSignal != null)
            {
                data = data.Where(u => u.NoSignal == condition.NoSignal);

            }
            #endregion

            #region 镜头信息筛选

            if (!string.IsNullOrEmpty(condition.DVR_Room))
            {
               data = data.Where(u => u.DVR_Room == condition.DVR_Room);
            }
            if (!string.IsNullOrEmpty(condition.Build))
            {
                data = data.Where(u => u.Build == condition.Build);
            }
            if (!string.IsNullOrEmpty(condition.floor))
            {
                data = data.Where(u => u.floor == condition.floor);
            }
            if (!string.IsNullOrEmpty(condition.DVR_ID))
            {
                data = data.Where(u => u.DVR_ID == condition.DVR_ID);
            }
            if (!string.IsNullOrEmpty(condition.Camera_ID))
            {
                data = data.Where(u => u.Camera_ID == condition.Camera_ID);
            }
            if (!string.IsNullOrEmpty(condition.Location))
            {
                data = data.Where(u => u.Location.Contains(condition.Location));
            }
            if (!string.IsNullOrEmpty(condition.department))
            {
                data = data.Where(u => u.department == condition.department);
            }

            #endregion


            //分页
            List<RequstCameraRepairDto> cameraRepairDtos;
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                cameraRepairDtos = data.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount).ToList();
            }
            else
            {
                cameraRepairDtos = data.OrderBy(d => d.Id).PageBy(input.SkipCount, input.MaxResultCount).ToList();
            }
            //返回
            return new PagedResultDto<RequstCameraRepairDto>()

            {
                TotalCount = data.Count(),
                Items = cameraRepairDtos
            };
        }

        #endregion

        #region 重写增/改方法，进行简繁转换
        /// <summary>
        /// 重写新增表，增加简体转繁体功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async Task<CameraRepairDto> CreateRustAsync(UpdateCameraRepairDto input)
        {


            string data = Newtonsoft.Json.JsonConvert.SerializeObject(input);

            string data2 = ChineseConverter.Convert(data, ChineseConversionDirection.SimplifiedToTraditional);

            UpdateCameraRepairDto input2 = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateCameraRepairDto>(data2);

            var reuust=await base.CreateAsync(input2);


            return reuust;

        }
        /// <summary>
        /// 重写修改方法，简体转繁体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<CameraRepairDto> UpdateAsync(int id, UpdateCameraRepairDto input)
        {

            string data = Newtonsoft.Json.JsonConvert.SerializeObject(input);

            string data2 = ChineseConverter.Convert(data, ChineseConversionDirection.SimplifiedToTraditional);

            UpdateCameraRepairDto input2 = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateCameraRepairDto>(data2);

            return base.UpdateAsync(id, input2);
        }
        #endregion

        #region 模糊查询功能
        // 模糊查询 按 楼栋-楼层位置搜索
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="dvrRoom">监控室</param>
        /// <param name="condition">查询条件</param>
        /// <param name="RepairSatate">维修状态</param>
        /// <param name="department">部门</param>
        /// <param name="AnomalyTimeStart">异常查询开始时间</param>
        /// <param name="AnomalyTimeEnd">异常查询结束时间</param>
        /// <param name="RepairedTimeStart">维修开始时间</param>
        /// <param name="RepairedTimeEnd">维修结束时间</param>
        /// <param name="AnomalyType">维修类别</param>
        /// <param name="input">分页</param>
        /// <returns></returns>
        public PagedResultDto<RequstCameraRepairDto> GetRepairsListBylike(string dvrRoom,string condition,bool? RepairState, string department, string AnomalyTimeStart, string AnomalyTimeEnd,string RepairedTimeStart, string RepairedTimeEnd,string AnomalyType, PagedSortedRequestDto input)
        {
            //加载CameraDto
            var dataall = from a in _camerarepository
                       join b in _cameraRepairrepository on a.Camera_ID equals b.Camera_ID
                       select new RequstCameraRepairDto
                       {
                           DVR_Room = a.Monitoring_room,
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
                           NoSignal = b.NoSignal,
                           Remark = b.Remark,
                           Id = b.Id,
                           CreatorId = b.CreatorId,
                           CreationTime = b.CreationTime,
                           LastModificationTime = b.LastModificationTime,
                           LastModifierId = b.LastModifierId,
                       };
            IQueryable<RequstCameraRepairDto> data1;
            IQueryable<RequstCameraRepairDto> data;
            data = dataall;
           
            //监控室筛选
            if (!string.IsNullOrEmpty(dvrRoom))
            {
                data = data.Where(u => u.DVR_Room == dvrRoom);
            }

            //状态筛选
            if (RepairState != null)
            {
                data = data.Where(u => u.RepairState == RepairState);
            }
            //部门筛选
            if (!string.IsNullOrEmpty(department))
            {
                department = ChineseConverter.Convert(department, ChineseConversionDirection.SimplifiedToTraditional);//简体转繁体
                data = data.Where(u => u.department == department);
            }
            //异常时间筛选
            if (!string.IsNullOrEmpty(AnomalyTimeStart)&&!string.IsNullOrEmpty(AnomalyTimeEnd))
            {
                data = data.Where(u =>string.Compare(u.AnomalyTime,AnomalyTimeStart)>=0&&string.Compare(u.AnomalyTime,AnomalyTimeEnd)<=0);
            }
            //维修时间筛选
            if (!string.IsNullOrEmpty(RepairedTimeStart)&&!string.IsNullOrEmpty(RepairedTimeEnd))
            {
                data = data.Where(u => string.Compare(u.RepairedTime,RepairedTimeStart) >= 0 && string.Compare(u.RepairedTime,RepairedTimeEnd) <= 0); 
            }
            //异常类别筛选
            if (!string.IsNullOrEmpty(AnomalyType))
            {
                data = data.Where(u => u.AnomalyType.Contains(AnomalyType));
            }
            //条件为空返回
            if (condition.IsNullOrEmpty())
            {
                if (!input.Sorting.IsNullOrWhiteSpace())
                {
                    data1 = data.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount);
                }
                else
                {
                    data1 = data.OrderBy(d => d.Id).PageBy(input.SkipCount, input.MaxResultCount);
                }


                return  new PagedResultDto<RequstCameraRepairDto>() { Items= data1.ToList() ,TotalCount=data.Count()} ;
            }
            //模糊查询
            else
            {
                //按 楼栋-楼层位置搜索
               condition = ChineseConverter.Convert(condition, ChineseConversionDirection.SimplifiedToTraditional);//简体转繁体
                if (condition.Length>4)
                {
              
                if (data.Where(u => u.Build.Contains(condition.Substring(0, 3))).ToList().Count != 0)
                {
                    data = data.Where(u => u.Build.Contains(condition.Substring(0, 3)));

                    string str1 = condition.Split('F')[0];

                    if (data.Where(u => u.floor.Contains(str1.Substring(4))).Count() != 0)
                    {
                        data = data.Where(u => u.floor.Contains(str1.Substring(4)));
                        string str2 = condition.Split('F')[1];
                        if (!str2.IsNullOrEmpty())
                            {
                          if (data.Where(u => u.Location.Contains(str2)).Count() != 0)
                               {
                                    data= data.Where(u => u.Location.Contains(str2));
                               }
                                else
                                {
                                    return null;
                                }

                            }
                    }

                }
                }

                //条件筛选
                if (data.Where(u => u.DVR_ID.Contains(condition)).Count() != 0)
                {
                    data = data.Where(u => u.DVR_ID.Contains(condition));
                }
                if (data.Where(u => u.Camera_ID.Contains(condition)).Count() != 0)
                {
                    data = data.Where(u => u.Camera_ID.Contains(condition));
                }
                if (data.Where(u => u.Location.Contains(condition)).Count() != 0)
                {
                    data = data.Where(u => u.Location.Contains(condition));
                }
               
                if (!input.Sorting.IsNullOrWhiteSpace())
                {
                   data1 = data.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount);
                }
                else
                {
                    data1 = data.OrderBy(d => d.Id).PageBy(input.SkipCount, input.MaxResultCount);
                }
                return new PagedResultDto<RequstCameraRepairDto>() { Items = data1.ToList(), TotalCount = data.Count() };
            }
        }

        #endregion
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public List<RequstCameraRepairDto> GetListAll()
        {
          
          //加载CameraDto
          var dataall = from a in _camerarepository
                          join b in _cameraRepairrepository on a.Camera_ID equals b.Camera_ID
                          select new RequstCameraRepairDto
                          {
                              DVR_Room = a.Monitoring_room,
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
                              NoSignal = b.NoSignal,
                              Remark = b.Remark,
                              Id = b.Id,
                              CreatorId = b.CreatorId,
                              CreationTime = b.CreationTime,
                              LastModificationTime = b.LastModificationTime,
                              LastModifierId = b.LastModifierId,
                          };

            return dataall.ToList();

        }


      
    }
}



   

