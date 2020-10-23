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
    public class AlarmManageStateAppService :// ApplicationService
  CrudAppService<
  AlarmManageState,//定义实体
  AlarmManageStateDto,//定义DTO
  Int32, //实体的主键
  PagedAndSortedResultRequestDto, //获取分页排序
  UpdateAlarmManageStateDto, //用于创建实体
  UpdateAlarmManageStateDto> //用于更新实体
  , IAlarmManageStateAppService

    {
       
        public AlarmManageStateAppService(IRepository<AlarmManageState, Int32> repository) : base(repository)
        {

           
        }













    }








    //}






}




   



   

