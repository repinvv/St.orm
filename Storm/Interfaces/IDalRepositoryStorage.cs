namespace St.Orm.Interfaces
{
    public interface IDalRepositoryStorage
    {
        IDalRepository<T> GetDalRepository<T>();
    }
}
