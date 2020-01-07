using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace OnMonitor.Monitor
{
    
    public partial class ProjectManages : AuditedAggregateRoot<Int32>
    {
        

        /// <summary>
        /// 工程变更类型
        /// </summary>
        [StringLength(55)]
        public string ProjectManageType { get; set; }
        /// <summary>
        /// 工程名称
        /// </summary>
       [StringLength(55)]
        public string ProjectName { get; set; }

        /// <summary>
        /// 工程单号
        /// </summary>
        [StringLength(55)]
        public string ProjectOrder { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [StringLength(55)]
        public string StartWorkDate { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        [StringLength(55)]
        public string CompleteDate { get; set; }
        /// <summary>
        /// 验收时间
        /// </summary>
        [StringLength(55)]
        public string AcceptanceData { get; set; }

        /// <summary>
        /// 施工厂商
        /// </summary>
        [StringLength(55)]
        public string ManufacturerName { get; set; }
        /// <summary>
        /// 工程说明
        /// </summary>
        [StringLength(255)]
        public string ProjectSpecifications { get; set; }
        /// <summary>
        /// 改造楼栋
        /// </summary>
        [StringLength(255)]
        public string Build { get; set; }
        /// <summary>
        /// 改造楼层
        /// </summary>
        [StringLength(255)]
        public string Floor { get; set; }
        /// <summary>
        /// 镜头编号
        /// </summary>
        [StringLength(255)]
        public string Camera_ID { get; set; }
        /// <summary>
        /// 验收结果说明
        /// </summary>
        [StringLength(255)]
        public string AcceptanceResult { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }

    }
}
