//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace BasicFeaturesTest.StormModel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    internal class CalculationDetailsDalRepository : IDalRepository<CalculationDetails, CalculationDetails>
    {
        private IDalRepositoryExtension<CalculationDetails> extension;

        public CalculationDetailsDalRepository()
        {
            extension = new EmptyRepositoryExtension<CalculationDetails>();
        }

        public void SetExtension(IDalRepositoryExtension<CalculationDetails> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 0;
        }

        public CalculationDetails Create(IDataReader reader, IQueryable<CalculationDetails> query, ILoadService loadService)
        {
            var entity = new CalculationDetails(query, loadService)
            {
                CalculationDetailsId = reader.GetGuid(0),
                CalculationId = reader.GetGuid(1),
                Year = reader.GetInt32(2),
                Month = reader.GetInt32(3),
                Value = reader.GetDecimal(4),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public CalculationDetails Clone(CalculationDetails source)
        {
            var clone = (source as ICloneable<CalculationDetails>).Clone();
            extension.ExtendClone(clone, source);
            return clone;
        }

        public List<CalculationDetails> Materialize(IQueryable<CalculationDetails> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query,
                reader => Create(reader, query, loadService),
                context.Connection,
                context.Transaction);
        }

        public IQueryable<CalculationDetails> GetByIdQuery(object id, IStormContext context)
        {
            var key = (Guid)id;
            return context.Set<CalculationDetails>().Where(x => x.CalculationDetailsId == key);
        }

        public void Save(CalculationDetails entity, ISavesCollector saves)
        {
            if(!extension.PreSave(entity))
            {
                return;
            }

            if(entity.CalculationDetailsId == Guid.Empty)
            {
                entity.CalculationDetailsId = Guid.NewGuid();
            }

            SetMtoFields(entity);
            saves.Save<CalculationDetails, CalculationDetails>(entity);
        }

        public void Update(CalculationDetails entity, CalculationDetails existing, ISavesCollector saves)
        {
            if(!extension.PreUpdate(entity, existing))
            {
                Delete(entity, saves);
                return;
            }

            SetMtoFields(entity);
            if (EntityChanged(entity, existing))
            {
                saves.Update<CalculationDetails, CalculationDetails>(entity, existing);
            }
            else
            {
                saves.NoUpdate<CalculationDetails, CalculationDetails>(entity, existing);
            }
        }

        public void Delete(CalculationDetails entity, ISavesCollector saves)
        {
            if(!extension.PreDelete(entity))
            {
                return;
            }

            DeleteRelations(entity, saves);
            saves.Delete<CalculationDetails, CalculationDetails>(entity);
        }

        public void SaveRelations(CalculationDetails entity, ISavesCollector saves)
        {
            extension.ExtendSaveRelations(entity, saves);
        }

        public void UpdateRelations(CalculationDetails entity, CalculationDetails existing, ISavesCollector saves)
        {
            var populated = (entity as ICloneable<Policy>).GetPopulated();
            extension.ExtendSaveRelations(entity, saves);
        }

        private void DeleteRelations(CalculationDetails entity, ISavesCollector saves)
        {
        }

        private void SetMtoFields(CalculationDetails entity)
        {
        }

        public bool EntityChanged(CalculationDetails entity, CalculationDetails existing)
        {
            return extension.ExtendEntityChanged(entity, existing)
                || entity.CalculationId != existing.CalculationId
                || entity.Year != existing.Year
                || entity.Month != existing.Month
                || entity.Value != existing.Value;
        }

        public void Insert(IStormContext context, IList<CalculationDetails> entities)
        {
            for (int index = 0; index < entities.Count; index++)
            {
                PersistenceEvents.BeforeInsert(entities[index]);
            }

        }
    }
}
