namespace StormGenerator.ModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class NameCreator
    {
        private readonly string[] separators = { "_", "-" };
        private readonly List<char> addEs = new List<char> { 'h', 'x', };

        public string CreateCamelCaseName(string source)
        {
            return string.Join(string.Empty, source.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(ConvertSection));
        }

        private string ConvertSection(string arg)
        {
            return char.ToUpper(arg[0]) + ConvertRestOfString(arg.Substring(1));
        }

        private string ConvertRestOfString(string arg)
        {
            return arg.All(char.IsUpper) || arg.All(char.IsLower) ? arg.ToLower() : arg;
        }

        public string CreatePluralName(string name)
        {
            return name.Last() == 's'
                ? name
                : addEs.Contains(name.Last())
                    ? name + "es"
                    : name + "s";
        }
    }
}
