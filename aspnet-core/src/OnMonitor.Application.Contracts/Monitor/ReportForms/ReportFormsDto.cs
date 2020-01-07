using System;

namespace OnMonitor.Monitor
{
    public class ReportFormsDto
    {
        /// <summary>
        /// 监控室
        /// </summary>
        public string DVRRoom { get; set; }
        /// <summary>
        /// 主机总数
        /// </summary>
        public int DVRTotal { get; set; }
        /// <summary>
        /// 在线主机
        /// </summary>
        public int DVROnLine { get; set; }

        /// <summary>
        /// 异常主机
        /// </summary>
        public int DVRAnomaly { get; set; }
        /// <summary>
        /// 镜头总数
        /// </summary>
        public int CameraTotal { get; set; }
        /// <summary>
        /// 维修总数
        /// </summary>
        public int RepairTotal { get; set; }
        /// <summary>
        /// 异常总数
        /// </summary>
        public int CameraAnomaly { get; set; }
    }
}