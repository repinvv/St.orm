namespace StormGenerator.Generation.StaticFilesGeneration
{
    using System.Collections.Generic;
    using St.Orm.Interfaces;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class DalRepositoryStorageGenerator : IStaticFileGenerator
    {
        public string GetName(Options options)
        {
            return "DalRepositoryStorage";
        }

        public void GenerateContent(List<Model> models, Options options, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(@"using System;
    using System.Collections.Generic;
    using " + typeof(IDalRepositoryStorage).Namespace + @";
    
    internal class DalRepositoryStorage : IDalRepositoryStorage
    {
        private readonly Dictionary<Type, object> repositories 
            = new Dictionary<Type, object>");
            stringGenerator.PushIndent(3);
            stringGenerator.Braces(() => GenerateKeyValuePairs(models, stringGenerator), true);
            stringGenerator.PopIndent(3);
            stringGenerator.AppendLine(@"
        public IDalRepository<T> GetDalRepository<T>()
        {
            return repositories[typeof(T)] as IDalRepository<T>;
        }
    }");
        }

        private void GenerateKeyValuePairs(List<Model> models, IStringGenerator stringGenerator)
        {
            foreach (var model in models)
            {
                stringGenerator.AppendLine("{ typeof(" + model.Name + "), new " + model.Name
                                           + GenerationConstants.ModelGeneration.RepositorySuffix + "() },");
            }
        }
    }
}
