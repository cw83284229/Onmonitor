using System;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.OrderMaterials.Dtos
{
    public class ProcurementDeltailDto : EntityDto<int>
    {
        public Guid ProcurementContentId { get; set; }

        public Guid ProductInfoId { get; set; }

        public long Count { get; set; }

        public decimal Price { get; set; }
    }
}