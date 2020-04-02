using System;
using System.ComponentModel;
namespace OnMonitor.OrderMaterials.Dtos
{
    public class CreateUpdateProcurementDeltailDto
    {
        [DisplayName("ProcurementDeltailProcurementContentId")]
        public Guid ProcurementContentId { get; set; }

        [DisplayName("ProcurementDeltailProductInfoId")]
        public Guid ProductInfoId { get; set; }

        [DisplayName("ProcurementDeltailCount")]
        public long Count { get; set; }

        [DisplayName("ProcurementDeltailPrice")]
        public decimal Price { get; set; }
    }
}