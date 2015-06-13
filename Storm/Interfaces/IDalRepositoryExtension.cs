namespace St.Orm.Interfaces
{
    using System.Data;

    public interface IDalRepositoryExtension<TDal>
    {
        int? RelationsCount();

        void ExtendClone(TDal clone, TDal source);

        void ExtendCreate(TDal entity, IDataReader reader);
    }
}
