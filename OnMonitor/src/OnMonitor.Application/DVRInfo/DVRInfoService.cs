using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Service
{
    public class DVRInfoService: ApplicationService, IApplicationService
    {
        readonly IRepository<DVR, Int32> _dvrrepository;
        readonly IRepository<Camera, Int32> _camerarepository;
        public DVRInfoService(IRepository<DVR, Int32> dvrrepository, IRepository<Camera, Int32> camerarepository) 
        {

            _camerarepository = camerarepository;
            _dvrrepository = dvrrepository;
        }


        /// <summary>
        ///获取资料库镜头位置信息
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<Dictionary<int, string>> GetCameraNameAsync (string DVR_ID)
        {

            //   var dvrdata = _dvrrepository.GetList().Where(u => u.DVR_ID == cameradata.DVR_ID).FirstOrDefault();
            var cameradata = _camerarepository.Where(u => u.DVR_ID == DVR_ID);
           
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            int i = 0;
            foreach (var item in cameradata)
            {

                keyValuePairs.Add(i++, $"{item.Camera_ID} {item.Build}-{item.floor} {item.Direction}{item.Location}");
            }

            return keyValuePairs;
            

        }







    }
}
