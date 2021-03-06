﻿namespace StormGenerator.Models.Pregen
{
    using System.Collections.Generic;
    using StormGenerator.Models.Config.Db;
    using StormGenerator.Models.Pregen.Relation;

    internal class Model : ModelBase
    {
        public Model Parent { get; set; }

        public DbModel DbModel { get; set; }

        public List<MappingField> MappingFields { get; set; }

        public List<RelationField> RelationFields { get; set; }

        public bool IsStruct { get; set; }

        public bool IsInherited { get; set; }

        public bool IsReadonly { get; set; }

        public bool IsManyToManyLink { get; set; }

        public List<Model> ChildModels { get; set; }

        public override string ToString()
        {
            return "Model: " + Name;
        }
    }
}
