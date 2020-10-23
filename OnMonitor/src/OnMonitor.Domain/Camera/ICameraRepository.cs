using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor
{
   public interface ICameraRepository:IRepository<Camera,int>
    {

        public Task BulkInsertAsync(IEnumerable<Camera> cameras);



    }
}
