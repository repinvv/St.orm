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

    internal class AssignmentDalRepository : IDalRepository<Assignment, Assignment>
    {
        private IDalRepositoryExtension<Assignment> extension;

        public AssignmentDalRepository()
        {
            extension = new EmptyRepositoryExtension<Assignment>();
        }

        public void SetExtension(IDalRepositoryExtension<Assignment> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 4;
        }

        public Assignment Create(IDataReader reader, IQueryable<Assignment> query, ILoadService loadService)
        {
            var entity = new Assignment(query, loadService)
            {
                AssignmentId = reader.GetInt32(0),
                PolicyId = reader.GetInt32(1),
                Comment = reader[2] as string,
                Created = reader.GetDateTime(3),
                Updated = reader.GetDateTime(4),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public Assignment Clone(Assignment source)
        {
            var clone = (source as ICloneable<Assignment>).Clone();
            extension.ExtendClone(clone, source);
            return clone;
        }

        public List<Assignment> Materialize(IQueryable<Assignment> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query,
                reader => Create(reader, query, loadService),
                context.Connection,
                context.Transaction);
        }

        public IQueryable<Assignment> GetByIdQuery(object id, IStormContext context)
        {
            var key = (int)id;
            return context.Set<Assignment>().Where(x => x.AssignmentId == key);
        }

        public void Save(Assignment entity, ISavesCollector saves)
        {
            if(!extension.PreSave(entity))
            {
                return;
            }

            SetMtoFields(entity);
            saves.Save<Assignment, Assignment>(entity);
        }

        public void Update(Assignment entity, Assignment existing, ISavesCollector saves)
        {
            if(!extension.PreUpdate(entity, existing))
            {
                Delete(entity, saves);
                return;
            }

            SetMtoFields(entity);
            if (EntityChanged(entity, existing))
            {
                saves.Update<Assignment, Assignment>(entity, existing);
            }
            else
            {
                saves.NoUpdate<Assignment, Assignment>(entity, existing);
            }
        }

        public void Delete(Assignment entity, ISavesCollector saves)
        {
            if(!extension.PreDelete(entity))
            {
                return;
            }

            DeleteRelations(entity, saves);
            saves.Delete<Assignment, Assignment>(entity);
        }

        public void SaveRelations(Assignment entity, ISavesCollector saves)
        {
            if(entity.Departments != null)
            {
                foreach(var item in entity.Departments)
                {
                    var mediator = new AssignmentDepartment
                    {
                        AssignmentId = entity.AssignmentId,
                        DepartmentId = item.DepartmentId
                    };
                    saves.Save<AssignmentDepartment, AssignmentDepartment>(mediator);
                }
            }

            if(entity.Eligibilities != null)
            {
                foreach(var item in entity.Eligibilities)
                {
                    var mediator = new AssignmentEligibility
                    {
                        AssignmentId = entity.AssignmentId,
                        EligibilityId = item.EligibilityId
                    };
                    saves.Save<AssignmentEligibility, AssignmentEligibility>(mediator);
                }
            }

            if (entity.Premiums != null)
            {
                foreach (var field in entity.Premiums)
                {
                    field.AssignmentId = entity.AssignmentId;
                }
            }

            SaveService.Save<Premium, Premium>(entity.Premiums, saves);

            if (entity.Covereds != null)
            {
                foreach (var field in entity.Covereds)
                {
                    field.AssignmentId = entity.AssignmentId;
                }
            }

            SaveService.Save<Covered, Covered>(entity.Covereds, saves);

            extension.ExtendSaveRelations(entity, saves);
        }

        public void UpdateRelations(Assignment entity, Assignment existing, ISavesCollector saves)
        {
            var populated = (entity as ICloneable<Policy>).GetPopulated();
            if(populated[0])
            {
            }

            if(populated[1])
            {
            }

            if(populated[2])
            {
                if (entity.Premiums != null)
                {
                    foreach (var field in entity.Premiums)
                    {
                        field.AssignmentId = entity.AssignmentId;
                    }
                }

                SaveService.Update<Premium, Premium>(entity.Premiums, existing.Premiums, saves);
            }

            if(populated[3])
            {
                if (entity.Covereds != null)
                {
                    foreach (var field in entity.Covereds)
                    {
                        field.AssignmentId = entity.AssignmentId;
                    }
                }

                SaveService.Update<Covered, Covered>(entity.Covereds, existing.Covereds, saves);
            }

            extension.ExtendSaveRelations(entity, saves);
        }

        private void DeleteRelations(Assignment entity, ISavesCollector saves)
        {
            SaveService.Delete<Premium, Premium>(entity.Premiums, saves);
            SaveService.Delete<Covered, Covered>(entity.Covereds, saves);
        }

        private void SetMtoFields(Assignment entity)
        {
        }

        public bool EntityChanged(Assignment entity, Assignment existing)
        {
            return extension.ExtendEntityChanged(entity, existing)
                || entity.PolicyId != existing.PolicyId
                || entity.Comment != existing.Comment
                || entity.Created != existing.Created
                || entity.Updated != existing.Updated;
        }
    }
}
