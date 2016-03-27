namespace StormGenerator.DatabaseReading
{
    using System.Collections.Generic;
    using StormGenerator.Models.DbModels;

    internal interface IDbModelsReader
    {
        List<Table> GetTables();
    }
}
