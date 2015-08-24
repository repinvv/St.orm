namespace StormGenerator.Models.Pregen
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class ModelExtension
    {
        public static List<MappingField> KeyFields(this Model model)
        {
            return GetKeyFields(model).ToList();
        }

        public static bool HasId(this Model model)
        {
            return GetKeyFields(model).Count() == 1;
        }

        private static IEnumerable<MappingField> GetKeyFields(Model model)
        {
            return model.MappingFields.Where(x => x.DbField.IsPrimaryKey).ToList();
        }
    }
}
