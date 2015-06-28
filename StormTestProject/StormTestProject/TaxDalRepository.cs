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
    using St.Orm.Interfaces;

    internal class TaxDalRepository : IDalRepository<Tax, Tax>
    {
        private IDalRepositoryExtension<Tax> extension;

        public TaxDalRepository()
        {
            extension = new EmptyRepositoryExtension<Tax>();
        }

        public void SetExtension(IDalRepositoryExtension<Tax> extension)
        {
            this.extension = extension;
        }

        public int NavPropsCount()
        {
            return extension.NavPropsCount() ?? 0;
        }

        public Tax Create(IDataReader reader, IQueryable<Tax> query, ILoadService loadService)
        {
            var entity = new Tax(query, loadService)
            {
                TaxId = reader.GetInt32(0),
                PolicyId = reader.GetInt32(1),
                Amount = reader.GetDecimal(2),
                Created = reader.GetDateTime(3),
                Updated = reader.GetDateTime(4),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public Tax Clone(Tax source)
        {
            var clone = (source as ICloneable<Tax>).Clone();
            extension.ExtendClone(clone, source);
            return clone;
        }

        public List<Tax> Materialize(IQueryable<Tax> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query,
                reader => Create(reader, query, loadService),
                context.Connection,
                context.Transaction);
        }

        public IQueryable<Tax> GetByIdQuery(object id, IStormContext context)
        {
            var key = (int)id;
            return context.Set<Tax>().Where(x => x.TaxId == key);
        }

        public void Save(Tax entity, ISavesCollector saves)
        {
            if(!extension.PreSave(entity))
            {
                return;
            }

            SetMtoFields(entity);
            saves.Save<Tax, Tax>(entity);
        }

        public void Update(Tax entity, Tax existing, ISavesCollector saves)
        {
            if(!extension.PreUpdate(entity, existing))
            {
                Delete(entity, saves);
                return;
            }

            SetMtoFields(entity);
            if (EntityChanged(entity, existing))
            {
                saves.Update<Tax, Tax>(entity, existing);
            }
            else
            {
                saves.NoUpdate<Tax, Tax>(entity, existing);
            }
        }

        public void Delete(Tax entity, ISavesCollector saves)
        {
            if(!extension.PreDelete(entity))
            {
                return;
            }

            DeleteRelations(entity, saves);
            saves.Delete<Tax, Tax>(entity);
        }

        public void SaveRelations(Tax entity, ISavesCollector saves)
        {
        }

        public void UpdateRelations(Tax entity, Tax existing, ISavesCollector saves)
        {
        }

        private void DeleteRelations(Tax entity, ISavesCollector saves)
        {
        }

        private void SetMtoFields(Tax entity)
        {
        }

        public bool EntityChanged(Tax entity, Tax existing)
        {
            return extension.ExtendEntityChanged(entity, existing)
                || entity.PolicyId != existing.PolicyId
                || entity.Amount != existing.Amount
                || entity.Created != existing.Created
                || entity.Updated != existing.Updated;
        }
    }
}
