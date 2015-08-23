namespace StormGenerator.Models.Pregen.Relation
{
    internal class Relation
    {
        public Model RootModel { get; set; }

        public MappingField RootField { get; set; }

        public Model Model { get; set; }

        public MappingField Field { get; set; }

        public string Id { get; set; }

        public int Index { get; set; }

        public bool Cascade { get; set; }
    }
}
