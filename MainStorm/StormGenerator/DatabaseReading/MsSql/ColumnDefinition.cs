namespace StormGenerator.DatabaseReading.MsSql
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal class ColumnDefinition
    {
        string[] precisionTypes = { "decimal", "numeric" };
        string[] lengthTypes = {
                                   "char",
                                   "varchar",
                                   "text",
                                   "nchar",
                                   "nvarchar",
                                   "ntext",
                                   "xml",
                                   "binary",
                                   "varbinary",
                                   "image"
                               };

        public string GetColumnDefinition(DbColumn column)
        {
            if (lengthTypes.Contains(column.Type))
            {
                return $"{column.Name} {column.Type}{GetColumnLength(column)}";
            }
            if (precisionTypes.Contains(column.Type))
            {
                return $"{column.Name} {column.Type}{GetColumnPrecision(column)}";
            }

            return $"{column.Name} {column.Type}";
        }

        private string GetColumnPrecision(DbColumn column)
        {
            if (column.Precision > 0 && column.Scale > 0)
            {
                return $"({column.Precision}, {column.Scale})";
            }

            if (column.Precision > 0)
            {
                return $"({column.Precision})";
            }

            return string.Empty;
        }

        private string GetColumnLength(DbColumn column)
        {
            switch (column.Length)
            {
                case -1:
                    return "(max)";
                case 0:
                    return string.Empty;
                default:
                    return $"({column.Length})";
            }
        }
    }
}
