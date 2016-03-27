namespace StormGenerator.DatabaseReading.MsSql
{
    internal class TableIdCreator
    {
        public string CreateTableId(string schema, string table)
        {
            return schema == "dbo" ? table : (schema + "." + table);
        }
    }
}
