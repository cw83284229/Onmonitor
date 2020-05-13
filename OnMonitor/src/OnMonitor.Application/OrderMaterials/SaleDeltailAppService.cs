using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.OrderMaterials
{
    public class SaleDeltailAppService : CrudAppService<SaleDeltail, SaleDeltailDto, int, PagedAndSortedResultRequestDto, CreateUpdateSaleDeltailDto, CreateUpdateSaleDeltailDto>,
        ISaleDeltailAppService
    {
        public SaleDeltailAppService(IRepository<SaleDeltail, int> repository) : base(repository)
        {
        }
    }
}