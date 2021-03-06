﻿namespace St.Orm
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

        public static void Save<TDal>(TDal entity, IStormContext context)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            var saves = new SavesCollector(context);
            var existing = (entity as ICloneable<TDal>).ClonedFrom();
            SaveService.Update<TDal, TDal>(entity, existing, saves);
            saves.Commit();
        }

        public static void Save<TDal>(TDal entity, TDal existing, IStormContext context)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            var saves = new SavesCollector(context);
            SaveService.Update<TDal, TDal>(entity, existing, saves);
            saves.Commit();
        }

        public static void Save<TDal>(ICollection<TDal> entities, IStormContext context)
        {
            if (entities == null)
            {
                throw new ArgumentException("entities");
            }

            var saves = new SavesCollector(context);
            var list = entities.AsList();
            var existing = new List<TDal>(list.Count);
            foreach (var entity in list)
            {
                existing.Add((entity as ICloneable<TDal>).ClonedFrom());
            }

            SaveService.Update<TDal, TDal>(list, existing, saves);
            saves.Commit();
        }

        public static void Save<TDal>(ICollection<TDal> entities, ICollection<TDal> existing, IStormContext context)
        {
            if (entities == null)
            {
                throw new ArgumentException("entities");
            }

            var saves = new SavesCollector(context);
            if (existing == null)
            {
                SaveService.Save<TDal, TDal>(entities.AsList(), saves);
            }
            else
            {
                SaveService.Update<TDal, TDal>(entities.AsList(), existing, saves);
            }

            saves.Commit();
        }

        public static void Delete<TDal>(TDal entity, IStormContext context)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            var saves = new SavesCollector(context);
            SaveService.Delete<TDal, TDal>(entity, saves);
            saves.Commit();
        }

        public static void Delete<TDal>(ICollection<TDal> entities, IStormContext context)
        {
            if (entities == null)
            {
                throw new ArgumentException("entities");
            }

            var saves = new SavesCollector(context);
            foreach (var entity in entities)
            {
                SaveService.Delete<TDal, TDal>(entity, saves);
            }

            saves.Commit();
        }
    }
}
