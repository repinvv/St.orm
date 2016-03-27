namespace StormGenerator.Settings
{
    internal class DbConnectionInfo
    {
        public DbType DbType { get; set; }

        public string Server { get; set; }

        public string Database { get; set; }

        public bool IntegratedSecurity { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
