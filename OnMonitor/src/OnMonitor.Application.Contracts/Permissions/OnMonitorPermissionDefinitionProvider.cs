using OnMonitor.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OnMonitor.Permissions
{
    public class OnMonitorPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(OnMonitorPermissions.GroupName, L("Permission:OnMonitor"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<OnMonitorResource>(name);
        }
    }
}