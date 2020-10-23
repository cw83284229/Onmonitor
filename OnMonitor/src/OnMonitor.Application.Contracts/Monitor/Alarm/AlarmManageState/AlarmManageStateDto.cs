using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Monitor
{
    public  class AlarmManageStateDto: AuditedEntityDto<Int32>
    {

        /// <summary>
        /// 报警编号
        /// </summary>
        [StringLength(255)]
        public string Alarm_ID { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        [StringLength(255)]
        public string AlarmTime { get; set; }
        /// <summary>
        /// 撤防时间
        /// </summary>
        [StringLength(255)]
        public string WithdrawTime { get; set; }
        /// <summary>
        /// 撤防人员
        /// </summary>
        [StringLength(255)]
        public string WithdrawMan { get; set; }
        /// <summary>
        /// 撤防原因
        /// </summary>
        [StringLength(255)]
        public string WithdrawRemark { get; set; }
        /// <summary>
        /// 布防时间
        /// </summary>
        [StringLength(255)]
        public string DefenceTime { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string TreatmentTime { get; set; }
        /// <summary>
        /// 处理状态时间
        /// </summary>
        public string TreatmentTimeState { get; set; }
        /// <summary>
        /// 处理回复
        /// </summary>
        public string TreatmentReply { get; set; }

    }
}
