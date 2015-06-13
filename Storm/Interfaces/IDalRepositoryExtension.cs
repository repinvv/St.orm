namespace St.Orm.Interfaces
{
    public interface IDalRepositoryExtension<TDal>
    {
        int? RelationsCount();
    }
}
