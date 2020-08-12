using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace OnMonitor.BLOB
{
   public class BOLBService:  ApplicationService, ITransientDependency
    {

        private readonly IBlobContainer _blobContainer;

        public BOLBService(IBlobContainer blobContainer)
        {
            _blobContainer = blobContainer;
        }

        public async Task SaveBytesAsync(string fileName, byte[] bytes)
        {
            await _blobContainer.SaveAsync(fileName, bytes);
        }

        public async Task<byte[]> GetBytesAsync(string fileName)
        {
            return await _blobContainer.GetAllBytesOrNullAsync(fileName);
        }


        public async Task<string> uploact(IFormFile layoutfile)
        {


            await _blobContainer.SaveAsync(layoutfile.FileName, layoutfile.OpenReadStream());

            return "OK";
        }



    }
}
