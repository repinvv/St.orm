namespace St.Orm.Interfaces
{
    public interface ISavesCollector
    {
        IStormContext Context { get; }

        void Save<TDal>(TDal entity);

        void Update<TDal>(TDal entity, TDal existing);

        void NoUpdate<TDal>(TDal entity, TDal existing);

        void Delete<TDal>(TDal entity);
    }
}
