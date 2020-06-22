using Microsoft.AspNetCore.Authorization;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor
{
  //  [Authorize(Roles = "admin")]
   
    public class CameraAppService :// ApplicationService
   CrudAppService<
   Camera,//定义实体
   CameraDto,//定义DTO
   Int32, //实体的主键
   PagedAndSortedResultRequestDto, //获取分页排序
   UpdateCameraDto, //用于创建实体
   UpdateCameraDto> //用于更新实体
   , ICameraAppService

    {
        IRepository<Camera, int> _repository;


        public CameraAppService(IRepository<Camera, int> repository) : base(repository)
        {
            _repository = repository;
        }

        #region 对数据进行筛选操作
        /// <summary>
        /// 通道主机号搜索数据库
        /// </summary>

        public async Task<List<CameraDto>> GetListByDVRID(string DVRID)
        {
            var camera = _repository.AsQueryable().Where(u => u.DVR_ID == DVRID).ToList();
            //  var get = await _repository.GetListAsync();

            foreach (var item in camera)
            {
                if (item.Direction != null)
                {   //繁体转简体
                    item.Direction = ChineseConverter.Convert(item.Direction, ChineseConversionDirection.TraditionalToSimplified);
                }

                if (item.Location != null)
                {
                    item.Location = ChineseConverter.Convert(item.Location, ChineseConversionDirection.TraditionalToSimplified);
                }

            }


            var cameras = ObjectMapper.Map<List<Camera>, List<CameraDto>>(camera);

            return cameras;
        }
        /// <summary>
        /// 通过镜头好搜索数据库
        /// </summary>
        /// <param name="CameraID">镜头号</param>
        /// <returns></returns>
        public async Task<List<CameraDto>> GetListByCameraID(string CameraID)
        {
            var data1 = _repository.AsQueryable().Where(u => u.Camera_ID == CameraID);

            var data = ObjectMapper.Map<IQueryable<Camera>, List<CameraDto>>(data1);

            return data;
        }
       
       
        #endregion

        #region 多条件查询
        /// <summary>
        /// 监控镜头多条件查询
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CameraDto>> GetListByCondition(CameraCondition condition, PagedSortedRequestDto input)
        {
            var data = from a in _repository
                        select new CameraDto
                        {
                            Id = a.Id,
                            DVR_ID=a.DVR_ID,
                            channel_ID=a.channel_ID,
                            Camera_ID=a.Camera_ID,
                            Camera_Tpye=a.Camera_Tpye,
                            Build=a.Build,
                            floor=a.floor,
                            Direction=a.Direction,
                            Location=a.Location,
                            department=a.department,
                            Alarm_ID=a.Alarm_ID,
                            category=a.category,
                            Cost_code=a.Cost_code,
                            CreationTime=a.CreationTime,
                            CreatorId=a.CreatorId,
                            install_time=a.install_time,
                            LastModificationTime=a.LastModificationTime,
                            LastModifierId=a.LastModifierId,
                            manufacturer=a.manufacturer,
                            MonitorClassification=a.MonitorClassification,
                            Monitoring_room=a.Monitoring_room,
                            Remark=a.Remark
                        };
            
            if (!String.IsNullOrEmpty(condition.Build))
            {
                data=data.Where(u => u.Build == condition.Build);
            }
            if (!String.IsNullOrEmpty(condition.floor))
            {
                data = data.Where(u => u.floor == condition.floor);
            }

            if (!String.IsNullOrEmpty(condition.DVR_ID))
            {
                data = data.Where(u => u.DVR_ID == condition.DVR_ID);
            }

            if (!String.IsNullOrEmpty(condition.Camera_ID))
            {
                data = data.Where(u => u.Camera_ID == condition.Camera_ID);
            }
            if (!String.IsNullOrEmpty(condition.Location))
            {
                data = data.Where(u => u.Location.Contains(condition.Location));
            }
            if (!String.IsNullOrEmpty(condition.install_time))
            {
                data = data.Where(u => u.install_time.Contains(condition.install_time));
            }
        
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
              var  data1 = data.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount);
                return new PagedResultDto<CameraDto> { TotalCount = data.ToList().Count, Items = data1.ToList() };
            }
            else
            {
              var  data2 = data.OrderBy(d => d.Id).PageBy(input.SkipCount, input.MaxResultCount);
                return new PagedResultDto<CameraDto> { TotalCount = data.ToList().Count, Items = data2.ToList() };
            }
         //   return new PagedResultDto<CameraDto> { TotalCount = data.ToList().Count, Items = data.ToList() };

        }

        #endregion

        #region 模糊搜索
        public PagedResultDto<CameraDto> GetListBylike(string condition, PagedSortedRequestDto input)
        {
            //加载CameraDto
            var data = from a in _repository
                       select new CameraDto
                       {
                           Id = a.Id,
                           DVR_ID = a.DVR_ID,
                           channel_ID = a.channel_ID,
                           Camera_ID = a.Camera_ID,
                           Camera_Tpye = a.Camera_Tpye,
                           Build = a.Build,
                           floor = a.floor,
                           Direction = a.Direction,
                           Location = a.Location,
                           department = a.department,
                           Alarm_ID = a.Alarm_ID,
                           category = a.category,
                           Cost_code = a.Cost_code,
                           CreationTime = a.CreationTime,
                           CreatorId = a.CreatorId,
                           install_time = a.install_time,
                           LastModificationTime = a.LastModificationTime,
                           LastModifierId = a.LastModifierId,
                           manufacturer = a.manufacturer,
                           MonitorClassification = a.MonitorClassification,
                           Monitoring_room = a.Monitoring_room,
                           Remark = a.Remark
                       };
            IQueryable<CameraDto> data1;


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
                return new PagedResultDto<CameraDto>() { Items = data1.ToList(), TotalCount = data.Count() };
            }
            else
            {

                condition = ChineseConverter.Convert(condition, ChineseConversionDirection.SimplifiedToTraditional);
                //按 楼栋-楼层位置搜索

                if (condition.Length > 4)
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
                                    data = data.Where(u => u.Location.Contains(str2));
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
                if (data.Where(u => u.department.Contains(condition)).Count() != 0)
                {

                    data = data.Where(u => u.Direction.Contains(condition));

                }

                if (!input.Sorting.IsNullOrWhiteSpace())
                {
                    data1 = data.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount);
                }
                else
                {
                    data1 = data.OrderBy(d => d.Id).PageBy(input.SkipCount, input.MaxResultCount);
                }
                return new PagedResultDto<CameraDto>() { Items = data1.ToList(), TotalCount = data.Count() };
            }
        }

        #endregion

        #region 多项目添加
        /// <summary>
        /// list多项目添加
        /// </summary>
        /// <param name="cameraDtos"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CameraDto>> PostList(List<UpdateCameraDto> cameraDtos)
        {

            if (cameraDtos == null)
            {
                return null;
            }
            List<Camera> cameras = new List<Camera>();
            var data = ObjectMapper.Map<List<UpdateCameraDto>, List<Camera>>(cameraDtos);
            //简繁转换
            string simplifieddata = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            string traditionaldata = ChineseConverter.Convert(simplifieddata, ChineseConversionDirection.SimplifiedToTraditional);
            var data2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Camera>>(traditionaldata);



            foreach (var item in data2)
            {
                var camera = _repository.InsertAsync(item);
                cameras.Add(camera.Result);
            }
            var camerasdto = ObjectMapper.Map<List<Camera>, List<CameraDto>>(cameras);
            return new PagedResultDto<CameraDto> { TotalCount = camerasdto.Count, Items = camerasdto };

        }
        #endregion

        #region 重写增/改方法，增加简繁转换
       [Authorize(Roles = "operation")]
        public override Task<CameraDto> CreateAsync(UpdateCameraDto input)
        {

            string data = Newtonsoft.Json.JsonConvert.SerializeObject(input);

            string data2 = ChineseConverter.Convert(data, ChineseConversionDirection.SimplifiedToTraditional);

            UpdateCameraDto input2 = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateCameraDto>(data2);

            return base.CreateAsync(input2);
        }

        [Authorize(Roles = "operation")]
        public override Task<CameraDto> UpdateAsync(int id, UpdateCameraDto input)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(input);

            string data2 = ChineseConverter.Convert(data, ChineseConversionDirection.SimplifiedToTraditional);

            UpdateCameraDto input2 = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateCameraDto>(data2);

            return base.UpdateAsync(id, input2);
        }

        #endregion

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<CameraDto>> GetListAllAsync()
        {

         var dataall= await _repository.GetListAsync();

          var requst=  ObjectMapper.Map<List<Camera>, List<CameraDto>>(dataall);

            return requst;

        }
    }
}