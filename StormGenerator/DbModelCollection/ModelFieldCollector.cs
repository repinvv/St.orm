namespace StormGenerator.DbModelCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using StormGenerator.Model.Db;

    internal class ModelFieldCollector
    {
        public DbField GetModelField(XElement xelement)
        {
            var name = xelement.Attribute("Name").Value;
            var typeName = xelement.Attribute("Type").Value;
            var nullableAttribute = xelement.Attribute("Nullable");
            var isNullable = nullableAttribute == null || nullableAttribute.Value == "true";
            var idAttribute = xelement.Attributes().FirstOrDefault(x => x.Name.LocalName == "StoreGeneratedPattern");
            var isId = idAttribute != null && idAttribute.Value.Contains("Identity");
            var type = GetFieldType(typeName, isNullable);
            return new DbField
                   {
                       FieldName = name,
                       IsIdentity = isId,
                       IsNullable = isNullable,
                       Type = type
                   };
        }

        private Type GetFieldType(string name, bool isNullable)
        {
            switch (name.ToLower())
            {
                case "boolean":
                    return typeof(bool);
                case "binary":
                    return typeof(byte[]);
                case "string":
                    return typeof(string);
                case "int64":
                case "long":
                    return isNullable ? typeof(long?) : typeof(long);
                case "int32":
                case "int":
                    return isNullable ? typeof(int?) : typeof(int);
                case "datetime":
                    return isNullable ? typeof(DateTime?) : typeof(DateTime);
                case "decimal":
                    return isNullable ? typeof(decimal?) : typeof(decimal);
                case "int16":
                    return isNullable ? typeof(short?) : typeof(short);
                case "guid":
                    return typeof(Guid);
                default:
                    throw new Exception("Type " + name + " not implemented.");
            }
        }

        public List<DbField> GetKeyFields(XElement element, IEnumerable<DbField> fields)
        {
            var names = element.Elements().Select(x => x.Attribute("Name").Value).ToArray();
            return fields.Where(x => names.Contains(x.FieldName)).ToList();
        }
    }
}