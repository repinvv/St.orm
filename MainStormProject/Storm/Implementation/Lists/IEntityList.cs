namespace St.Orm.Implementation.Lists
{
    using St.Orm.Interfaces;

    internal interface IEntityList
    {
        void PersistenceAction(IStormContext context);

        void RelationAction(SavesCollector saves);
    }
}
