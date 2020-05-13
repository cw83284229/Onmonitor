using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.OrderMaterials
{
    public interface ISaleDeltailAppService :
        ICrudAppService< 
            SaleDeltailDto, 
            int, 
            PagedAndSortedResultRequestDto,
            CreateUpdateSaleDeltailDto,
            CreateUpdateSaleDeltailDto>
    {

    }
}