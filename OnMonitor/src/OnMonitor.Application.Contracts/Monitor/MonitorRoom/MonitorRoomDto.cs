using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Monitor
{
    public class MonitorRoomDto:EntityDto<int>
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
