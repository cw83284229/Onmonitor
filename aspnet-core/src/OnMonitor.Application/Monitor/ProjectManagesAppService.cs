using Microsoft.AspNetCore.Authorization;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor
{

   // [Authorize(Roles ="operator")]

    public class ProjectManagesAppService :// ApplicationService
    CrudAppService<
    ProjectManages,//定义实体
    ProjectManagesDto,//定义DTO
    Int32, //实体的主键
    PagedAndSortedResultRequestDto, //获取分页排序
    ProjectManagesDto, //用于创建实体
    ProjectManagesDto> //用于更新实体
    , IProjectManagesAppService

    {
       
        public ProjectManagesAppService(IRepository<ProjectManages, Int32> repository) : base(repository)
        {

         }

    

       }

      
       


    }



   

