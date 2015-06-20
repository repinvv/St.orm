namespace StormGenerator.Models.Pregen
{
    internal class ModelBase
    {
        public ModelBase()
        {
            Enabled = true;
        }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public bool Readonly { get; set; }
    }
}
