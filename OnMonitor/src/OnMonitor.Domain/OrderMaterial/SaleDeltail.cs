using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace OnMonitor.OrderMaterials
{
  public  class SaleDeltail: Entity<int>
    {

        
      

        /// <summary>
        /// 订单Id
        /// </summary>
        public Guid SaleContentId { get; set; }
        /// <summary>
        /// 材料Id
        /// </summary>
        public Guid ProductInfoId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public long  Count { get; set; }
      
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }




        protected SaleDeltail()
        {
        }

        public SaleDeltail(
            int id,
            Guid saleContentId,
            Guid productInfoId,
            long count,
            decimal price
        ) :base(id)
        {
            SaleContentId = saleContentId;
            ProductInfoId = productInfoId;
            Count = count;
            Price = price;
        }
    }
}
