namespace St.Orm
{
    using System.Data;
    using St.Orm.Interfaces;

    public class EmptyRepositoryExtension<TDal> : IDalRepositoryExtension<TDal>
    {
        public int? RelationsCount()
        {
            return null;
        }

        public virtual void ExtendClone(TDal output, TDal source)
        { }

        public virtual void ExtendCreate(TDal entity, IDataReader reader)
        { }

        public bool PreSave(TDal entity)
        {
            return true;
        }

        public bool PreUpdate(TDal entity, TDal existing)
        {
            return PreSave(entity);
        }

        public bool PreDelete(TDal entity)
        {
            return true;
        }

        public void ExtendSaveRelations(TDal entity, ISavesCollector saves)
        { }

        public void ExtendUpdateRelations(TDal entity, TDal existing, ISavesCollector saves)
        { }

        public bool ExtendEntityChanged(TDal entity, TDal existing)
        {
            return false;
        }
    }
}
