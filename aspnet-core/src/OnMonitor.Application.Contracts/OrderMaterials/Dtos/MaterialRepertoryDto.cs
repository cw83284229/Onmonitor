using System;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.OrderMaterials.Dtos
{
    public class MaterialRepertoryDto : EntityDto<int>
    {

        /// <summary>
        /// 材料Id
        /// </summary>
        public Guid ProductInfoId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public long Count { get; set; }

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