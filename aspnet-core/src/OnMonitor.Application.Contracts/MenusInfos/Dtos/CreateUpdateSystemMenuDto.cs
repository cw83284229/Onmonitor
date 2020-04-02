using System;
using System.ComponentModel;
namespace OnMonitor.MenusInfos.Dtos
{
    public class CreateUpdateSystemMenuDto
    {
        [DisplayName("SystemMenupid")]
        public long pid { get; set; }

        [DisplayName("SystemMenutitle")]
        public string title { get; set; }

        [DisplayName("SystemMenuicon")]
        public string icon { get; set; }

        [DisplayName("SystemMenuhref")]
        public string href { get; set; }

        [DisplayName("SystemMenutarget")]
        public string target { get; set; }

        [DisplayName("SystemMenusort")]
        public int sort { get; set; }

        [DisplayName("SystemMenustatus")]
        public bool status { get; set; }
    }
}