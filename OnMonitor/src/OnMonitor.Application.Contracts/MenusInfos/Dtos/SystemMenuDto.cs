using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.MenusInfos.Dtos
{
    public class SystemMenuDto : EntityDto<long>
    {
      

        public string title { get; set; }

        public string icon { get; set; }

        public string href { get; set; }

        public string target { get; set; }

        public int sort { get; set; }

        public List<SystemMenuDto> Child { get; set; }
    }
}