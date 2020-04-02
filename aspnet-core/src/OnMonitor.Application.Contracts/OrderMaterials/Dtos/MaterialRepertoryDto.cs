using System;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.OrderMaterials.Dtos
{
    public class MaterialRepertoryDto : EntityDto<int>
    {

        /// <summary>
        /// ����Id
        /// </summary>
        public Guid ProductInfoId { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// �洢λ��
        /// </summary>
        public string RepertoryLocation { get; set; }

    }
}