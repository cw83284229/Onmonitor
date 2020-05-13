using System;
using System.Collections.Generic;

namespace OnMonitor.Monitor
{
    public class DVRInfoDto
    {

        /// <summary>
        /// 主机号
        /// </summary>
        public String DVR_IP { get; set; }
        /// <summary>
        /// 主机序列号
        /// </summary>
        public String DVR_SN { get; set; }
        /// <summary>
        /// 主机时间
        /// </summary>
        public String DVR_DateTine { get; set; }
        /// <summary>
        /// 硬盘容量
        /// </summary>
        public int HardDrive { get; set; }
        /// <summary>
        /// 通道数量
        /// </summary>
        public int ChannelTotal { get; set; }

        /// <summary>
        /// 硬盘清单
        /// </summary>
        public List<DVRDisk> DVRDisk { get; set; }

        /// <summary>
        /// 通道清单
        /// </summary>
        public List<Channelname> Channelname { get; set; }
        /// <summary>
        /// 编码格式
        /// </summary>
        public string Encode { get; set; }

    }

    public class DVRDisk

    {
        public int Number { get; set; }
        public long Disk { get; set; }
    }
    public class Channelname

    {
        public int Number { get; set; }
        public string Name { get; set; }
    }

}