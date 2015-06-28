namespace St.Orm.Interfaces
{
    public interface ISavesCollector
    {
        IStormContext Context { get; }

        void Save<TDal, TQuery>(TDal entity);

        void Update<TDal, TQuery>(TDal entity, TDal existing);

        void NoUpdate<TDal, TQuery>(TDal entity, TDal existing);

        void Delete<TDal, TQuery>(TDal entity);
    }
}
