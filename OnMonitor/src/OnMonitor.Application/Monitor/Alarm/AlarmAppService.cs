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










    }








    //}






}




   



   

