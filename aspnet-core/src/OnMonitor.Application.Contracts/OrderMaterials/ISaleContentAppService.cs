using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.OrderMaterials
{
    public interface ISaleContentAppService :
        ICrudAppService< 
            SaleContentDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateSaleContentDto,
            CreateUpdateSaleContentDto>
    {

    }
}