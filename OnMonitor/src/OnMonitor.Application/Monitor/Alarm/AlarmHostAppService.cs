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
  int, //实体的主键
  PagedAndSortedResultRequestDto, //获取分页排序
  UpdateAlarmHostDto, //用于创建实体
  UpdateAlarmHostDto> //用于更新实体
  , IAlarmHostAppService

    {
        IRepository<AlarmHost, int> _repository;
        public AlarmHostAppService(IRepository<AlarmHost, int> repository) : base(repository)
        {
            _repository = repository;
           
        }
        /// <summary>
        /// 依据主机IP获取报警主机信息
        /// </summary>
        /// <param name="AlarmHostIP"></param>
        /// <returns></returns>
        public List<AlarmHost> GetAlarmHosts(string AlarmHostIP)
        {
          var data=  _repository.GetListAsync().Result;

            if (!string.IsNullOrEmpty(AlarmHostIP))
            {
                data = data.Where(u=>u.AlarmHostIP==AlarmHostIP).ToList();
            }

            return data;
        
        }











    }








    //}






}




   



   

