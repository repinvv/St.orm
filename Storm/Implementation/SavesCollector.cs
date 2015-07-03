namespace St.Orm.Implementation
{
    using System.Linq;
    using St.Orm.Interfaces;

    internal class SavesCollector : ISavesCollector
    {
        private readonly EntityStorage saves = new EntityStorage();
        private EntityStorage updates = new EntityStorage();
        private EntityStorage updatesExisting = new EntityStorage();
        private EntityStorage noUpdate = new EntityStorage();
        private EntityStorage noUpdateExisting = new EntityStorage();
        private EntityStorage deletes = new EntityStorage();

        public SavesCollector(IStormContext context)
        {
            Context = context;
        }

        public IStormContext Context { get; private set; }

        public void Save<TDal, TQuery>(TDal entity) where TDal : class
        {
            saves.Add(entity, () => Context.GetDalRepository<TDal, TQuery>());
        }

        public void Update<TDal, TQuery>(TDal entity, TDal existing)
        { }

        public void NoUpdate<TDal, TQuery>(TDal entity, TDal existing)
        { }

        public void Delete<TDal, TQuery>(TDal entity)
        { }

        public void Commit()
        {
            foreach (var group in saves.GetLists())
            {
                group.Insert(Context);
            }
        }
    }
}
