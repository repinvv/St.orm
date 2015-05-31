namespace StormGenerator.ModelsCollection
{
    using System;
    using System.Linq;

    internal class NameCreator
    {
        private static readonly string[] Separators = { "_", "-" };

        public string CreateCamelCaseName(string source)
        {
            return string.Join(string.Empty, source.Split(Separators, StringSplitOptions.RemoveEmptyEntries).Select(ConvertSection));
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
            return name.Last() == 's' ? name : name + "s";
        }
    }
}
