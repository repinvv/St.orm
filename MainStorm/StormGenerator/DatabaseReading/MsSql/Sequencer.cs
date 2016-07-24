namespace StormGenerator.DatabaseReading.MsSql
{
    using System.Text.RegularExpressions;

    internal class Sequencer
    {
        private readonly Regex regex = new Regex(@"NEXT VALUE FOR \[(.*)\]");

        public string GetSequence(string defaultValue)
        {
            var match = regex.Match(defaultValue ?? string.Empty);
            return match.Success ? Clean(match.Groups[1].Value) : null;
        }

        private string Clean(string value)
        {
            return value.Replace("[", "").Replace("]", "");
        }
    }
}
