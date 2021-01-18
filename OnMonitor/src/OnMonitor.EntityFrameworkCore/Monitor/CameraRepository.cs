using OnMonitor.EntityFrameworkCore;
using OnMonitor.MonitorRepair;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMonitor.Monitor
{
    public class CameraRepairRepository : EfCoreRepository<OnMonitorDbContext, CameraRepair, int>,ICameraRepairRepository
    {
        public CameraRepairRepository(IDbContextProvider<OnMonitorDbContext> dbContextProvider) : base(dbContextProvider) 
        {
           
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="cameras"></param>
        /// <returns></returns>
        public async Task BulkInsertAsync(IEnumerable<CameraRepair> cameraRepairs)
        {
           await DbContext.Set<CameraRepair>().AddRangeAsync(cameraRepairs);
           var data=  await DbContext.SaveChangesAsync();
        
        
        }


       

    }
}
