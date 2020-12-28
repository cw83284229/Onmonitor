using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;


namespace OnMonitor.Monitor.Alarm
{
    
    public partial class AlarmHostDto : EntityDto<Int32>
    {
        /// <summary>
        /// 监控室
        /// </summary>
        [StringLength(255)]
        public string Monitoring_room { get; set; }
        /// <summary>
        /// 报警主机号
        /// </summary>
        [StringLength(255)]
        public string AlarmHost_ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(255)]
        public string User { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(255)]
        public string Password { get; set; }
        /// <summary>
        /// 报警主机类型
        /// </summary>
        [StringLength(255)]
        public string AlarmHostType { get; set; }
        /// <summary>
        /// 主机IP
        /// </summary>
        [StringLength(255)]
        public string AlarmHostIP { get; set; }
        /// <summary>
        /// 通道数量
        /// </summary>
        [StringLength(255)]
        public int? AlarmChannelCount { get; set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        [StringLength(255)]
        public string install_time { get; set; }
        /// <summary>
        /// 安装厂商
        /// </summary>
        [StringLength(255)]
        public string category { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }

    }
}
