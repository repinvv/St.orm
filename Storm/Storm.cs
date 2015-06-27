namespace St.Orm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Implementation;
    using St.Orm.Interfaces;
    using St.Orm.Parameters;

    public static class Storm
    {
        public static List<TDal> Get<TDal>(IQueryable<TDal> query, IStormContext context, params LoadParameter[] parameters)
        {
            return StormGetImplementation.Get(query, context, parameters);
        }

        public static TDal GetById<TDal>(object id, IStormContext context, params LoadParameter[] parameters)
        {
            var query = context.GetDalRepository<TDal, TDal>().GetByIdQuery(id, context);
            return StormGetImplementation.Get(query, context, parameters).SingleOrDefault();
        }

        public static void Save<T>(ICollection<T> entities, IStormContext context)
        {
            if (entities == null)
            {
                throw new ArgumentException("entities");
            }

            var saves = new SavesCollector();
            var list = entities.AsList();
            var existing = new List<T>(entities.Count);
            foreach (var entity in entities)
            {
                existing.Add((entity as ICloneable<T>).ClonedFrom());
            }
                
            saveService.UpdateEntities(list, existing, saves, null);
            saves.Commit(context);
        }

        public static void SaveEntities<T>(ICollection<T> entities, ICollection<T> existing, IStormContext context)
        {
            if (entities == null)
            {
                throw new ArgumentException("entities");
            }

            var saves = new SavesCollector();
            saveService.UpdateEntities(entities.AsList(), existing.AsList(), saves, null);
            saves.Commit(context);
        }
    }
}
