namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.Common;
    using StormGenerator.Generation.StaticFilesGeneration.ContextGeneration.RelationFieldInitializers;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class InitializersGenerator
    {
        private readonly RelationFieldInitializer relationFieldInitializer;
        private readonly InitializerStartingLine initializerStartingLine;
        private readonly ObjectStringService objectStringService;

        public InitializersGenerator(RelationFieldInitializer relationFieldInitializer, 
            InitializerStartingLine initializerStartingLine,
            ObjectStringService objectStringService)
        {
            this.relationFieldInitializer = relationFieldInitializer;
            this.initializerStartingLine = initializerStartingLine;
            this.objectStringService = objectStringService;
        }

        public bool NeedsInitializer(Model model)
        {
            return model == model.Parent;
        }

        public void GenerateInitializers(List<Model> models, IStringGenerator stringGenerator)
        {
            var relationIds = new HashSet<string>();
            foreach (var model in models.Where(NeedsInitializer))
            {
                stringGenerator.AppendLine();
                stringGenerator.AppendLine("protected virtual void Initialize" + model.Name + "Fields(DbModelBuilder modelBuilder)");
                stringGenerator.Braces(() => GenerateInitializer(model, relationIds, stringGenerator));
            }
        }

        private void GenerateInitializer(Model model, HashSet<string> relationIds, IStringGenerator stringGenerator)
        {
            InitializeTable(model, stringGenerator);
            foreach (var field in model.RelationFields.Active( x=>!relationIds.Contains(x.AssociationId)))
            {
                relationIds.Add(field.AssociationId);
                relationFieldInitializer.InitializeRelationField(model, field, stringGenerator);
            }

            foreach (var field in model.MappingFields.Active())
            {
                InitializeMappingField(model, field, stringGenerator);
            }
        }

        private void InitializeMappingField(Model model, MappingField field, IStringGenerator stringGenerator)
        {
            initializerStartingLine.CreateInitializerStartingLine(model, stringGenerator);
            stringGenerator.PushIndent();
            stringGenerator.AppendLine(".Property(e => e." + field.Name + ")");
            if (GenerationConstants.ModelGeneration.TypesWithPrecision.Contains(field.DbField.Type))
            {
                stringGenerator.AppendLine($".HasPrecision({field.DbField.Precision}, {field.DbField.Scale})");
            }

            if (field.DbField.IsPrimaryKey)
            {
                var gen = field.DbField.IsIdentity ? "Identity": "None";
                stringGenerator.AppendLine($".HasDatabaseGeneratedOption(DatabaseGeneratedOption.{gen})");
            }

            if (field.Type == typeof(string))
            {
                if (!field.DbField.IsNullable)
                {
                    stringGenerator.AppendLine(".IsRequired()");
                }

                if (field.DbField.Length > 0)
                {
                    stringGenerator.AppendLine($".HasMaxLength({field.DbField.Length})");
                }
            }

            stringGenerator.AppendLine($".HasColumnName(\"{field.DbField.Name}\")");
            stringGenerator.AppendLine($".HasColumnType(\"{field.DbField.Type}\")");
            stringGenerator.AppendLine($".HasColumnOrder({field.DbField.Index});");
            stringGenerator.PopIndent();
        }

        private void InitializeTable(Model model, IStringGenerator stringGenerator)
        {
            initializerStartingLine.CreateInitializerStartingLine(model, stringGenerator);
            stringGenerator.PushIndent();
            stringGenerator.AppendLine($".ToTable(\"{model.DbModel.Name}\", \"{model.DbModel.Schema}\")");
            var keyFields = model.KeyFields().Select(x => x.Name).ToList();
            var key = objectStringService.CreateObjectString(keyFields, "x");
            stringGenerator.AppendLine($".HasKey(x => {key});");
            stringGenerator.PopIndent();
        }
    }
}
