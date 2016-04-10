namespace StormGenerator.Models.GenModels
{
    using System.Collections.Generic;
    using StormGenerator.Models.DbModels;

    internal class Model : Item
    {
        public string Id => string.IsNullOrEmpty(NamespaceSuffix)
            ? Name
            : NamespaceSuffix + "." + Name;

        public string NamespaceSuffix { get; set; }

        public Table Table { get; set; }

        public List<Field> Fields { get; set; }

        public List<NavProp> NavProps { get; set; }
    }
}
