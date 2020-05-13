using System;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.OrderMaterials.Dtos
{
    public class ProcurementContentDto : AuditedEntityDto<Guid>
    {
        public string Manufacturer { get; set; }

        public string ProcurementTime { get; set; }

        public string ProcurementMethod { get; set; }

        public string Remark { get; set; }

        public int IsShipments { get; set; }
    }
}