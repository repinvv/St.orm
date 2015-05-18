namespace StormGenerator.DbModelCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using StormGenerator.Models.Db;

    internal class DbModelsCollector
    {
        private readonly ModelCollector modelCollector;
        private readonly FieldTypesCollector fieldTypesCollector;
        private readonly SchemaNamesCollector schemaNamesCollector;
        private readonly AssociationCollector associationCollector;
        private readonly AssociationTypeUpdater associationTypeUpdater;

        public DbModelsCollector(ModelCollector modelCollector, 
            FieldTypesCollector fieldTypesCollector, 
            SchemaNamesCollector schemaNamesCollector,
            AssociationCollector associationCollector,
            AssociationTypeUpdater associationTypeUpdater)
        {
            this.modelCollector = modelCollector;
            this.fieldTypesCollector = fieldTypesCollector;
            this.schemaNamesCollector = schemaNamesCollector;
            this.associationCollector = associationCollector;
            this.associationTypeUpdater = associationTypeUpdater;
        }

        public List<DbModel> GetModels(string edmxSchema)
        {
            const string SchemaName = "http://schemas.microsoft.com/ado/2009/11/edmx";
            var xdoc = XDocument.Load(edmxSchema);
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("edmx", SchemaName);
            var elements = xdoc.XPathSelectElement("/edmx:Edmx/edmx:Runtime/edmx:ConceptualModels", namespaceManager)
                               .Elements()
                               .First()
                               .Elements().ToList();
            var models = elements.Where(x => x.Name.LocalName == "EntityType").Select(x => modelCollector.GetModel(x)).ToList();
            elements = xdoc.XPathSelectElement("/edmx:Edmx/edmx:Runtime/edmx:StorageModels", namespaceManager)
                               .Elements()
                               .First()
                               .Elements().ToList();
            associationCollector.PopulateAssociations(models, elements.Where(x => x.Name.LocalName == "Association").ToList());
            fieldTypesCollector.UpdateFieldTypes(models, elements.Where(x => x.Name.LocalName == "EntityType"));
            elements = xdoc.XPathSelectElement("/edmx:Edmx/edmx:Runtime/edmx:StorageModels", namespaceManager)
                           .Elements()
                           .First()
                           .Elements().ToList();
            models.ForEach(x => associationTypeUpdater.UpdateAssociationTypes(x));
            var a = elements.First(x => x.Name.LocalName == "EntityContainer");
            elements = a.Elements()
                        .Where(x => x.Name.LocalName == "EntitySet")
                        .ToList();
            schemaNamesCollector.UpdateSchemaName(models, elements);
            return models;
        }
    }
}