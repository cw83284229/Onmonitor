using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.OrderMaterials
{
    public class ProductInfoAppService : CrudAppService<ProductInfo, ProductInfoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProductInfoDto, CreateUpdateProductInfoDto>,
        IProductInfoAppService
    {
        public ProductInfoAppService(IRepository<ProductInfo, Guid> repository) : base(repository)
        {
        }
    }
}