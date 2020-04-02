using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.OrderMaterials
{
    public class ProcurementDeltailAppService : CrudAppService<ProcurementDeltail, ProcurementDeltailDto, int, PagedAndSortedResultRequestDto, CreateUpdateProcurementDeltailDto, CreateUpdateProcurementDeltailDto>,
        IProcurementDeltailAppService
    {
        public ProcurementDeltailAppService(IRepository<ProcurementDeltail, int> repository) : base(repository)
        {
        }
    }
}