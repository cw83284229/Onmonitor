using System;
using System.IO;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor.Alarm
{


    public interface IAlarmManageStateAppService://IApplicationService
        ICrudAppService<
            AlarmManageStateDto,//定义DTO
            Int32, //实体的主键
            PagedAndSortedResultRequestDto, //获取分页排序
            UpdateAlarmManageStateDto, //用于创建实体
            UpdateAlarmManageStateDto> //用于更新实体
    {



     
    }
      
}
