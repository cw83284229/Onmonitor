using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.OrderMaterials
{
    public class MaterialRepertoryAppService : CrudAppService<MaterialRepertory, MaterialRepertoryDto, int, PagedAndSortedResultRequestDto, CreateUpdateMaterialRepertoryDto, CreateUpdateMaterialRepertoryDto>,
        IMaterialRepertoryAppService
    {
        public MaterialRepertoryAppService(IRepository<MaterialRepertory, int> repository) : base(repository)
        {
        }
    }
}