namespace StormGenerator.Generation.RepositoryGeneration.Common
{
    using System;
    using System.Linq;
    using StormGenerator.Models.Pregen;

    internal class IdentityFinder
    {
        public bool HasId(Model model)
        {
            return model.MappingFields.ActiveCount(x => x.DbField.IsPrimaryKey) == 1;
        }

        public void WithIdentity(Model model, Action<MappingField> action)
        {
            if (!HasId(model))
            {
                return;
            }

            action(model.MappingFields.First(x => x.DbField.IsPrimaryKey));
        }
    }
}
