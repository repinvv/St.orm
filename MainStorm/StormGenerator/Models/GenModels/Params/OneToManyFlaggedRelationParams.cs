namespace StormGenerator.Models.GenModels.Params
{
    using StormGenerator.Models.Configs.RelationConfigs.Params;

    internal class OneToManyFlaggedRelationParams : IRelationParams
    {
        public RelationType RelationType => RelationType.OneToManyFlagged;

        public string FlagColumn { get; set; }

        public string TrueValue { get; set; }

        public string FalseValue { get; set; }
    }
}
