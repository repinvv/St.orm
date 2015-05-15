namespace StormGenerator.DbModelCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using St.Orm;
    using StormGenerator.Model.Db;

    internal class AssociationCollector
    {
        public void PopulateAssociations(List<DbModel> models, List<XElement> elements)
        {
            var modelDict = models.ToDictionary(x => x.Name);
            foreach (var element in elements)
            {
                var referentialElement = element.Elements().FirstOrDefault(x => x.Name.LocalName == "ReferentialConstraint");
                if (referentialElement == null)
                {
                    continue;
                }

                var principal = referentialElement.Elements().First(x => x.Name.LocalName == "Principal");
                var model = modelDict[principal.Attribute("Role").Value];
                var dependent = referentialElement.Elements().First(x => x.Name.LocalName == "Dependent");
                var dependentName = dependent.Attribute("Role").Value;
                if (!IsDependentNameOtherEntity(dependentName, model.Name))
                {
                    continue;
                }

                var dependentModel = modelDict.SafeGet(dependentName);
                if (dependentModel == null)
                {
                    dependentModel = new DbModel
                                     {
                                         IsManyToManyLink = true,
                                         Name = dependentName,
                                         Fields = new List<DbField>(),
                                         Associations = new List<DbAssociation>()
                                     };
                    models.Add(dependentModel);
                    modelDict.Add(dependentName, dependentModel);
                }                

                model.Associations.Add(CreateAssociation(model, principal, dependentModel, dependent));
            }
        }

        private bool IsDependentNameOtherEntity(string dependentName, string modelName)
        {
            return !dependentName.StartsWith(modelName)
                   || dependentName.Length != modelName.Length + 1
                   || !char.IsDigit(dependentName.Last());
        }

        private DbAssociation CreateAssociation(DbModel model, XElement principal, DbModel depModel, XElement dependent)
        {
            var modelFields = GetReferenceFields(principal, model);
            var referenceFields = GetReferenceFields(dependent, depModel);
            return new DbAssociation
                   {
                       Dependent = depModel,
                       ReferenceFields = model.Fields
                                              .Where(x => x.IsPrimaryKey)
                                              .Select(field => referenceFields[modelFields.IndexOf(field)])
                                              .ToList()
                   };
        }

        private List<DbField> GetReferenceFields(XElement dependent, DbModel dependentModel)
        {
            return dependent
                .Elements()
                .Where(x => x.Name.LocalName == "PropertyRef")
                .Select(x => GetOrCreateModelField(dependentModel, x))
                .ToList();
        }

        private DbField GetOrCreateModelField(DbModel dependentModel, XElement element)
        {
            var fieldName = element.Attribute("Name").Value;
            var field = dependentModel.Fields.FirstOrDefault(y => y.FieldName == fieldName);
            if (field == null)
            {
                field = new DbField
                        {
                            FieldName = fieldName,
                            IsPrimaryKey = true,
                            Index = dependentModel.Fields.Count,
                            IsNullable = false
                        };
                dependentModel.Fields.Add(field);
            }

            return field;
        }
    }
}
