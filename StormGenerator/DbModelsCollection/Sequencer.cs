namespace StormGenerator.DbModelsCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using StormGenerator.Models.Config.Db;

    internal class Sequencer
    {
        private readonly Regex regex = new Regex(@"NEXT VALUE FOR \[(.*)\]");

        public void MarkSequences(List<DbModel> models)
        {
            foreach (var model in models.Where(x => x.Fields.Count(y => y.IsPrimaryKey) == 1))
            {
                var field = model.Fields.First(x => x.IsPrimaryKey);
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
