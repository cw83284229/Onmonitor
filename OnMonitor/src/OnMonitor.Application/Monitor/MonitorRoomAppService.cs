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

namespace OnMonitor.Monitor
{

 // [Authorize(Roles ="admin")]
    public class MonitorRoomAppService :// ApplicationService
  CrudAppService<
  MonitorRoom,//定义实体
  MonitorRoomDto,//定义DTO
  Int32, //实体的主键
  PagedAndSortedResultRequestDto, //获取分页排序
  UpdateMonitorRoomDto, //用于创建实体
  UpdateMonitorRoomDto> //用于更新实体
  , IMonitorRoomAppService

    {
       
        public MonitorRoomAppService(IRepository<MonitorRoom, Int32> repository) : base(repository)
        {

           
        }













    }








    //}






}




   



   

