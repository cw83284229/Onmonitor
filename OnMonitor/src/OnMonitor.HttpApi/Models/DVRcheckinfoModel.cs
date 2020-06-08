using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnMonitor.Models
{
   public class DVRcheckinfoModel
    {


        /// <summary>
        /// 主机号
        /// </summary>
        public string DVR_ID { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string DVR_SN { get; set; }
        /// <summary>
        /// 数据库序列号
        /// </summary>
        public string DataDVR_SN { get; set; }
        /// <summary>
        /// DVR类型
        /// </summary>
        public string DVR_type { get; set; }
        /// <summary>
        /// DVR 通道数量
        /// </summary>
        public int? DVR_ChannelTotal { get; set; }
        /// <summary>
        /// 数据库通道数量
        /// </summary>
        public int? DataChannelTotal { get; set; }
        /// <summary>
        /// 硬盘总量
        /// </summary>
        public int? DiskTotal { get; set; }
        /// <summary>
        /// 数据库硬盘总量
        /// </summary>
        public int? DataDiskTotal { get; set; }
        /// <summary>
        /// 硬盘信息
        /// </summary>
        public List<DVRDisk> DVRDISK { get; set; }
        /// <summary>
        /// DVR通道位置信息?josn类型
        /// </summary>
        public List<DVRChannelInfoModel>  DVRChannelInfo { get; set; }
        /// <summary>
        /// dvr时间
        /// </summary>
        public string DVRTime { get; set; }
        /// <summary>
        ///系统时间
        /// </summary>
        public string SystemTime { get; set; }

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
        /// 备注
        /// </summary>
        public string Remark { get; set; }



    }
}
