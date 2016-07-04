namespace StormGenerator.DatabaseReading.MsSql
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using StormGenerator.Models.DbModels;

    internal class Sequencer
    {
        private readonly Regex regex = new Regex(@"NEXT VALUE FOR \[(.*)\]");

        public string GetSequence(string defaultValue)
        {
            var match = regex.Match(defaultValue ?? string.Empty);
            return match.Success ? match.Groups[1].Value : null;
        }
    }
}
