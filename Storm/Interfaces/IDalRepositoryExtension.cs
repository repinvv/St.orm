namespace St.Orm.Interfaces
{
    using System.Data;

    public interface IDalRepositoryExtension<TDal>
    {
        // override this method if you add custom navigation properties
        int? NavPropsCount();

        // extra action for cloning
        void ExtendClone(TDal clone, TDal source);

        // extra action for entity creation during materialization
        void ExtendCreate(TDal entity, IDataReader reader);

        // extra action before saving, return false if entity is "invalid"
        // and should not be saved to DB 
        bool PreSave(TDal entity);

        // extra action before updating, return false if entity is "invalid"
        // and should be deleted from DB 
        bool PreUpdate(TDal entity, TDal existing);

        // extra action before deletion, include delete entities in custom nav props
        // return false to prevent delete operation
        bool PreDelete(TDal entity);

        // save entities in custom nav props
        void ExtendSaveRelations(TDal entity);

        // update entities in custom nav props
        void ExtendUpdateRelations(TDal entity, TDal existing);

        // check extra fields
        bool ExtendEntityChanged(TDal entity, TDal existing);
    }
}
