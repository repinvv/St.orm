namespace StormGenerator.Models.Config.Db
{
    using System.Collections.Generic;

    public class DbField
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsIdentity { get; set; }

        public int StringLength { get; set; }

        public int Index { get; set; }

        public bool IsReadonly { get; set; }

        public List<DbAssociation> Associations { get; set; }
    }
}
