namespace StormGenerator.DbModelCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using StormGenerator.Models.Db;

    internal class FieldTypesCollector
    {
        public void UpdateFieldTypes(List<DbModel> models, IEnumerable<XElement> elements)
        {
            var dict = models.ToDictionary(x => x.Name, x => x);
            foreach (var element in elements)
            {
                DbModel model;
                if (!dict.TryGetValue(element.Attribute("Name").Value, out model))
                {
                    continue;
                }

                foreach (var source in element.Elements())
                {
                    if (source.Name.LocalName != "Property")
                    {
                        continue;
                    }

                    var field = model.Fields.FirstOrDefault(x => x.Name == source.Attribute("Name").Value)
                                ?? model.Fields.FirstOrDefault(x => x.Name.Substring(0, x.Name.Length - 1) == source.Attribute("Name").Value);
                    if (field == null)
                    {
                        continue;
                    }

                    field.StorageType = source.Attribute("Type").Value;
                    var nullableAttr = source.Attribute("Nullable");
                    field.StorageNullable = nullableAttr == null || nullableAttr.Value.ToLower() != "false";
                    var maxlenAttr = source.Attribute("MaxLength");
                    if (maxlenAttr != null)
                    {
                        field.StorageLength = int.Parse(maxlenAttr.Value);
                    }
                }
            }
        }
    }
}
