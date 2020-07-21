using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimilarImages;
using Utility.Common.ImageHelper;
using Volo.Abp;

namespace OnMonitor.Samples
{
    [RemoteService]
    [Route("api/OnMonitor/TEST")]
    public class TESTController : OnMonitorController
    {
        private readonly ISampleAppService _sampleAppService;

        public TESTController(ISampleAppService sampleAppService)
        {
            _sampleAppService = sampleAppService;
        }

        [HttpGet]
        public async Task<string> GetAsync(string d,string df)
        {

            string Imgpath1 = $"D:\\{d}";
            string Imgpath2 = $"D:\\{df}";


            Image image1 = Image.FromFile(Imgpath1);
            Image image2 = Image.FromFile(Imgpath2);
            ImageHelp2 imageHelp = new ImageHelp2();
          
           Bitmap bitmap1= imageHelp.Resize(image1);
           Bitmap bitmap2 = imageHelp.Resize(image2);
           var reqst=  ImageHash.GetSimilarity(bitmap1, bitmap2,ImageHash.HashEnum.Mean);
        // var reqst=   imageHelp.GetSimilarity(bitmap1, bitmap2);

            return reqst.ToString();






        }

        [HttpGet]
        [Route("authorized")]
        [Authorize]
        public async Task<SampleDto> GetAuthorizedAsync()
        {
            return await _sampleAppService.GetAsync();
        }
    }
}
