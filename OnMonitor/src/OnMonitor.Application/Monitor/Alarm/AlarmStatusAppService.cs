using Microsoft.AspNetCore.Authorization;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor.Alarm
{

 // [Authorize(Roles ="admin")]
    public class AlarmStatusAppService :// ApplicationService
  CrudAppService<
  AlarmStatus,//定义实体
  AlarmStatusDto,//定义DTO
  Int32, //实体的主键
  PagedAndSortedResultRequestDto, //获取分页排序
  UpdateAlarmStatusDto, //用于创建实体
  UpdateAlarmStatusDto> //用于更新实体
  , IAlarmStatusAppService
    {
        IRepository<AlarmStatus, Int32> _repository;
        IRepository<AlarmManageState, Int32> _alarmManagerepository;
        IRepository<Alarm, Int32> _alarmrepository;
        IRepository<AlarmHost, Int32> _alrmHostrepository;
        IRepository<MonitorRoom, Int32> _monitorRoomsRepository;
        public AlarmStatusAppService(IRepository<AlarmStatus, Int32> repository,IRepository<AlarmManageState, Int32> alarmManagerepository, IRepository<Alarm, Int32> alarmrepository, IRepository<AlarmHost, Int32> alrmHostrepository, IRepository<MonitorRoom, Int32> monitorRoomsRepository) : base(repository)
        {
            _alarmManagerepository=alarmManagerepository;
            _repository = repository;
            _alrmHostrepository = alrmHostrepository;
            _alarmrepository = alarmrepository;
            _monitorRoomsRepository = monitorRoomsRepository;

        }

      


        /// <summary>
        /// 条件筛选查询
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<RequstAlarmStatusDto> GetRequstList(ConditionAlarmStatusDto condition, PagedSortedRequestDto input)
        {

            //加载RequstAlarmStatusDto
            var data = from a in _monitorRoomsRepository
                       join b in _alrmHostrepository on a.RoomLocation equals b.Monitoring_room
                       join c in _alarmrepository on b.AlarmHost_ID equals c.AlarmHost_ID
                       join d in _repository on new { b.AlarmHostIP, c.Channel_ID } equals new { d.AlarmHostIP, d.Channel_ID }
                       select new RequstAlarmStatusDto
                       {

                           Factory = a.Factory,
                           RoomType = a.RoomType,
                           RoomLocation = a.RoomLocation,

                           AlarmHostID = b.AlarmHost_ID,
                           AlarmHostIP = b.AlarmHostIP,

                           department = c.department,
                           Alarm_ID = c.Alarm_ID,
                           Build = c.Build,
                           floor = c.floor,
                           Channel_ID = c.Channel_ID,
                           GeteType = c.GeteType,
                           IsOpenDoor = c.IsOpenOrClosed,
                           SensorType = c.SensorType,
                           Location = c.Location,


                           BypassState = d.BypassState,
                           IsDefence = d.IsDefence,
                           IsAlarm = d.IsAlarm,
                           IsAnomaly = d.IsAnomaly,
                           TreatmentState = d.TreatmentState,

                           Remark = d.Remark,
                           Id = d.Id,
                           LastModificationTime = d.LastModificationTime,

                       };
          

            #region 条件筛选

            if (!string.IsNullOrEmpty(condition.Factory))//厂区筛选
            {
                data = data.Where(u => u.Factory.Contains(condition.Factory));
            }
            if (!string.IsNullOrEmpty(condition.RoomType))//监控室类别筛选
            {
                data = data.Where(u => u.RoomType.Contains(condition.RoomType));
            }
            if (!string.IsNullOrEmpty(condition.RoomLocation))//监控室筛选
            {
                data = data.Where(u => u.RoomLocation.Contains(condition.RoomLocation));
            }

            if (!string.IsNullOrEmpty(condition.Build))//楼栋筛选
            {
                data = data.Where(u => u.Build.Contains(condition.Build));
            }
            if (!string.IsNullOrEmpty(condition.floor))//楼层筛选
            {
                data = data.Where(u => u.floor.Contains(condition.floor));
            }
            if (!string.IsNullOrEmpty(condition.Location))//位置筛选
            {
                data = data.Where(u => u.Location.Contains(condition.Location));
            }
            if (!string.IsNullOrEmpty(condition.Alarm_ID))//编号筛选
            {
                data = data.Where(u => u.Alarm_ID.Contains(condition.Alarm_ID));
            }
            if (!string.IsNullOrEmpty(condition.department))//编号筛选
            {
                data = data.Where(u => u.department.Contains(condition.department));
            }
            if (!string.IsNullOrEmpty(condition.SensorType))//传感器筛选
            {
                data = data.Where(u => u.SensorType.Contains(condition.SensorType));
            }

            if (condition.BypassState != null)//旁路筛选
            {
                data = data.Where(u => u.BypassState == condition.BypassState);
            }
            if (condition.IsDefence != null)//布防筛选
            {
                data = data.Where(u => u.IsDefence == condition.IsDefence);
            }
            if (condition.IsAnomaly != null)//在线筛选
            {
                data = data.Where(u => u.IsAnomaly == condition.IsAnomaly);
            }
            if (condition.IsOpenDoor != null)//开岗筛选
            {
                data = data.Where(u => u.IsOpenDoor == condition.IsOpenDoor);
            }
            if (condition.IsAlarm != null)//报警筛选
            {
                data = data.Where(u => u.IsAlarm == condition.IsAlarm);
            }
            if (condition.TreatmentState != null)//报警筛选
            {
                data = data.Where(u => u.TreatmentState == condition.TreatmentState);
            }

            #endregion

            var requstdata = data.OrderBy(u => input.Sorting).PageBy(input.SkipCount, input.MaxResultCount);


            return new PagedResultDto<RequstAlarmStatusDto>() { Items = requstdata.ToList(), TotalCount = data.Count() };
        }
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public List<RequstAlarmStatusDto> GetRequstListAll()
        {

            //加载RequstAlarmStatusDto
            var data = from a in _monitorRoomsRepository
                       join b in _alrmHostrepository on a.RoomLocation equals b.Monitoring_room
                       join c in _alarmrepository on b.AlarmHost_ID equals c.AlarmHost_ID
                       join d in _repository on new { b.AlarmHostIP, c.Channel_ID } equals new { d.AlarmHostIP, d.Channel_ID }
                       select new RequstAlarmStatusDto
                       {

                           Factory = a.Factory,
                           RoomType = a.RoomType,
                           RoomLocation = a.RoomLocation,

                           AlarmHostID = b.AlarmHost_ID,
                           AlarmHostIP = b.AlarmHostIP,

                           department = c.department,
                           Alarm_ID = c.Alarm_ID,
                           Build = c.Build,
                           floor = c.floor,
                           Channel_ID = c.Channel_ID,
                           GeteType = c.GeteType,
                           IsOpenDoor = c.IsOpenOrClosed,
                           SensorType = c.SensorType,
                           Location = c.Location,


                           BypassState = d.BypassState,
                           IsDefence = d.IsDefence,
                           IsAlarm = d.IsAlarm,
                           IsAnomaly = d.IsAnomaly,
                           TreatmentState = d.TreatmentState,

                           Remark = d.Remark,
                           Id = d.Id,
                           LastModificationTime = d.LastModificationTime,

                       };

            

            data = data.OrderByDescending(u => u.LastModificationTime);

            var dd = data.ToList();
            return dd;
          
        }

    }






}




   



   

