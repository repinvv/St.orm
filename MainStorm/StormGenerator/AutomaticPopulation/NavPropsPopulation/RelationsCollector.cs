namespace StormGenerator.AutomaticPopulation.NavPropsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.DbModels;

    internal class RelationsCollector
    {
        public List<Relation> CollectRelations(Table table)
        {
            var relations = from column in table.Columns
                            from association in column.Associations
                            select new Relation
                            {
                                RefTableId = association.TableId,
                                RefColumn = association.FieldName,
                                Column = column.Name,
                                Id = association.ConstraintId,
                                Index = association.Index,
                                Cascade = association.Cascade
                            };
            return relations.ToList();
        }
    }
}
