using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace OnMonitor.Monitor
{
   public class MonitorRoom:Entity<int>
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
        public string RoomLocation { get; set; }
        /// <summary>
        /// 监控室类别
        /// </summary>
        [StringLength(255)]
        public string RoomType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }




    }
}
