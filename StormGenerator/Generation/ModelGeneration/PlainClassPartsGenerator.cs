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
            stringGenerator.AppendLine("private readonly bool[] populated = new bool[" + model.RelationFields.ActiveCount() + "];");
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
                GenerateConstructorAssignments(stringGenerator);
            });
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public " + model.Name + "(IQueryable<" + model.Parent.Name + "> sourceQuery, ILoadService loadService)");
            stringGenerator.Braces(() => GenerateConstructorAssignments(stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public " + model.Name + "()");
            stringGenerator.Braces(() =>
            {
                foreach (var field in model.RelationFields.Active().Where(x => x.IsList))
                {
                    stringGenerator.AppendLine(field.Name + " = new HashSet<" + field.FieldModel.Name + ">();");
                }
            });
        }

        private static void GenerateConstructorAssignments(IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("this.loadService = loadService;");
            stringGenerator.AppendLine("this.sourceQuery = sourceQuery;");
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
            stringGenerator.Braces(() => stringGenerator.AppendLine("return populated;"));
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
