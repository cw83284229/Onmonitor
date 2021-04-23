namespace EquipmentStatus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppAlarmManageStates
    {
        public int Id { get; set; }

        [StringLength(40)]
        public string ConcurrencyStamp { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public Guid? LastModifierId { get; set; }

        /// <summary>
        /// ��������IP
        /// </summary>
        [StringLength(255)]
        public string AlarmHost_IP { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        [StringLength(255)]
        public string Alarm_ID { get; set; }
        /// <summary>
        /// ͨ�����
        /// </summary>
        public int? Channel_ID { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [StringLength(255)]
        public string AlarmTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [StringLength(255)]
        public string WithdrawTime { get; set; }
        /// <summary>
        /// ������Ա
        /// </summary>
        [StringLength(255)]
        public string WithdrawMan { get; set; }
        /// <summary>
        /// ����ԭ��
        /// </summary>
        [StringLength(255)]
        public string WithdrawRemark { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [StringLength(255)]
        public string DefenceTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string TreatmentTime { get; set; }
        /// <summary>
        /// ����״̬ʱ��
        /// </summary>
        public string TreatmentTimeState { get; set; }
        /// <summary>
        /// �ֳ�������
        /// </summary>
        public string TreatmentMan { get; set; }
        /// <summary>
        /// ����ظ�
        /// </summary>
        public string TreatmentReply { get; set; }
        /// <summary>
        /// �쳣���
        /// </summary>
        public string AnomalyType { get; set; }


        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
    }
}
