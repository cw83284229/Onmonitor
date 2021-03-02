using System;
using System.IO;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor.Alarm
{


    public interface IAlarmAppService://IApplicationService
        ICrudAppService<
            AlarmDto,//定义DTO
            Int32, //实体的主键
            PagedAndSortedResultRequestDto, //获取分页排序
            UpdateAlarmDto, //用于创建实体
            UpdateAlarmDto> //用于更新实体
    {


        public AlarmHostDto GetAlarmHostDto(string Alarm_ID);
        public AlarmDto GetAlarmDto(string Alarm_ID);


    }
      
}
