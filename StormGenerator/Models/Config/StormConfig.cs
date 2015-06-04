namespace StormGenerator.Models.Config
{
    using System.Collections.Generic;
    using StormGenerator.Models.Config.Db;

    public class StormConfig
    {
        public RelationsMode RelationsMode { get; set; }

        public List<DbModel> DbModels { get; set; }

        public List<ModelConfig> ModelConfigs { get; set; }
    }
}
