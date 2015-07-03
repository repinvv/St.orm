namespace St.Orm.Implementation
{
    using System;
    using St.Orm.Interfaces;

    internal interface IEntityList
    {
        void Add(object entity);

        Type QueryType { get; }

        int Count { get; }

        void Insert(IStormContext context);
    }
}