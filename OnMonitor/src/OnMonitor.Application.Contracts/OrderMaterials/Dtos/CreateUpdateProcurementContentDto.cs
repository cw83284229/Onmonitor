using System;
using System.ComponentModel;
namespace OnMonitor.OrderMaterials.Dtos
{
    public class CreateUpdateProcurementContentDto
    {
        [DisplayName("供货厂商")]
        public string Manufacturer { get; set; }

        [DisplayName("进货时间")]
        public string ProcurementTime { get; set; }

        [DisplayName("供货方式")]
        public string ProcurementMethod { get; set; }

        [DisplayName("备注")]
        public string Remark { get; set; }

        [DisplayName("是否入系统")]
        public int IsShipments { get; set; }
    }
}