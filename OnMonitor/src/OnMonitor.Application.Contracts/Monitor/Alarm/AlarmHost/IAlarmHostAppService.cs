using System;
using System.IO;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor.Alarm
{


    public interface IAlarmHostAppService://IApplicationService
        ICrudAppService<
            AlarmHostDto,//定义DTO
            Int32, //实体的主键
            PagedAndSortedResultRequestDto, //获取分页排序
            UpdateAlarmHostDto, //用于创建实体
            UpdateAlarmHostDto> //用于更新实体
    {



     
    }
      
}
