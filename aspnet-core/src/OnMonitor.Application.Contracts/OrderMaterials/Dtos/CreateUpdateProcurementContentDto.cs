using System;
using System.ComponentModel;
namespace OnMonitor.OrderMaterials.Dtos
{
    public class CreateUpdateProcurementContentDto
    {
        [DisplayName("��������")]
        public string Manufacturer { get; set; }

        [DisplayName("����ʱ��")]
        public string ProcurementTime { get; set; }

        [DisplayName("������ʽ")]
        public string ProcurementMethod { get; set; }

        [DisplayName("��ע")]
        public string Remark { get; set; }

        [DisplayName("�Ƿ���ϵͳ")]
        public int IsShipments { get; set; }
    }
}