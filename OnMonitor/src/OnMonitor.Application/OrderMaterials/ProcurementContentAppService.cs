using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.OrderMaterials
{
    public class ProcurementContentAppService : CrudAppService<ProcurementContent, ProcurementContentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProcurementContentDto, CreateUpdateProcurementContentDto>,
        IProcurementContentAppService
    {
        public ProcurementContentAppService(IRepository<ProcurementContent, Guid> repository) : base(repository)
        {
        }
    }
}