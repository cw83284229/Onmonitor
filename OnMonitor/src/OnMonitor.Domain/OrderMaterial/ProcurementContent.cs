
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace OnMonitor.OrderMaterials
{
   
   public class ProcurementContent:AuditedAggregateRoot<Guid>
    {
         /// <summary>
         /// 供货厂商
         /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public string ProcurementTime { get; set; }
         /// <summary>
         /// 入口方法
         /// </summary>
        public string ProcurementMethod { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否入系统
        /// </summary>

        public int IsShipments { get; set; }

     

        protected ProcurementContent()
        {
        }

        public ProcurementContent(
            Guid id,
            string manufacturer,
            string procurementTime,
            string procurementMethod,
            string remark,
            int isShipments
        ) :base(id)
        {
            Manufacturer = manufacturer;
            ProcurementTime = procurementTime;
            ProcurementMethod = procurementMethod;
            Remark = remark;
            IsShipments = isShipments;
        }
    }
}
