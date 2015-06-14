namespace St.Orm
{
    using System.Data;
    using St.Orm.Interfaces;

    public class EmptyRepositoryExtension<TDal> : IDalRepositoryExtension<TDal>
    {
        public virtual int? RelationsCount()
        {
            return null;
        }

        public virtual void ExtendClone(TDal output, TDal source)
        { }

        public virtual void ExtendCreate(TDal entity, IDataReader reader)
        { }
    }
}
