namespace StormGenerator.Generation.RepositoryGeneration.Common
{
    using System;
    using System.Linq;
    using StormGenerator.Models.Pregen;

    internal class IdentityFinder
    {
        public void WithIdentity(Model model, Action<MappingField> action)
        {
            if (!model.HasId())
            {
                return;
            }

            action(model.KeyFields().First());
        }
    }
}
