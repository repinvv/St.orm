namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using System.Linq;
    using StormGenerator.Generation.Common;
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class GetByIdQueryGenerator : IMethodGenerator
    {
        private readonly TypeService typeService;
        private readonly IdentityFinder identityFinder;

        public GetByIdQueryGenerator(TypeService typeService, IdentityFinder identityFinder)
        {
            this.typeService = typeService;
            this.identityFinder = identityFinder;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public IQueryable<" + model.Parent.Name + "> GetByIdQuery(object id, IStormContext context)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            if (!identityFinder.HasId(model))
            {
                stringGenerator.AppendLine("throw new Exception(\"Get by id is only available for entities with single primary key field.\");");
                return;
            }

            identityFinder.WithIdentity(model, field =>
            {
                stringGenerator.AppendLine("var key = (" + typeService.GetTypeName(field.Type) + ")id;");
                stringGenerator.AppendLine("return context.Set<" + model.Parent.Name + ">().Where(x => x." + field.Name + " == key);");
            });
        }
    }
}
