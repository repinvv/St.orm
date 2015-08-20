namespace StormGenerator.Generation.StaticFilesGeneration
{
    using System;
    using System.Collections.Generic;
    using St.Orm;
    using St.Orm.Interfaces;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class DalRepositoryStorageGenerator : IStaticFileGenerator
    {
        private readonly ModelIterator modelIterator;

        public DalRepositoryStorageGenerator(ModelIterator modelIterator)
        {
            this.modelIterator = modelIterator;
        }

        public string GetName()
        {
            return "Storm.DalRepositoryStorage";
        }

        public void GenerateContent(List<Model> models, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(@"using System;
    using System.Collections.Generic;
    using " + typeof(EmptyRepositoryExtension<>).Namespace + @";
    using " + typeof(IDalRepository<,>).Namespace + @";

    internal static class DalRepositoryStorage
    {
        private static readonly Dictionary<Type, object> repositories
            = new Dictionary<Type, object>");
            stringGenerator.PushIndent(3);
            stringGenerator.Braces(() => GenerateKeyValuePairs(models, stringGenerator), true);
            stringGenerator.PopIndent(3);
            stringGenerator.AppendLine(@"
        public static IDalRepository<TDal, TQuery> GetDalRepository<TDal, TQuery>()
        {
            return repositories[typeof(TDal)] as IDalRepository<TDal, TQuery>;
        }
    }");
        }

        private void GenerateKeyValuePairs(List<Model> models, IStringGenerator stringGenerator)
        {
            Action<Model> action =
                model => stringGenerator
                    .AppendLine($"{{ typeof({model.Name}), new {model.Name}{GenerationConstants.ModelGeneration.RepositorySuffix}() }},");
            modelIterator.ForAllModels(models, action);
        }
    }
}
