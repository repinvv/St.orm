namespace StormGenerator.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class NameCreator
    {
        private readonly string[] separators = { "_", "-" };
        private readonly List<char> addEs = new List<char> { 'h', 'x', 's' };
        private readonly List<char> syllables = new List<char> { 'a', 'e', 'i', 'o', 'u', 'y' };

        public string CreateCamelCaseName(string source)
        {
            return string.Join(string.Empty, source.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(ConvertSection));
        }

        public string CreatePluralName(string name)
        {
            if (addEs.Contains(char.ToLower(name.Last())))
            {
                return name + "es";
            }

            if (char.ToLower(name.Last()) == 'a')
            {
                return name + "e";
            }

            if (char.ToLower(name.Last()) == 'y')
            {
                if (name.Length > 1 && !syllables.Contains(char.ToLower(name[name.Length - 2])))
                {
                    return name.Substring(0, name.Length - 1) + "ies";
                }
            }

            return name + "s";
        }

        private string ConvertSection(string arg)
        {
            return char.ToUpper(arg[0]) + ConvertRestOfString(arg.Substring(1));
        }

        private string ConvertRestOfString(string arg)
        {
            return arg.All(char.IsUpper) || arg.All(char.IsLower) ? arg.ToLower() : arg;
        }
    }
}
