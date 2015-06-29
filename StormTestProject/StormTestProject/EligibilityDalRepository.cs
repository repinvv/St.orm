//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using St.Orm;
    using St.Orm.Implementation;
    using St.Orm.Interfaces;

    internal class EligibilityDalRepository : IDalRepository<Eligibility, Eligibility>
    {
        private IDalRepositoryExtension<Eligibility> extension;

        public EligibilityDalRepository()
        {
            extension = new EmptyRepositoryExtension<Eligibility>();
        }

        public void SetExtension(IDalRepositoryExtension<Eligibility> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 0;
        }

        public Eligibility Create(IDataReader reader, IQueryable<Eligibility> query, ILoadService loadService)
        {
            var entity = new Eligibility(query, loadService)
            {
                EligibilityId = reader.GetInt32(0),
                Name = reader[1] as string,
                Created = reader.GetDateTime(2),
                Updated = reader.GetDateTime(3),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public Eligibility Clone(Eligibility source)
        {
            var clone = (source as ICloneable<Eligibility>).Clone();
            extension.ExtendClone(clone, source);
            return clone;
        }

        public List<Eligibility> Materialize(IQueryable<Eligibility> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query,
                reader => Create(reader, query, loadService),
                context.Connection,
                context.Transaction);
        }

        public IQueryable<Eligibility> GetByIdQuery(object id, IStormContext context)
        {
            var key = (int)id;
            return context.Set<Eligibility>().Where(x => x.EligibilityId == key);
        }

        public void Save(Eligibility entity, ISavesCollector saves)
        {
            if(!extension.PreSave(entity))
            {
                return;
            }

            SetMtoFields(entity);
            saves.Save<Eligibility, Eligibility>(entity);
        }

        public void Update(Eligibility entity, Eligibility existing, ISavesCollector saves)
        {
            if(!extension.PreUpdate(entity, existing))
            {
                Delete(entity, saves);
                return;
            }

            SetMtoFields(entity);
            if (EntityChanged(entity, existing))
            {
                saves.Update<Eligibility, Eligibility>(entity, existing);
            }
            else
            {
                saves.NoUpdate<Eligibility, Eligibility>(entity, existing);
            }
        }

        public void Delete(Eligibility entity, ISavesCollector saves)
        {
            if(!extension.PreDelete(entity))
            {
                return;
            }

            DeleteRelations(entity, saves);
            saves.Delete<Eligibility, Eligibility>(entity);
        }

        public void SaveRelations(Eligibility entity, ISavesCollector saves)
        {
            extension.ExtendSaveRelations(entity, saves);
        }

        public void UpdateRelations(Eligibility entity, Eligibility existing, ISavesCollector saves)
        {
            var populated = (entity as ICloneable<Policy>).GetPopulated();
            extension.ExtendSaveRelations(entity, saves);
        }

        private void DeleteRelations(Eligibility entity, ISavesCollector saves)
        {
        }

        private void SetMtoFields(Eligibility entity)
        {
        }

        public bool EntityChanged(Eligibility entity, Eligibility existing)
        {
            return extension.ExtendEntityChanged(entity, existing)
                || entity.Name != existing.Name
                || entity.Created != existing.Created
                || entity.Updated != existing.Updated;
        }
    }
}
