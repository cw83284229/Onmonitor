using System;
using System.Collections.Generic;
using System.Text;

namespace OnMonitor.Monitor
{
  public  class DVRCameraDto
    {


        /// <summary>
        /// 厂区
        /// </summary>
       
        public string Factory { get; set; }
        /// <summary>
        /// 监控室
        /// </summary>
       
        public string Monitoring_room { get; set; }
        /// <summary>
        /// 主机号
        /// </summary>

        public string DVR_ID { get; set; }
    
        /// <summary>
        /// 楼栋
        /// </summary>
     
        public string Build { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
 
        public string floor { get; set; }


        /// <summary>
        /// 镜头号
        /// </summary>
        public string CameraID { get; set; }

    }
}
