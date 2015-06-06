namespace StormGenerator.Generation.CommonGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class TypeNameService
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

        private string GetTypeAliasName(Type type)
        {
            return TypeAliases.ContainsKey(type) ? TypeAliases[type] : type.Name;
        }

        public string GetTypeName(Type type)
        {
            return IsNullable(type) ? (GetTypeAliasName(GenArgument(type)) + "?") : GetTypeAliasName(type);
        }

        private bool IsNullable(Type type)
        {
            return IsDefinedGeneric(type, typeof(Nullable<>));
        }

        private bool IsDefinedGeneric(Type type, Type genType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == genType;
        }

        private Type GenArgument(Type type)
        {
            return type.GetGenericArguments().First();
        }
    }
}