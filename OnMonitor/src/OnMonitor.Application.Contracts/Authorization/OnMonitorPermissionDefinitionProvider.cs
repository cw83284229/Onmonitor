using OnMonitor.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OnMonitor.Authorization
{
    public class OnMonitorPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var moduleGroup = context.AddGroup(OnMonitorPermissions.GroupName, L("Permission:OnMonitor"));
            var myGroup = context.AddGroup(OnMonitorPermissions.GroupName);

            myGroup.AddPermission("DVR_Check");
            myGroup.AddPermission("CCTV_Modification");
            myGroup.AddPermission("CCTV_VideoViewing");
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<OnMonitorResource>(name);
        }
    }
}