namespace StormGenerator.Model
{
    using StormGenerator.Model.Db;

    internal class Model
    {
        public string Name { get; set; }

        public DbModel DbModel { get; set; }
    }
}
