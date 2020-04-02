using System;
using System.ComponentModel;
namespace OnMonitor.OrderMaterials.Dtos
{
    public class CreateUpdateSaleDeltailDto
    {
        [DisplayName("SaleDeltailSaleContentId")]
        public Guid SaleContentId { get; set; }

        [DisplayName("SaleDeltailProductInfoId")]
        public Guid ProductInfoId { get; set; }

        [DisplayName("SaleDeltailCount")]
        public long Count { get; set; }

        [DisplayName("SaleDeltailPrice")]
        public decimal Price { get; set; }
    }
}