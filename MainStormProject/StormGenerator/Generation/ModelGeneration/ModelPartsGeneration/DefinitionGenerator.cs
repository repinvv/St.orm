namespace StormGenerator.Generation.ModelGeneration.ModelPartsGeneration
{
    using StormGenerator.Infrastructure;
    using StormGenerator.Models.Pregen;

    internal class DefinitionGenerator
    {
        private readonly OptionsService options;

        public DefinitionGenerator(OptionsService options)
        {
            this.options = options;
        }

        internal string GetModelDefinition(Model model, Model refModel, string modelType, string additionalInterface)
        {
            var haveId = refModel.KeyFields().Count == 1 ? ", IHaveId" : string.Empty;
            var customInterface = string.IsNullOrWhiteSpace(options.Options.CustomInterfaceForEntities)
                                      ? string.Empty
                                      : ", " + options.Options.CustomInterfaceForEntities.Trim();
            return $"public partial {modelType} {model.Name} : IEquatable<{refModel.Name}>" + additionalInterface + haveId + customInterface;
        }
    }
}
