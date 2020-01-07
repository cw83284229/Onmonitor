
using Microsoft.AspNetCore.Authorization;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor
{

    
    public interface IProjectManagesAppService://IApplicationService
        ICrudAppService<
            ProjectManagesDto,//定义DTO
            Int32, //实体的主键
            PagedAndSortedResultRequestDto, //获取分页排序
            ProjectManagesDto, //用于创建实体
            ProjectManagesDto> //用于更新实体
    {
        //   public String GetList();

       // public Task<List<CameraDto>> GetListByCondition(CameraCondition condition);

    }
}
