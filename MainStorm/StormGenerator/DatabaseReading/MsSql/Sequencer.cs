namespace StormGenerator.DatabaseReading.MsSql
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using StormGenerator.Models.DbModels;

    internal class Sequencer
    {
        private readonly Regex regex = new Regex(@"NEXT VALUE FOR \[(.*)\]");

        public void MarkSequences(List<Table> models)
        {
            foreach (var model in models.Where(x => x.Columns.Count(y => y.IsPrimaryKey) == 1))
            {
                var field = model.Columns.First(x => x.IsPrimaryKey);
                if (field.IsIdentity)
                {
                    continue;
                }

                var match = regex.Match(field.Default ?? string.Empty);
                model.Sequence = match.Success ? match.Groups[1].Value : null;
            }
        }
    }
}
