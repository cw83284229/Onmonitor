using System;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.OrderMaterials.Dtos
{
    public class ProductInfoDto : AuditedEntityDto<Guid>
    {
        public string MaterialsNumber { get; set; }

        public string MaterialsType { get; set; }

        public string MaterialsName { get; set; }

        /// <summary>
        /// 材料型号
        /// </summary>
        public string MaterialSpecification { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string MaterialManufacturer { get; set; }

        public string Picture { get; set; }

        public string units { get; set; }

        public decimal MateriralsPrice { get; set; }

        public decimal MarketPrice { get; set; }

        public string Remark { get; set; }
    }
}