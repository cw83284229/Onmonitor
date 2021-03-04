using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace OnMonitor.Monitor
{
   public class DVRChannelInfo : Entity<int>
    {
        /// <summary>
        /// 主机号
        /// </summary>
        public string DVR_ID { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
        public int channel_ID { get; set; }

        /// <summary>
        /// 镜头号
        /// </summary>
        public string Camera_ID { get; set; }
        /// <summary>
        /// 数据库镜头名称
        /// </summary>
        public string DataChannelName { get; set;}
        /// <summary>
        /// DVR通道显示名称
        /// </summary>
        public string DVRChannelName { get; set; }
        /// <summary>
        /// 名称检查
        /// </summary>
        public bool? ChannelNameCheck { get; set; }
        /// <summary>
        /// 图像检查
        /// </summary>
        public bool? ImageCheck { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string LastUpdateTime { get; set; }
        /// <summary>
        /// 镜头编码
        /// </summary>
        public string DVRChannelEncoding { get; set; }
        /// <summary>
        /// 镜头类型
        /// </summary>
        public string CameraType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
