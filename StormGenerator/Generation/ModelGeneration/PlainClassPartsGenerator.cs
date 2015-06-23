namespace StormGenerator.Generation.ModelGeneration
{
    using System;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class PlainClassPartsGenerator : IModelPartsGenerator
    {
        private readonly TypeService service;
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;

        public PlainClassPartsGenerator(TypeService service,
            UsingsGenerator usingsGenerator,
            FieldUtility fieldUtility)
        {
            this.service = service;
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
        }

        public void GenerateUsings(Model model, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.Active().Select(fieldUtility.GetUsing)
                              .Concat(GenerationConstants.ModelGeneration.ModelClassUsings);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
        }

        public void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public partial class " + model.Name + " : ICloneable<" + model.Name + ">");
        }

        public void GenerateMappingProperty(Model model, MappingField field, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + service.GetTypeName(field.Type) + " " + field.Name + " { get;set; }");
        }

        public void GeneratePrivateFields(Model model, IStringGenerator stringGenerator)
        {
            if (model.RelationFields.AnyActive())
            {
                stringGenerator.AppendLine("private readonly bool[] populated;");
            }

            stringGenerator.AppendLine("private readonly ILoadService loadService;");
            stringGenerator.AppendLine("IQueryable<" + model.Parent.Name + "> sourceQuery;");
            stringGenerator.AppendLine("private readonly " + model.Name + " clonedFrom;");
        }

        public void GenerateConstructors(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + model.Name + "(" + model.Name + " clonedFrom, IQueryable<" + model.Parent.Name + "> sourceQuery, ILoadService loadService)");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine("this.clonedFrom = clonedFrom;");
                GenerateConstructorAssignments(model, stringGenerator);
            });
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public " + model.Name + "(IQueryable<" + model.Parent.Name + "> sourceQuery, ILoadService loadService)");
            stringGenerator.Braces(() => GenerateConstructorAssignments(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public " + model.Name + "()");
            stringGenerator.Braces(() =>
            {
                var fields = model.RelationFields.Active();
                for (int index = 0; index < fields.Count; index++)
                {
                    var field = fields[index];
                    if (field.IsList)
                    {
                        stringGenerator.AppendLine("field" + index + " = new HashSet<" + field.FieldModel.Name + ">();");
                    }
                }

                var bools = string.Join(", ", Enumerable.Range(1, model.RelationFields.ActiveCount()).Select(x => "true"));
                if (model.RelationFields.AnyActive())
                {
                    stringGenerator.AppendLine("populated = new bool[]{" + bools + "};");
                }
            });
        }

        private static void GenerateConstructorAssignments(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("this.loadService = loadService;");
            stringGenerator.AppendLine("this.sourceQuery = sourceQuery;");
            if (model.RelationFields.AnyActive())
            {
                stringGenerator.AppendLine("populated = new bool[" + model.RelationFields.ActiveCount() + "];");
            }
        }

        public void GenerateCloneableMembers(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(model.Name + " ICloneable<" + model.Name + ">.Clone()");
            stringGenerator.Braces(() => GenerateCloneContent(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine(model.Name + " ICloneable<" + model.Name + ">.ClonedFrom()");
            stringGenerator.Braces(() => stringGenerator.AppendLine("return clonedFrom;"));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("bool[] ICloneable<" + model.Name + ">.GetPopulated()");
            stringGenerator.Braces(() => stringGenerator.AppendLine(model.RelationFields.AnyActive() ? "return populated;" : "return null;"));
        }

        private void GenerateCloneContent(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("return new " + model.Name + "(this, sourceQuery, loadService)");
            stringGenerator.Braces(() => GenerateFields(model, stringGenerator), true);
        }

        private void GenerateFields(Model model, IStringGenerator stringGenerator)
        {
            foreach (var field in model.MappingFields.Active())
            {
                stringGenerator.AppendLine(field.Name + " = " + field.Name + ",");
            }
        }
    }
}
