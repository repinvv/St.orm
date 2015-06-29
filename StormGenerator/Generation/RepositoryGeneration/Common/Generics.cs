namespace StormGenerator.Generation.RepositoryGeneration.Common
{
    using StormGenerator.Models.Pregen;

    internal class Generics
    {
        public string Line(Model model)
        {
            return "<" + model.Name + ", " + model.Parent.Name + ">";
        }
    }
}
