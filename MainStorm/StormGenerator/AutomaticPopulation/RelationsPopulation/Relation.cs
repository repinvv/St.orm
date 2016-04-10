namespace StormGenerator.AutomaticPopulation.RelationsPopulation
{
    internal class Relation
    {
        public string RefTableId { get; set; }

        public string RefColumn { get; set; }
        
        public string Column { get; set; }

        public string Id { get; set; }

        public int Index { get; set; }

        public bool Cascade { get; set; }
    }
}