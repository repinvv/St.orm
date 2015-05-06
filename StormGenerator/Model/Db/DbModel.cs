namespace StormGenerator.Model.Db
{
    public class DbModel
    {
        public string Name { get; set; }

        public string SchemaName { get; set; }

        public DbField[] Fields { get; set; }
    }
}
