
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.MonitorRepair
{

    
    public interface ICameraRepairAppService://IApplicationService
        ICrudAppService<
            CameraRepairDto,//定义DTO
            Int32, //实体的主键
            PagedAndSortedResultRequestDto, //获取分页排序
            UpdateCameraRepairDto, //用于创建实体
           UpdateCameraRepairDto> //用于更新实体
    {
        //  public List<CameraRepairDto> GetList();


        public  PagedResultDto<RequstCameraRepairDto> GetRepairsList(PagedSortedRequestDto input);
        public PagedResultDto<RequstCameraRepairDto> GetRepairsListByCondition(QueryCondition condition, PagedSortedRequestDto input);

    }
}
