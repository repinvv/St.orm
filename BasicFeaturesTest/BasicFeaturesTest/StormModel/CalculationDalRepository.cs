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

    internal class CalculationDalRepository : IDalRepository<Calculation, Calculation>
    {
        private IDalRepositoryExtension<Calculation> extension;

        public CalculationDalRepository()
        {
            extension = new EmptyRepositoryExtension<Calculation>();
        }

        public void SetExtension(IDalRepositoryExtension<Calculation> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 1;
        }

        public Calculation Create(IDataReader reader, IQueryable<Calculation> query, ILoadService loadService)
        {
            var entity = new Calculation(query, loadService)
            {
                CalculationId = reader.GetGuid(0),
                Name = reader[1] as string,
                DueDate = reader[2] as DateTime?,
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public Calculation Clone(Calculation source)
        {
            var clone = (source as ICloneable<Calculation>).Clone();
            extension.ExtendClone(clone, source);
            return clone;
        }

        public List<Calculation> Materialize(IQueryable<Calculation> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query,
                reader => Create(reader, query, loadService),
                context.Connection,
                context.Transaction);
        }

        public IQueryable<Calculation> GetByIdQuery(object id, IStormContext context)
        {
            var key = (Guid)id;
            return context.Set<Calculation>().Where(x => x.CalculationId == key);
        }

        public void Save(Calculation entity, ISavesCollector saves)
        {
            if(!extension.PreSave(entity))
            {
                return;
            }

            if(entity.CalculationId == Guid.Empty)
            {
                entity.CalculationId = Guid.NewGuid();
            }

            SetMtoFields(entity);
            saves.Save<Calculation, Calculation>(entity);
        }

        public void Update(Calculation entity, Calculation existing, ISavesCollector saves)
        {
            if(!extension.PreUpdate(entity, existing))
            {
                Delete(entity, saves);
                return;
            }

            SetMtoFields(entity);
            if (EntityChanged(entity, existing))
            {
                saves.Update<Calculation, Calculation>(entity, existing);
            }
            else
            {
                saves.NoUpdate<Calculation, Calculation>(entity, existing);
            }
        }

        public void Delete(Calculation entity, ISavesCollector saves)
        {
            if(!extension.PreDelete(entity))
            {
                return;
            }

            DeleteRelations(entity, saves);
            saves.Delete<Calculation, Calculation>(entity);
        }

        public void SaveRelations(Calculation entity, ISavesCollector saves)
        {
            if (entity.CalculationDetailses != null)
            {
                foreach (var field in entity.CalculationDetailses)
                {
                    field.CalculationId = entity.CalculationId;
                }
            }

            SaveService.Save<CalculationDetails, CalculationDetails>(entity.CalculationDetailses, saves);

            extension.ExtendSaveRelations(entity, saves);
        }

        public void UpdateRelations(Calculation entity, Calculation existing, ISavesCollector saves)
        {
            var populated = (entity as ICloneable<Policy>).GetPopulated();
            if(populated[0])
            {
                if (entity.CalculationDetailses != null)
                {
                    foreach (var field in entity.CalculationDetailses)
                    {
                        field.CalculationId = entity.CalculationId;
                    }
                }

                SaveService.Update<CalculationDetails, CalculationDetails>(entity.CalculationDetailses, existing.CalculationDetailses, saves);
            }

            extension.ExtendSaveRelations(entity, saves);
        }

        private void DeleteRelations(Calculation entity, ISavesCollector saves)
        {
            SaveService.Delete<CalculationDetails, CalculationDetails>(entity.CalculationDetailses, saves);
        }

        private void SetMtoFields(Calculation entity)
        {
        }

        public bool EntityChanged(Calculation entity, Calculation existing)
        {
            return extension.ExtendEntityChanged(entity, existing)
                || entity.Name != existing.Name
                || entity.DueDate != existing.DueDate;
        }

        public void Insert(IStormContext context, IList<Calculation> entities)
        {
            for (int index = 0; index < entities.Count; index++)
            {
                PersistenceEvents.BeforeInsert(entities[index]);
            }

        }
    }
}
