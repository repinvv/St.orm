namespace StormGenerator.Models.GenModels
{
    using StormGenerator.Models.GenModels.Params;

    internal class Relation : Item
    {
        public Model FarModel { get; set; }

        public Relation ReverseRelation { get; set; }

        public IRelationParams Parameters { get; set; }
    }
}
