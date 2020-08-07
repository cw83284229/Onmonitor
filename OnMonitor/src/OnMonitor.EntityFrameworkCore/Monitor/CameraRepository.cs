using OnMonitor.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMonitor.Monitor
{
    public class CameraRepository : EfCoreRepository<OnMonitorDbContext, Camera, int>,ICameraRepository
    {
        public CameraRepository(IDbContextProvider<OnMonitorDbContext> dbContextProvider) : base(dbContextProvider) 
        {
        
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="cameras"></param>
        /// <returns></returns>
        public async Task BulkInsertAsync(IEnumerable<Camera> cameras)
        {
           await DbContext.Set<Camera>().AddRangeAsync(cameras);
           var data=  await DbContext.SaveChangesAsync();
        
        
        }




    }
}
