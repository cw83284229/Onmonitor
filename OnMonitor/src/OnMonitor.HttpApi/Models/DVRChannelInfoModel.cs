using System;
using System.Collections.Generic;
using System.Text;

namespace OnMonitor.Models
{
   public class DVRChannelInfoModel
    {
       public int Channelid { get; set; }

       public string DataChannelName { get; set;}
        
       public string DVRChannelName { get; set; }

       public bool? ChannelNameCheck { get; set; }

    }
}
