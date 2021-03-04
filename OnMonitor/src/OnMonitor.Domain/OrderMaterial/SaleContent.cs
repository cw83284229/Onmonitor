
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace OnMonitor.OrderMaterials
{
   
   public class SaleContent:AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 出货位置
        /// </summary>
        [StringLength(55)]
        public string SaleStore { get; set; }
        /// <summary>
        /// 订货时间
        /// </summary>
        [StringLength(55)]
        public string SaleTime { get; set; }
         /// <summary>
         /// 出货方式
         /// </summary>
        public string ShipmentMethod { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否入装好使用
        /// </summary>

        public int IsShipments { get; set; }

     

        protected SaleContent()
        {
        }

        public SaleContent(
            Guid id,
            string saleStore,
            string saleTime,
            string shipmentMethod,
            string remark,
            int isShipments
        ) :base(id)
        {
            SaleStore = saleStore;
            SaleTime = saleTime;
            ShipmentMethod = shipmentMethod;
            Remark = remark;
            IsShipments = isShipments;
        }
    }
}
