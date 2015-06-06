namespace StormGenerator
{
    public class Options
    {
        public string OutputNamespace { get; set; }

        public string ContextName { get; set; }

        public string SettingsFile { get; set; }

        public bool ForceLoadDbInfo { get; set; }

        public string ConnectionString { get; set; }

        public string Server { get; set; }

        public string Database { get; set; }

        public bool IntegratedSecurity { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
