namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration.RelationFieldInitializers
{
    using System;
    using System.Collections.Generic;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class RelationFieldInitializer
    {
        private readonly InitializerStartingLine initializerStartingLine;
        private readonly Dictionary<Type, IRelationFieldInitializer> initializers;

        public RelationFieldInitializer(ManyToManyIgnoreInitializer manyToManyIgnoreInitializer,
            OneToManyFieldInitializer oneToManyFieldInitializer,
            ManyToOneFieldInitializer manyToOneFieldInitializer,
            InitializerStartingLine initializerStartingLine)
        {
            this.initializerStartingLine = initializerStartingLine;
            initializers = new Dictionary<Type, IRelationFieldInitializer>
                           {
                               { typeof(OneToManyField), oneToManyFieldInitializer },
                               { typeof(ManyToManyField), manyToManyIgnoreInitializer },
                               { typeof(ManyToOneField), manyToOneFieldInitializer }
                           };
        }

        public void InitializeRelationField(Model model, RelationField field, IStringGenerator stringGenerator)
        {
            var initializer = initializers[field.GetType()];
            initializerStartingLine.CreateInitializerStartingLine(model, stringGenerator);
            stringGenerator.PushIndent();
            initializer.InitializeRelationField(field, stringGenerator);
            stringGenerator.PopIndent();
        }
    }
}
