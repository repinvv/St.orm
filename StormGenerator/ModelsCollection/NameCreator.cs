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
            return char.ToUpper(arg[0]) + arg.Substring(1).ToLower();
        }

        public string CreatePluralName(string name)
        {
            return name.Last() == 's' ? name : name + "s";
        }
    }
}
