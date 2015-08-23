namespace StormGenerator.Models.Config.Db
{
    using System.Collections.Generic;

    public class DbModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Schema { get; set; }

        public bool IsView { get; set; }

        public string Sequence { get; set; }

        public List<DbField> Fields { get; set; }
    }
}
