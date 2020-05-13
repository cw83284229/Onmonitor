using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace OnMonitor.OrderMaterials
{
  public  class MaterialRepertory: Entity<int>
    {

        
      

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
        /// <summary>
        /// 存储位置
        /// </summary>
        public string RepertoryLocation { get; set; }





    }
}
