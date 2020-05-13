using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnMonitor.Monitor
{
  public  class UpdateDVRDto
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
        /// 楼层
        /// </summary>
        [StringLength(255)]
        public string Camera_foor { get; set; }
        /// <summary>
        /// 主机号
        /// </summary>
        [StringLength(255)]
        public string DVR_ID { get; set; }
        /// <summary>
        /// 归属服务器
        /// </summary>
        [StringLength(255)]
        public string Home_server { get; set; }
        /// <summary>
        /// 硬盘容量
        /// </summary>
        public int? Hard_drive { get; set; }
        /// <summary>
        /// 主机IP
        /// </summary>
        [StringLength(255)]
        public string DVR_IP { get; set; }
        /// <summary>
        /// 主机端口
        /// </summary>
        [StringLength(255)]
        public string DVR_port { get; set; }
        /// <summary>
        /// DVR账户
        /// </summary>
        [StringLength(255)]
        public string DVR_usre { get; set; }
        /// <summary>
        /// DVR密码
        /// </summary>
        [StringLength(255)]
        public string DVR_possword { get; set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        [StringLength(255)]
        public string install_time { get; set; }
        /// <summary>
        /// 安装厂商
        /// </summary>
        [StringLength(255)]
        public string Manufacturer { get; set; }
        /// <summary>
        /// DVR类型
        /// </summary>
        [StringLength(255)]
        public string DVR_type { get; set; }
        /// <summary>
        /// 设备SN
        /// </summary>
        [StringLength(255)]
        public string DVR_SN { get; set; }
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
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }
    }


}

