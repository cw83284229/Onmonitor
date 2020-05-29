using Volo.Abp.Reflection;

namespace OnMonitor.Authorization
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