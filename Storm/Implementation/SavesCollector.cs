namespace St.Orm.Implementation
{
    using St.Orm.Interfaces;

    internal class SavesCollector : ISavesCollector
    {
        public SavesCollector(IStormContext context)
        {
            Context = context;
        }

        public IStormContext Context { get; private set; }

        public void Save<TDal, TQuery>(TDal entity)
        { }

        public void Update<TDal, TQuery>(TDal entity, TDal existing)
        { }

        public void NoUpdate<TDal, TQuery>(TDal entity, TDal existing)
        { }

        public void Delete<TDal, TQuery>(TDal entity)
        { }

        public void Commit()
        {
        }
    }
}
