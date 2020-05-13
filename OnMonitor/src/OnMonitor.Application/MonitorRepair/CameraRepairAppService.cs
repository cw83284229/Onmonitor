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

    [Authorize(Roles = "admin")]
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
        public  PagedResultDto<CameraRepairDto> GetRepairsList(PagedAndSortedResultRequestDto input)
        {

          //  var cameras = await _camerarepository.GetListAsync();
          //  var cameraRepair = await _cameraRepairrepository.GetListAsync();
            //加载CameraDto
            var data = from a in _camerarepository
                       join b in _cameraRepairrepository on a.Camera_ID equals b.Camera_ID
                       select new CameraRepairDto
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
            List<CameraRepairDto> cameraRepairDtos;
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                cameraRepairDtos = data.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount).ToList();
            }
            else
            {
                cameraRepairDtos = data.OrderBy(d=>d.Id).PageBy(input.SkipCount, input.MaxResultCount).ToList();
            }




            return new PagedResultDto<CameraRepairDto>()

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
        public  PagedResultDto<CameraRepairDto> GetRepairsListByCondition(QueryCondition condition, PagedAndSortedResultRequestDto input)
        {

          //  var cameras = await _camerarepository.GetListAsync();
          //  var cameraRepair = await _cameraRepairrepository.GetListAsync();
            //加载CameraDto
            var data = from a in _camerarepository
                       join b in _cameraRepairrepository on a.Camera_ID equals b.Camera_ID
                       select new CameraRepairDto
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
                data = data.Where(u => u.AnomalyTime == condition.AnomalyTime);
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
                data = data.Where(u => u.RepairedTime == condition.RepairedTime);

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
                data = data.Where(u => u.Location == condition.Location);
            }
            if (!string.IsNullOrEmpty(condition.department))
            {
                data = data.Where(u => u.department == condition.department);
            }

            #endregion


            //分页
            List<CameraRepairDto> cameraRepairDtos;
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                cameraRepairDtos = data.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount).ToList();
            }
            else
            {
                cameraRepairDtos = data.OrderBy(d => d.Id).PageBy(input.SkipCount, input.MaxResultCount).ToList();
            }
            //返回
            return new PagedResultDto<CameraRepairDto>()

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
        public override Task<CameraRepairDto> CreateAsync(UpdateCameraRepairDto input)
        {


            string data = Newtonsoft.Json.JsonConvert.SerializeObject(input);

            string data2 = ChineseConverter.Convert(data, ChineseConversionDirection.SimplifiedToTraditional);

            UpdateCameraRepairDto input2 = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateCameraRepairDto>(data2);

            return base.CreateAsync(input2);
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


    }
}



   

