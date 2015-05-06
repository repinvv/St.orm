namespace StormGenerator.Model.Serial
{
    using System.Collections.Generic;

    public class SerialModel
    {
        public string Name { get; set; }

        public string DbModelName { get; set; }

        public bool IsStructure { get; set; }

        public List<SerialField> Fields { get; set; } 
    }
}
