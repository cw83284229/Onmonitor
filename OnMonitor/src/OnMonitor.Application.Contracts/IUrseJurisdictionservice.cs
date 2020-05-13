using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OnMonitor
{
   public interface IUrseJurisdictionService : IApplicationService
    {

        public Task GramtPermissionForUserAsync(Guid userId, string permissionName);


        public Task ProhibitPermissionForUserAsync(Guid userId, string permissionName);

        public Task GramtPermissionForRoleAsync(string roleName, string permissionName);
        public Task ProhibitPermissionForRoleAsync(string roleName, string permissionName);

    }
}
