using System;
using System.Collections.Generic;
using System.Text;

namespace OnMonitor.Models.SignalRModel
{
   public class ResponseSignalRDto
    {
        /// <summary>
        /// 显示内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 执行按钮
        /// </summary>
        public string Action { get; set; }
  

    }
}
