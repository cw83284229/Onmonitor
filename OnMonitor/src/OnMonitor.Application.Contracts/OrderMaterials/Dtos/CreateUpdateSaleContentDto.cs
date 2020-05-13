using System;
using System.ComponentModel;
namespace OnMonitor.OrderMaterials.Dtos
{
    public class CreateUpdateSaleContentDto
    {
        [DisplayName("SaleContentSaleStore")]
        public string SaleStore { get; set; }

        [DisplayName("SaleContentSaleTime")]
        public string SaleTime { get; set; }

        [DisplayName("SaleContentShipmentMethod")]
        public string ShipmentMethod { get; set; }

        [DisplayName("SaleContentRemark")]
        public string Remark { get; set; }

        [DisplayName("SaleContentIsShipments")]
        public int IsShipments { get; set; }
    }
}