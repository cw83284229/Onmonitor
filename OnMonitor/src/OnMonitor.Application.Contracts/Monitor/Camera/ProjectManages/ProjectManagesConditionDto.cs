using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Monitor
{

    public partial class ProjectManagesConditionDto
    {
        

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
        /// 验收开始时间
        /// </summary>
        [StringLength(55)]
        public string AcceptanceDataStart { get; set; }
        /// <summary>
        /// 验收结束时间
        /// </summary>
        [StringLength(55)]
        public string AcceptanceDataEnd { get; set; }
        /// <summary>
        /// 施工厂商
        /// </summary>
        [StringLength(55)]
        public string ManufacturerName { get; set; }
      
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
   
        public string Camera_ID { get; set; }
  
    }
}
