
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor
{
    public interface IDVRAppService ://IApplicationService
         ICrudAppService<
             DVRDto,//定义DTO
             Int32, //实体的主键
             PagedAndSortedResultRequestDto, //获取分页排序
             UpdateDVRDto, //用于创建实体
             UpdateDVRDto> //用于更新实体
    {
        //  public String GetList8();
        public Task<PagedResultDto<DVRDto>> GetListByCondition(string Monitoring_room, String Build, String Floor, string DVR_ID);


    }
}
