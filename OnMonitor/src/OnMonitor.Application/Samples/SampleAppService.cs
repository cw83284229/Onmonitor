using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OnMonitor.Monitor;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Samples
{
    
    public class SampleAppService : OnMonitorAppService, ISampleAppService
    {
       // IRepository<DVR, int> _dvrrepository;
        IDVRRepository _dvrrepository;
        public SampleAppService(IDVRRepository dvrrepository) 
        {

            _dvrrepository = dvrrepository;
        }
        public Task<SampleDto> GetAsync()
        {

          var data=  _dvrrepository.GetListAsync(0,20,null,null);
            
            
            return Task.FromResult(
                new SampleDto
                {
                    Value = data.Result.Count
                }
            );
        }
        public Task<SampleDto> GetlistAsync()
        {

            var data = _dvrrepository.GetListAsync();


            return Task.FromResult(
                new SampleDto
                {
                    Value = data.Result.Count
                }
            );
        }
        [RemoteService(IsEnabled = false)]
        public Task<SampleDto> GetAuthorizedAsync()
        {
            return Task.FromResult(
                new SampleDto
                {
                    Value = 42
                }
            );
        }
    }
}