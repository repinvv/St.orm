namespace StormGenerator.ModelsCreation
{
    using StormGenerator.Models.GenModels;

    internal static class ModelExtension
    {
        public static bool IsKeyField(this Field field)
        {
            return field.Column.IsPrimaryKey;
        }
    }
}
