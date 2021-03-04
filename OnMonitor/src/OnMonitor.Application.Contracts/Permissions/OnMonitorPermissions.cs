using Volo.Abp.Reflection;

namespace OnMonitor.Permissions
{
    public class OnMonitorPermissions
    {
        public const string GroupName = "OnMonitor";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(OnMonitorPermissions));
        }
    }
}