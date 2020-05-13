
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;



namespace OnMonitor.OrderMaterials

{ 

    /// <summary>
    /// 货品信息
    /// </summary>
    public class ProductInfo:AuditedAggregateRoot<Guid>
    {

        /// <summary>
        /// 产品编号
        /// </summary>
        public string MaterialsNumber { get; set; }


        /// <summary>
        /// 材料类型
        /// </summary>
        public string MaterialsType { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string MaterialsName { get; set; }

        /// <summary>
        /// 材料型号
        /// </summary>
        public string MaterialSpecification { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string MaterialManufacturer { get; set; }
       
        /// <summary>
        /// 材料图片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string units { get; set; }
        
        /// <summary>
        /// 原始价格
        /// </summary>
        public decimal MateriralsPrice { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }



      

     

        protected ProductInfo()
        {
        }

        public ProductInfo(
            Guid id,
            string materialsNumber,
            string materialsType,
            string materialsName,
            string picture,
            string units,
            decimal materiralsPrice,
            decimal marketPrice,
            string remark
        ) :base(id)
        {
            MaterialsNumber = materialsNumber;
            MaterialsType = materialsType;
            MaterialsName = materialsName;
            Picture = picture;
            units = units;
            MateriralsPrice = materiralsPrice;
            MarketPrice = marketPrice;
            Remark = remark;
        }
    }
}
