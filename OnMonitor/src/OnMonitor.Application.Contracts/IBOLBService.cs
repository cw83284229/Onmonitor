using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OnMonitor
{
   public interface IBOLBService: IApplicationService
    {
        public Task SaveBytesAsync(string fileName, byte[] bytes);

        public Task<byte[]> GetBytesAsync(string fileName);
       


    }
}
