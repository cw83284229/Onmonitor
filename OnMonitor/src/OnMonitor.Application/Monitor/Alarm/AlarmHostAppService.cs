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
    public class AlarmHostAppService :// ApplicationService
  CrudAppService<
  AlarmHost,//定义实体
  AlarmHostDto,//定义DTO
  Int32, //实体的主键
  PagedAndSortedResultRequestDto, //获取分页排序
  UpdateAlarmHostDto, //用于创建实体
  UpdateAlarmHostDto> //用于更新实体
  , IAlarmHostAppService

    {
       
        public AlarmHostAppService(IRepository<AlarmHost, Int32> repository) : base(repository)
        {

           
        }













    }








    //}






}




   



   

