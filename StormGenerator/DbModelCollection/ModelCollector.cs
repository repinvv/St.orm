namespace StormGenerator.DbModelCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using StormGenerator.Models.Db;

    internal class ModelCollector
    {
        private readonly ModelFieldCollector modelFieldsCollector;

        public ModelCollector(ModelFieldCollector modelFieldsCollector)
        {
            this.modelFieldsCollector = modelFieldsCollector;
        }

        public DbModel GetModel(XElement xelement)
        {
            var attributes = xelement.Attributes();
            var elements = xelement.Elements().ToArray();
            var fields = elements.Where(x => x.Name.LocalName == "Property")
                                 .Select(x => modelFieldsCollector.GetModelField(x)).ToList();
            for (int i = 0; i < fields.Count; i++)
            {
                fields[i].Index = i;
            }

            modelFieldsCollector.GetKeyFields(elements.First(x => x.Name.LocalName == "Key"), fields).ForEach(x => x.IsPrimaryKey = true);
            return new DbModel
                   {
                       Name = attributes.First(x => x.Name == "Name").Value,
                       Fields = fields,
                       Associations = new List<DbAssociation>()
                   };
        }
    }
}