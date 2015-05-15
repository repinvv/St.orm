namespace StormGenerator.DbModelCollection
{
    using System.Linq;
    using StormGenerator.Model.Db;

    internal class AssociationTypeUpdater
    {
        public void UpdateAssociationTypes(DbModel model)
        {
            foreach (var association in model.Associations.Where(x => x.Dependent.IsManyToManyLink))
            {
                var keyField = model.Fields.First(x => x.IsPrimaryKey);
                association.ReferenceFields[0].Type = keyField.Type;
            }
        }
    }
}
