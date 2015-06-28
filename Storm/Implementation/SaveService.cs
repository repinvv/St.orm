namespace St.Orm.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Interfaces;

    public static class SaveService
    {
        public static void Save<TDal, TQuery>(TDal entity, ISavesCollector saves)
        {
            if (entity != null)
            {
                var repo = saves.Context.GetDalRepository<TDal, TQuery>();
                repo.Save(entity, saves);
            }
        }

        public static void Save<TDal, TQuery>(ICollection<TDal> entities, ISavesCollector saves)
        {
            if (entities == null)
            {
                return;
            }

            var repo = saves.Context.GetDalRepository<TDal, TQuery>();
            foreach (var entity in entities)
            {
                repo.Save(entity, saves);
            }
        }

        public static void Update<TDal, TQuery>(TDal entity, TDal existing, ISavesCollector saves)
        {
            if (entity == null)
            {
                Delete<TDal, TQuery>(existing, saves);
                return;
            }

            var repo = saves.Context.GetDalRepository<TDal, TQuery>();
            if (!entity.Equals(existing) || entity.GetType() != existing.GetType())
            {
                Delete<TDal, TQuery>(existing, saves);
                repo.Save(entity, saves);
            }
            else
            {
                repo.Update(entity, existing, saves);
            }
        }

        public static void Update<TDal, TQuery>(ICollection<TDal> entities, ICollection<TDal> existing, ISavesCollector saves)
        {
            if (entities == null)
            {
                Delete<TDal, TQuery>(existing, saves);
                return;
            }

            var repo = saves.Context.GetDalRepository<TDal, TQuery>();
            Delete<TDal, TDal>(existing.Except(entities), saves);
            var existingDictionary = existing.ToDictionary(x => x);
            foreach (var entity in entities)
            {
                var existingEntity = existingDictionary.SafeGet(entity);
                if (existingEntity != null)
                {
                    if (entity.GetType() != existingEntity.GetType())
                    {
                        repo.Delete(existingEntity, saves);
                        repo.Save(entity, saves);
                    }
                    else
                    {
                        repo.Update(entity, existingEntity, saves);
                    }
                }
                else
                {
                    repo.Save(entity, saves);
                }
            }
        }

        public static void Delete<TDal, TQuery>(TDal entity, ISavesCollector saves)
        {
            if (entity == null)
            {
                return;
            }

            var repo = saves.Context.GetDalRepository<TDal, TQuery>();
            repo.Delete(entity, saves);
        }

        public static void Delete<TDal, TQuery>(IEnumerable<TDal> entities, ISavesCollector saves)
        {
            if (entities == null)
            {
                return;
            }

            var repo = saves.Context.GetDalRepository<TDal, TQuery>();
            foreach (var entity in entities)
            {
                repo.Delete(entity, saves);
            }
        }
    }
}
