using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTV_Client.DaHua.Models
{
    public class DiskInfo
    {
        /// <summary>
        /// 硬盘编号
        /// </summary>
        public string DiskNumber { get; set; }
        /// <summary>
        /// 空闲容量
        /// </summary>
        public string FreeSpace { get; set; }
        /// <summary>
        /// 总容量
        /// </summary>
        public string TotalSpace { get; set; }
        /// <summary>
        /// 硬盘类型
        /// </summary>
        public string DiskType { get; set; }

        /// <summary>
        /// 硬盘状态
        /// </summary>
        public string DiskStatus { get; set; }
        /// <summary>
        /// 分区号
        /// </summary>
        public string SubareaNumber { get; set; }

        /// <summary>
        /// 远程标示
        /// </summary>
        public string Signal { get; set; }
    }

    }

  
