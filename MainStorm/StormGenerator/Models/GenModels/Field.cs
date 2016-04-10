namespace StormGenerator.Models.GenModels
{
    using StormGenerator.Models.DbModels;

    internal class Field : Item
    {
        public Column Column { get; set; }
    }
}
