namespace StormGenerator.DbModelsCollection
{
    internal class TableIdCreator
    {
        public string CreateTableId(string db, string schema, string name)
        {
            return db + "." + schema + "." + name;
        }
    }
}
