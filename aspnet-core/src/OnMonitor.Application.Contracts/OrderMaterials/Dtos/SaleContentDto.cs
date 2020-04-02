using System;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.OrderMaterials.Dtos
{
    public class SaleContentDto : AuditedEntityDto<Guid>
    {
        public string SaleStore { get; set; }

        public string SaleTime { get; set; }

        public string ShipmentMethod { get; set; }

        public string Remark { get; set; }

        public int IsShipments { get; set; }
    }
}