namespace StormGenerator.Generation.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class TypeService
    {
        private static readonly Dictionary<Type, string> TypeAliases = new Dictionary<Type, string>
                                                                       {
                                                                           { typeof(char), "char" },
                                                                           { typeof(byte[]), "byte[]" },
                                                                           { typeof(byte), "byte" },
                                                                           { typeof(long), "long" },
                                                                           { typeof(int), "int" },
                                                                           { typeof(decimal), "decimal" },
                                                                           { typeof(string), "string" },
                                                                           { typeof(short), "short" },
                                                                           { typeof(bool), "bool" }
                                                                       };

        public string GetTypeName(Type type)
        {
            return IsNullable(type) ? (GetTypeAliasName(GenArgument(type)) + "?") : GetTypeAliasName(type);
        }

        public bool IsNullable(Type type)
        {
            return IsDefinedGeneric(type, typeof(Nullable<>));
        }

        public bool CanBeNull(Type type)
        {
            return IsNullable(type) || type == typeof(string) || type == typeof(byte[]);
        }

        private Type GenArgument(Type type)
        {
            return type.GetGenericArguments().First();
        }

        private string GetTypeAliasName(Type type)
        {
            return TypeAliases.ContainsKey(type) ? TypeAliases[type] : type.Name;
        }

        private bool IsDefinedGeneric(Type type, Type genType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == genType;
        }
    }
}
