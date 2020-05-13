using System;
using System.IO;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor
{


    public interface IDVRInfoAppService :IApplicationService

    {


        public Stream GetChannelPicture(string CameraID);


    }
      
}
