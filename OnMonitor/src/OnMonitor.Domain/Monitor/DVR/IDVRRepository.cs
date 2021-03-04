using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor
{
   public  interface IDVRRepository : IRepository<DVR, int>
    {

        Task<DVR> FindByNameAsync(string name);

        Task<List<DVR>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
