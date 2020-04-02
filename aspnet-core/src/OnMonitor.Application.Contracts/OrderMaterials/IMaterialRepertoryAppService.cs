using System;
using OnMonitor.OrderMaterials.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.OrderMaterials
{
    public interface IMaterialRepertoryAppService :
        ICrudAppService< 
            MaterialRepertoryDto, 
            int, 
            PagedAndSortedResultRequestDto,
            CreateUpdateMaterialRepertoryDto,
            CreateUpdateMaterialRepertoryDto>
    {

    }
}