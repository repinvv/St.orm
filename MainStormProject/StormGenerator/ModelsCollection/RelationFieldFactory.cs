﻿namespace StormGenerator.ModelsCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class RelationFieldFactory
    {
        private readonly NameCreator nameCreator;

        public RelationFieldFactory(NameCreator nameCreator)
        {
            this.nameCreator = nameCreator;
        }

        public RelationField CreateManyToOneField(List<Relation> relations)
        {
            return new ManyToOneField
            {
                Name = relations[0].RootModel.Name,
                AssociationId = relations[0].Id,
                FieldModel = relations[0].RootModel,
                FarEndFields = relations.Select(x => x.RootField).ToList(),
                NearEndFields = relations.Select(x => x.Field).ToList(),
                Cascade = relations[0].Cascade
            };
        }

        public RelationField CreateOneToManyField(List<Relation> relations)
        {
            return new OneToManyField
            {
                Name = nameCreator.CreatePluralName(relations[0].Model.Name),
                IsList = true,
                AssociationId = relations[0].Id,
                FieldModel = relations[0].Model,
                NearEndFields = relations.Select(x => x.RootField).ToList(),
                FarEndFields = relations.Select(x => x.Field).ToList(),
                Cascade = relations[0].Cascade
            };
        }

        public void CreateManyToManyField(Relation first, Relation last)
        {
            var mtoField = CreateManyToOneField(new List<Relation> { last });
            first.Model.RelationFields.Add(mtoField);
            var mtmField = new ManyToManyField
                           {
                               Name = nameCreator.CreatePluralName(last.RootModel.Name),
                               FieldModel = last.RootModel,
                               AssociationId = first.Id,
                               IsList = true,
                               NearEndFields = new List<MappingField> { first.RootField },
                               FarEndFields = new List<MappingField> { first.Field },
                               MediatorModel = first.Model,
                               MediatorMtoField = mtoField,
                               Cascade = first.Cascade
                           };
            first.RootModel.RelationFields.Add(mtmField);
        }
    }
}
