using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.OrderMaterials
{
    public class SaleContentAppService : CrudAppService<SaleContent, SaleContentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSaleContentDto, CreateUpdateSaleContentDto>,
        ISaleContentAppService
    {
        public SaleContentAppService(IRepository<SaleContent, Guid> repository) : base(repository)
        {
        }
    }
}