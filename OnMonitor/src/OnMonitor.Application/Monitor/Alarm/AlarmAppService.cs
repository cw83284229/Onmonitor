using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor.Alarm
{

    // [Authorize(Roles ="admin")]
    public class AlarmAppService :// ApplicationService
  CrudAppService<
  Alarm,//定义实体
  AlarmDto,//定义DTO
  Int32, //实体的主键
  PagedAndSortedResultRequestDto, //获取分页排序
  UpdateAlarmDto, //用于创建实体
  UpdateAlarmDto> //用于更新实体
  , IAlarmAppService

    {
        IRepository<Alarm, Int32> _alarmrepository;
        IRepository<AlarmHost, Int32> _alrmHostrepository;
        public AlarmAppService(IRepository<Alarm, Int32> alarmrepository, IRepository<AlarmHost, Int32> alrmHostrepository) : base(alarmrepository)
        {
            _alarmrepository = alarmrepository;
            _alrmHostrepository = alrmHostrepository;
           
        }

        /// <summary>
        /// 依据报警编号获取报警主机信息
        /// </summary>
        /// <param name="Alarm_ID"></param>
        /// <returns></returns>
        public AlarmHostDto GetAlarmHostDto(string Alarm_ID)
        {
            var alarmdata = _alarmrepository.Where(u => u.Alarm_ID == Alarm_ID).FirstOrDefault();
            if (alarmdata!=null)
            {
                var alarmHostdata = _alrmHostrepository.Where(u => u.AlarmHost_ID == alarmdata.AlarmHost_ID).FirstOrDefault();

            var requst=    ObjectMapper.Map<AlarmHost, AlarmHostDto>(alarmHostdata);
                return requst;
            }


            return null;
        
        
        }

        /// <summary>
        /// 依据报警编号获取实体信息
        /// </summary>
        /// <param name="Alarm_ID"></param>
        /// <returns></returns>
        public AlarmDto GetAlarmDto(string Alarm_ID)
        {
            var data = _alarmrepository.GetListAsync().Result;

            var alarms = data.Where(u => u.Alarm_ID.Contains(Alarm_ID)).FirstOrDefault();

            var requst = ObjectMapper.Map<Alarm, AlarmDto>(alarms);
            return requst;
        }


        public List<Alarm> GetAlarmList(string AlarmHostIP)
        {
          var Hostdata=_alrmHostrepository.Where(u=>u.AlarmHostIP==AlarmHostIP).FirstOrDefault();

            var alarmdata = _alarmrepository.Where(u => u.AlarmHost_ID == Hostdata.AlarmHost_ID).ToList();

            return alarmdata;
        
        
        
        
        }




    }








    //}






}




   



   

