using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor.Alarm
{


    public interface IAlarmStatusAppService://IApplicationService
        ICrudAppService<
            AlarmStatusDto,//定义DTO
            Int32, //实体的主键
            PagedAndSortedResultRequestDto, //获取分页排序
            UpdateAlarmStatusDto, //用于创建实体
            UpdateAlarmStatusDto> //用于更新实体
    {

        public PagedResultDto<RequstAlarmStatusDto> GetRequstList(ConditionAlarmStatusDto condition, PagedSortedRequestDto input);

        public IQueryable<RequstAlarmStatusDto> GetRequstListAll();

    }
      
}
