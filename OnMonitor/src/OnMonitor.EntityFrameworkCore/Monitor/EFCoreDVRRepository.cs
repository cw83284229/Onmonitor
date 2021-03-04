using Microsoft.EntityFrameworkCore;
using OnMonitor.EntityFrameworkCore;
using OnMonitor.Monitor;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMonitor
{
   public class EFCoreDVRRepository : EfCoreRepository<OnMonitorDbContext, DVR, int>,
            IDVRRepository
    {
        public EFCoreDVRRepository(
            IDbContextProvider<OnMonitorDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<DVR> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(author => author.DVR_ID == name);
        }

        public async Task<List<DVR>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var data =await GetQueryableAsync();
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    author => author.DVR_ID.Contains(filter)
                 )
                //.OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
  
}
