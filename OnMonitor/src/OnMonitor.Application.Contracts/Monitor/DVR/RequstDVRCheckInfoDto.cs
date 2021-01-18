using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Monitor
{
    public  class RequstDVRCheckInfoDto : AuditedEntityDto<Int32>
    {
        /// <summary>
        /// 厂区
        /// </summary>
        [StringLength(255)]
        public string Factory { get; set; }
        /// <summary>
        /// 监控室
        /// </summary>
        [StringLength(255)]
        public string Monitoring_room { get; set; }
        /// <summary>
        /// 楼栋
        /// </summary>
        [StringLength(255)]
        public string Camera_build { get; set; }
        /// <summary>
        /// DVR 通道数量
        /// </summary>
        public int? DVR_Channel { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [StringLength(255)]
        public string department { get; set; }
        /// <summary>
        /// 费用代码
        /// </summary>
        [StringLength(255)]
        public string Cost_code { get; set; }
        /// <summary>
        /// 主机号
        /// </summary>
        [StringLength(255)]
        public string DVR_ID { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        [StringLength(255)]
        public string DVR_SN { get; set; }
        /// <summary>
        /// DVR类型
        /// </summary>
        [StringLength(255)]
        public string DVR_type { get; set; }
       
        /// <summary>
        /// 硬盘总量
        /// </summary>
        public int? DiskTotal { get; set; }
        /// <summary>
        /// 硬盘信息？josn类型
        /// </summary>
        public string DVRDISK { get; set; }
        /// <summary>
        /// DVR通道位置信息?josn类型
        /// </summary>
        public string DVRChannelInfo { get; set; }
        /// <summary>
        /// dvr时间
        /// </summary>
        public string DVRTime { get; set; }

        /// <summary>
        /// 主机在线状态
        /// </summary>
        public bool? DVR_Online { get; set; }
        /// <summary>
        /// 时间信息检查
        /// </summary>
        public bool? TimeInfoChenk { get; set; }

        /// <summary>
        /// 硬盘信息检查
        /// </summary>
        public bool? DiskChenk { get; set; }

        /// <summary>
        /// SN信息检查
        /// </summary>
        public bool? SNChenk { get; set; }
        /// <summary>
        /// 90天存储检查
        /// </summary>
        public bool? VideoCheck90Day { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }


    }




    }
