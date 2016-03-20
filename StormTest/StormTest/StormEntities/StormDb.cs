namespace StormTest.StormEntities
{
    using LinqToDB;

    public partial class StormDb : LinqToDB.Data.DataConnection
    {
        public ITable<company> companies { get { return GetTable<company>(); } }
        public ITable<department> departments { get { return GetTable<department>(); } }
    }
}
