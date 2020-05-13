using System;
using System.ComponentModel;
namespace OnMonitor.OrderMaterials.Dtos
{
    public class CreateUpdateProductInfoDto
    {
        [DisplayName("ProductInfoMaterialsNumber")]
        public string MaterialsNumber { get; set; }

        [DisplayName("ProductInfoMaterialsType")]
        public string MaterialsType { get; set; }

        [DisplayName("ProductInfoMaterialsName")]
        public string MaterialsName { get; set; }

        /// <summary>
        /// �����ͺ�
        /// </summary>
        public string MaterialSpecification { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string MaterialManufacturer { get; set; }

        [DisplayName("ProductInfoPicture")]
        public string Picture { get; set; }

        [DisplayName("ProductInfounits")]
        public string units { get; set; }

        [DisplayName("ProductInfoMateriralsPrice")]
        public decimal MateriralsPrice { get; set; }

        [DisplayName("ProductInfoMarketPrice")]
        public decimal MarketPrice { get; set; }

        [DisplayName("ProductInfoRemark")]
        public string Remark { get; set; }
    }
}