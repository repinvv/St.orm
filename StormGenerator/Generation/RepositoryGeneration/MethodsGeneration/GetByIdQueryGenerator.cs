namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using System.Linq;
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class GetByIdQueryGenerator : IMethodGenerator
    {
        private readonly TypeService typeService;

        public GetByIdQueryGenerator(TypeService typeService)
        {
            this.typeService = typeService;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public IQueryable<" + model.Parent.Name + "> GetByIdQuery(object id, IStormContext context)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            if (model.Parent.MappingFields.Count(x => x.DbField.IsPrimaryKey) != 1)
            {
                stringGenerator.AppendLine("throw new Exception(\"Get by id is only available for entities with single primary key field.\");");
                return;
            }

            var field = model.Parent.MappingFields.First(x => x.DbField.IsPrimaryKey);
            stringGenerator.AppendLine("var key = (" + typeService.GetTypeName(field.Type) + ")id;");
            stringGenerator.AppendLine("return context.Set<" + model.Parent.Name + ">().Where(x => x." + field.Name + " == key);");
        }
    }
}
