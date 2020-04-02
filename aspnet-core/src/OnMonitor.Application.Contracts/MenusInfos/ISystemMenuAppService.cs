using System;
using OnMonitor.MenusInfos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.MenusInfos
{
    public interface ISystemMenuAppService :
        ICrudAppService< 
            SystemMenuDto, 
            long, 
            PagedAndSortedResultRequestDto,
            CreateUpdateSystemMenuDto,
            CreateUpdateSystemMenuDto>
    {

    }
}