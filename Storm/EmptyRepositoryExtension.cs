namespace St.Orm
{
    using St.Orm.Interfaces;

    public class EmptyRepositoryExtension<TDal> : IDalRepositoryExtension<TDal>
    {
        public virtual int? RelationsCount()
        {
            return null;
        }

        public virtual void ExtendClone(TDal output, TDal source)
        { }
    }
}
