namespace StormGenerator.Models.GenModels
{
    internal class NavProp : Item
    {
        public Model FarModel { get; set; }

        public NavProp ReverseNavProp { get; set; }
    }
}
