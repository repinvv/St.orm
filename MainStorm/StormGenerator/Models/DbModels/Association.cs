namespace StormGenerator.Models.DbModels
{
    internal class Association
    {
        public string ConstraintId { get; set; }

        public int Index { get; set; }

        public string TableId { get; set; }

        public string FieldName { get; set; }

        public bool Cascade { get; set; }
    }
}
