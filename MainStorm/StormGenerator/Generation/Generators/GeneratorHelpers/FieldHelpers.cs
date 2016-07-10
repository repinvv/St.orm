namespace StormGenerator.Generation.Generators.GeneratorHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.GenModels;

    internal static class FieldHelpers
    {
        public static string TypeDefault(this Field field)
        {
            return field.Column.CsType == typeof(string)
                ? "string.Empty"
                : $"default({field.Column.CsTypeName})";
        }

        public static string GetReaderMethod(this Field field, int index)
        {
            if (field.Column.IsNullable)
            {
                return $"reader.IsDBNull({index}) ? {field.GetTypeCast()}null : "
                       + field.GetReaderMethodNullable(index);
            }

            return GetReaderMethodNonNullable(field.Column.CsType, index);
        }

        private static string GetTypeCast(this Field field)
        {
            if (field.Column.CsType == typeof(string) || field.Column.CsType == typeof(byte[]))
            {
                return string.Empty;
            }

            return $"({field.Column.CsTypeName})";
        }

        private static string GetReaderMethodNullable(this Field field, int index)
        {
            if (field.Column.CsType == typeof(string) || field.Column.CsType == typeof(byte[]))
            {
                return GetReaderMethodNonNullable(field.Column.CsType, index);
            }

            var type = field.Column.CsType.GetGenericArguments().First();
            return GetReaderMethodNonNullable(type, index);
        }

        private static readonly Dictionary<Type, string> ReaderMethods =
            new Dictionary<Type, string>
            {
                { typeof(long), "GetInt64" },
                { typeof(int), "GetInt32" },
                { typeof(decimal), "GetDecimal" },
                { typeof(bool), "GetBoolean" },
                { typeof(short), "GetInt16" },
                { typeof(byte), "GetByte" },
                { typeof(Guid), "GetGuid" },
                { typeof(float), "GetFloat" },
                { typeof(double), "GetDouble" },
                { typeof(DateTime), "GetDateTime" },
                { typeof(TimeSpan), "GetTimeSpan" },
                { typeof(DateTimeOffset), "GetDateTimeOffset" },
                { typeof(string), "GetString" },
                { typeof(byte[]), "ReadBytes" }
            };

        private static string GetReaderMethodNonNullable(Type type, int index)
        {
            return  $"reader.{ReaderMethods[type]}({index})";
        }

        public static IEnumerable<string> GetSelectLines(this IEnumerable<Field> fields, string prefix = "")
        {
            var groups = fields.SplitInGroupsBy(8)
                              .ToList();
            foreach (var g in groups)
            {
                var join = string.Join(", ", g.Select(x => prefix + x.Column.Name));
                yield return g == groups.Last() ? @join : @join + ",";
            }
        }

        public static IEnumerable<string> GetArgLines(this IEnumerable<Field> fields)
        {
            var groups = fields.SplitInGroupsBy(8).ToList();
            int i = 0;
            foreach (var g in groups)
            {
                var start = g == groups.First() ? "(" : "";
                var end = g == groups.Last() ? ")" : ",";
                var join = string.Join(", ", g.Select(x => "@parm"+ i++ +"i\" + i + \""));
                yield return "sb.AppendLine(\"" + start + join + end + "\");";

            }
        }

        private static readonly Dictionary<string, string> SqlTypesMap 
            = new Dictionary<string, string>
              {
                { "bigint", "BigInt" },
                { "int", "Int" },
                { "numeric", "Decimal" },
                { "bit", "Bit" },
                { "smallint", "SmallInt" },
                { "decimal", "Decimal" },
                { "smallmoney", "SmallMoney" },
                { "tinyint", "TinyInt" },
                { "money", "Money" },
                { "uniqueidentifier", "UniqueIdentifier" },
                { "float", "Float" },
                { "real", "Real" },
                { "date", "Date" },
                { "time", "Time" },
                { "datetimeoffset", "DateTimeOffset" },
                { "datetime", "DateTime" },
                { "datetime2", "DateTime2" },
                { "smalldatetime", "SmallDateTime" },
                { "char", "Char" },
                { "varchar", "VarChar" },
                { "text", "Text" },
                { "nchar", "NChar" },
                { "nvarchar","NVarChar" },
                { "ntext", "NText" },
                { "xml", "Xml" },
                { "binary", "Binary" },
                { "varbinary", "VarBinary" },
                { "image", "Image" },
            };

        public static string GetSqlType(this Field field)
        {
            return SqlTypesMap[field.Column.DbType];
        }
    }
}
