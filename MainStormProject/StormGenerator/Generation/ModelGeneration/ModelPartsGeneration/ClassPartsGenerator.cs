namespace StormGenerator.Generation.ModelGeneration.ModelPartsGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ClassPartsGenerator : IModelPartsGenerator
    {
        private readonly TypeService service;
        private readonly FieldUtility fieldUtility;
        private readonly DefinitionGenerator definitionGenerator;

        public ClassPartsGenerator(TypeService service,
            FieldUtility fieldUtility,
            DefinitionGenerator definitionGenerator)
        {
            this.service = service;
            this.fieldUtility = fieldUtility;
            this.definitionGenerator = definitionGenerator;
        }

        public IEnumerable<string> GetUsings(Model model)
        {
            return model.MappingFields.ActiveSelect(fieldUtility.GetUsing)
                              .Concat(GenerationConstants.ModelGeneration.ModelClassUsings);
        }

        public string GetDefinition(Model model)
        {
            return definitionGenerator.GetModelDefinition(model, model.Parent, "class", $", ICloneable<{model.Name}>");
        }

        public void GeneratePrivateFields(Model model, IStringGenerator stringGenerator)
        {
            if (model.RelationFields.ActiveAny())
            {
                stringGenerator.AppendLine("private readonly bool[] populated;");
            }

            stringGenerator.AppendLine("private readonly ILoadService loadService;");
            stringGenerator.AppendLine($"IQueryable<{model.Parent.Name}> sourceQuery;");
            stringGenerator.AppendLine($"private readonly {model.Name} clonedFrom;");
        }

        public void GenerateConstructors(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"public {model.Name}({model.Name} clonedFrom, IQueryable<{model.Parent.Name}> sourceQuery, " +
                                       "ILoadService loadService)");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine("this.clonedFrom = clonedFrom;");
                GenerateConstructorAssignments(model, stringGenerator);
            });
            stringGenerator.AppendLine();
            stringGenerator.AppendLine($"public {model.Name}(IQueryable<{model.Parent.Name}> sourceQuery, ILoadService loadService)");
            stringGenerator.Braces(() => GenerateConstructorAssignments(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine($"public {model.Name}()");
            stringGenerator.Braces(() =>
            {
                var fields = model.RelationFields.Active();
                for (int index = 0; index < fields.Count; index++)
                {
                    var field = fields[index];
                    if (field.IsList)
                    {
                        stringGenerator.AppendLine($"field{index} = new List<{field.FieldModel.Name}>();");
                    }
                }

                var bools = string.Join(", ", Enumerable.Range(1, model.RelationFields.ActiveCount()).Select(x => "true"));
                if (model.RelationFields.ActiveAny())
                {
                    stringGenerator.AppendLine($"populated = new bool[]{{{bools}}};");
                }
            });
        }

        private static void GenerateConstructorAssignments(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("this.loadService = loadService;");
            stringGenerator.AppendLine("this.sourceQuery = sourceQuery;");
            if (model.RelationFields.ActiveAny())
            {
                stringGenerator.AppendLine($"populated = new bool[{model.RelationFields.ActiveCount()}];");
            }
        }

        public void GenerateCloneableMembers(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(model.Name + $" ICloneable<{model.Name}>.Clone()");
            stringGenerator.Braces(() => GenerateCloneContent(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine(model.Name + $" ICloneable<{model.Name}>.ClonedFrom()");
            stringGenerator.Braces("return clonedFrom;");
            stringGenerator.AppendLine();
            stringGenerator.AppendLine($"bool[] ICloneable<{model.Name}>.GetPopulated()");
            stringGenerator.Braces(model.RelationFields.ActiveAny() ? "return populated;" : "return null;");
        }

        private void GenerateCloneContent(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"return new {model.Name}(this, sourceQuery, loadService)");
            stringGenerator.Braces(() => GenerateFields(model, stringGenerator), true);
        }

        private void GenerateFields(Model model, IStringGenerator stringGenerator)
        {
            foreach (var field in model.MappingFields.Active())
            {
                stringGenerator.AppendLine(field.Name + $" = {field.Name},");
            }
        }
    }
}
