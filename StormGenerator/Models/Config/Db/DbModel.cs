namespace StormGenerator.Models.Config.Db
{
    using System.Collections.Generic;

    internal class DbModel
    {
        public string Name { get; set; }

        public string SchemaName { get; set; }

        public List<DbField> Fields { get; set; }

        public List<DbAssociation> Associations { get; set; }
    }
}
