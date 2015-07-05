namespace StormGenerator.Generation.RepositoryGeneration.Common
{
    using System;
    using System.Collections.Generic;
    using StormGenerator.Generation.Common;

    internal class MaterializerLineGenerator
    {
        private static readonly Dictionary<Type, string> Getters =
            new Dictionary<Type, string>
            {
                { typeof(byte[]), "GetBytes" },
                { typeof(char), "GetChar" },
                { typeof(byte), "GetByte" },
                { typeof(short), "GetInt16" },
                { typeof(int), "GetInt32" },
                { typeof(long), "GetInt64" },
                { typeof(decimal), "GetDecimal" },
                { typeof(float), "GetFloat" },
                { typeof(double), "GetDouble" },
                { typeof(DateTime), "GetDateTime" },
                { typeof(bool), "GetBoolean" },
                { typeof(Guid), "GetGuid" }
            };

        private readonly TypeService typeService;

        public MaterializerLineGenerator(TypeService typeService)
        {
            this.typeService = typeService;
        }

        public string GenerateMaterializerLine(string readerName, Type type, int index)
        {
            if (typeService.IsNullable(type) || type == typeof(string) || type == typeof(byte[]))
            {
                return readerName + "[" + index + "] as " + typeService.GetTypeName(type);
            }

            if (Getters.ContainsKey(type))
            {
                return readerName + "." + Getters[type] + "(" + index + ")";
            }

            return "(" + typeService.GetTypeName(type) + ")input[" + index + "]";
        }
    }
}
