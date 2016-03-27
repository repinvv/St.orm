namespace StormGenerator.DatabaseReading
{
    using System.Collections.Generic;
    using StormGenerator.DatabaseReading.DbModels;

    internal interface IDbModelsReader
    {
        List<Table> GetTables();
    }
}
