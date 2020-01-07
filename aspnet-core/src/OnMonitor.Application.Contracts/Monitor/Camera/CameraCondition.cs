using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Monitor
{
    public class CameraCondition
    {

        /// <summary>
        /// 主机号
        /// </summary>

        [StringLength(255)]

        public string DVR_ID { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
  
        public int channel_ID { get; set; }

        /// <summary>
        /// 镜头号
        /// </summary>

        [StringLength(255)]
        public string Camera_ID { get; set; }
        /// <summary>
        /// 楼栋
        /// </summary>
        [StringLength(255)]

        public string Build { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        [StringLength(255)]

        public string floor { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        [StringLength(255)]

        public string Direction { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [StringLength(255)]

        public string Location { get; set; }
        /// <summary>
        /// 监控分类
        /// </summary>
        [StringLength(255)]
        public string MonitorClassification { get; set; }
        /// <summary>
        /// 镜头类型
        /// </summary>
        [StringLength(255)]

        public string Camera_Tpye { get; set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        [StringLength(255)]

        public string install_time { get; set; }


    }

}

