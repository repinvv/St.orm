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

        public static string GetGetErMethod(this Field field, int index)
        {
            if (field.Column.IsNullable)
            {
                return $"GetEr.IsDBNull({index}) ? {field.GetTypeCast()}null : "
                       + field.GetGetErMethodNullable(index);
            }

            return GetGetErMethodNonNullable(field.Column.CsType, index);
        }

        private static string GetTypeCast(this Field field)
        {
            if (field.Column.CsType == typeof(string) || field.Column.CsType == typeof(byte[]))
            {
                return string.Empty;
            }

            return $"({field.Column.CsTypeName})";
        }

        private static string GetGetErMethodNullable(this Field field, int index)
        {
            if (field.Column.CsType == typeof(string) || field.Column.CsType == typeof(byte[]))
            {
                return GetGetErMethodNonNullable(field.Column.CsType, index);
            }

            var type = field.Column.CsType.GetGenericArguments().First();
            return GetGetErMethodNonNullable(type, index);
        }

        private static readonly Dictionary<Type, string> GetErMethods =
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

        private static string GetGetErMethodNonNullable(Type type, int index)
        {
            return  $"GetEr.{GetErMethods[type]}({index})";
        }
    }
}
