namespace OnMonitor
{
    public static class OnMonitorDbProperties
    {
        public static string DbTablePrefix { get; set; } = "App";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "OnMonitor";
    }
}
