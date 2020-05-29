using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.PermissionManagement;

namespace OnMonitor
{
    public class UrseJurisdictionService : ApplicationService,ITransientDependency,IUrseJurisdictionService
    {

        private readonly IPermissionManager _permissionManager;


        public UrseJurisdictionService(IPermissionManager permissionManager)
        {

            _permissionManager = permissionManager;
        
        }
        /// <summary>
        /// 给特定用户设定权限->权限名称1.查看监控"CCTV_VideoViewing"2.修改资料库资料"CCTV_Modification"
        /// </summary>
        /// <param name="userId">用户GUID</param>
        /// <param name="permissionName">权限名称</param>
        /// <returns></returns>
        public async Task GramtPermissionForUserAsync(Guid userId,string permissionName)
        {
            await _permissionManager.SetForUserAsync(userId, permissionName, true);

        }
        /// <summary>
        /// 取消用户特定权限
        /// </summary>
        /// <param name="userId">用户GUID</param>
        /// <param name="permissionName">权限名称</param>
        /// <returns></returns>
        public async Task ProhibitPermissionForUserAsync(Guid userId, string permissionName)
        {
            await _permissionManager.SetForUserAsync(userId, permissionName, false);
        }

        /// <summary>
        /// 给角色设定权限
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="permissionName">权限名称</param>
        /// <returns></returns>
        public async Task GramtPermissionForRoleAsync(string roleName, string permissionName)
        {
            await _permissionManager.SetForRoleAsync(roleName, permissionName,true);

        }
        /// <summary>
        /// 取消角色特定权限
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="permissionName">权限名称</param>
        /// <returns></returns>
        public async Task ProhibitPermissionForRoleAsync(string roleName, string permissionName)
        {
            await _permissionManager.SetForRoleAsync(roleName,permissionName,false);
        }

    }
}
