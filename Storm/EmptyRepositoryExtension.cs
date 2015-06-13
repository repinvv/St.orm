namespace St.Orm
{
    using St.Orm.Interfaces;

    public class EmptyRepositoryExtension<TDal> : IDalRepositoryExtension<TDal>
    {
        public int? RelationsCount()
        {
            return null;
        }
    }
}
