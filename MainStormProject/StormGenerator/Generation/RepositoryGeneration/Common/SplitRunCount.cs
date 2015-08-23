namespace StormGenerator.Generation.RepositoryGeneration.Common
{
    using System;
    using StormGenerator.Models.Pregen;

    internal class SplitRunCount
    {
        private const int MaxParms = 1000;
        private const int MaxItems = 200;

        public int Get(Model model)
        {
            return Math.Min(MaxItems, MaxParms / (model.MappingFields.ActiveCount() - 1));
        }
    }
}
