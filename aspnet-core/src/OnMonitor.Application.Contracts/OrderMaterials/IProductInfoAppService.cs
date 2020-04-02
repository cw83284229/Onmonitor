using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.OrderMaterials
{
    public interface IProductInfoAppService :
        ICrudAppService< 
            ProductInfoDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateProductInfoDto,
            CreateUpdateProductInfoDto>
    {

    }
}