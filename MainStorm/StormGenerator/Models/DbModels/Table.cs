namespace StormGenerator.Models.DbModels
{
    using System.Collections.Generic;

    internal class Table
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Schema { get; set; }

        public bool IsView { get; set; }

        public List<Column> Columns { get; set; }
    }
}
