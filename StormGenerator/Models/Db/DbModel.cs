namespace StormGenerator.Models.Db
{
    using System.Collections.Generic;

    public class DbModel
    {
        public bool IsManyToManyLink { get; set; }

        public string Name { get; set; }

        public string SchemaName { get; set; }

        public List<DbField> Fields { get; set; }

        public List<DbAssociation> Associations { get; set; }
    }
}
