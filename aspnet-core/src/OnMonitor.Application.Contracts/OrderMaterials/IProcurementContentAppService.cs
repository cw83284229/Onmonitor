using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.OrderMaterials
{
    public interface IProcurementContentAppService :
        ICrudAppService< 
            ProcurementContentDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateProcurementContentDto,
            CreateUpdateProcurementContentDto>
    {

    }
}