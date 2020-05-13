using Microsoft.AspNetCore.Mvc;
using OnMonitor.Monitor;

namespace OnMonitor.Controllers
{
    [Route("api/ExcelInDB")]
    public class ExcelInDBController : OnMonitorController
    {

        public ICameraAppService _cameraAppService;
        public IDVRAppService _dVRAppService;
      


        public ExcelInDBController(ICameraAppService cameraAppService, IDVRAppService dVRAppService)
        {
            _cameraAppService = cameraAppService;
            _dVRAppService = dVRAppService;
           
        }

       
    }
}
