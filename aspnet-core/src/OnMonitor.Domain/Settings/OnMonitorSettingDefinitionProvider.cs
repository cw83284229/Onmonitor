using Volo.Abp.Settings;

namespace OnMonitor.Settings
{
    public class OnMonitorSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(OnMonitorSettings.MySetting1));
        }
    }
}
