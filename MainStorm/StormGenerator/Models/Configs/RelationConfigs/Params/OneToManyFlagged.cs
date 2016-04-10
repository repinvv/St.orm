namespace StormGenerator.Models.Configs.RelationConfigs.Params
{
    internal class OneToManyFlaggedConfigParams : IRelationConfigParams
    {
        public RelationType RelationType => RelationType.OneToManyFlagged;

        public string ReverseRelationName { get; set; }

        public string FlagColumn { get; set; }

        public string TrueValue { get; set; }

        public string FalseValue { get; set; }
    }
}
