using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor
{

    // [Authorize(Roles ="admin")]
    public interface IMonitorRoomAppService :// ApplicationService
  ICrudAppService<
 
  MonitorRoomDto,//定义DTO
  Int32, //实体的主键
  PagedAndSortedResultRequestDto, //获取分页排序
  UpdateMonitorRoomDto, //用于创建实体
  UpdateMonitorRoomDto> //用于更新实体
  

    {
       
      












    }








    //}






}




   



   

