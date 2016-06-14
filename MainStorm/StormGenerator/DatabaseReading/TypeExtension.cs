namespace StormGenerator.DatabaseReading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Storm;

    internal static class TypeExtension
    {
        private static readonly Dictionary<Type, string> TypeAliases
            = new Dictionary<Type, string>
              {
                  { typeof(char), "char" },
                  { typeof(byte[]), "byte[]" },
                  { typeof(byte), "byte" },
                  { typeof(long), "long" },
                  { typeof(int), "int" },
                  { typeof(decimal), "decimal" },
                  { typeof(string), "string" },
                  { typeof(short), "short" },
                  { typeof(bool), "bool" },
                  { typeof(float), "float" },
                  { typeof(double), "double" }
              };

        public static string GetTypeName(this Type type)
        {
            return IsNullable(type)
                ? (GetTypeAliasName(GenArgument(type)) + "?")
                : GetTypeAliasName(type);

        }

        public static bool IsNullable(this Type type)
        {
            return IsDefinedGeneric(type, typeof(Nullable<>));
        }

        private static Type GenArgument(Type type)
        {
            return type.GetGenericArguments().First();
        }

        private static string GetTypeAliasName(Type type)
        {
            return TypeAliases.SafeGet(type, type.Name);
        }

        private static bool IsDefinedGeneric(Type type, Type genType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == genType;
        }
    }
}
