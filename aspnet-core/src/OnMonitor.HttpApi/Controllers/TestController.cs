using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnMonitor.Models.Test;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnMonitor.Controllers
{
    [Route("api/test")]
    public class TestController : OnMonitorController
    {
        IHostEnvironment _hostEnvironment ;
        public TestController(IHostEnvironment hostEnvironment)
        {
           _hostEnvironment= hostEnvironment ;
        }

        [HttpGet]
        [Route("")]
        public async Task<List<TestModel>> GetAsync()
        {



           var DA= _hostEnvironment.ContentRootPath;
            var DD = _hostEnvironment.ContentRootFileProvider;
            return new List<TestModel>
            {
                new TestModel {Name = "John", BirthDate = new DateTime(1942, 11, 18)},
                new TestModel {Name = "Adams", BirthDate = new DateTime(1997, 05, 24)}
            };

        }
    }
}
