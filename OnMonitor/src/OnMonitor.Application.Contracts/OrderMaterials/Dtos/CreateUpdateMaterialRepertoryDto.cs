using System;
using System.ComponentModel;
namespace OnMonitor.OrderMaterials.Dtos
{
    public class CreateUpdateMaterialRepertoryDto
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