//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject.StormModel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using St.Orm;
    using St.Orm.Implementation;
    using St.Orm.Interfaces;

    internal class PolicyDalRepository : IDalRepository<Policy, Policy>
    {
        private IDalRepositoryExtension<Policy> extension;

        public PolicyDalRepository()
        {
            extension = new EmptyRepositoryExtension<Policy>();
        }

        public void SetExtension(IDalRepositoryExtension<Policy> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 4;
        }

        public Policy Create(IDataReader reader, IQueryable<Policy> query, ILoadService loadService)
        {
            var entity = new Policy(query, loadService)
            {
                PolicyId = reader.GetInt32(0),
                CountryId = reader[1] as int?,
                CurrencyId = reader.GetInt32(2),
                Name = reader[3] as string,
                Created = reader.GetDateTime(4),
                Updated = reader.GetDateTime(5),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public Policy Clone(Policy source)
        {
            var clone = (source as ICloneable<Policy>).Clone();
            extension.ExtendClone(clone, source);
            return clone;
        }

        public List<Policy> Materialize(IQueryable<Policy> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query,
                reader => Create(reader, query, loadService),
                context.Connection,
                context.Transaction);
        }

        public IQueryable<Policy> GetByIdQuery(object id, IStormContext context)
        {
            var key = (int)id;
            return context.Set<Policy>().Where(x => x.PolicyId == key);
        }

        public void Save(Policy entity, ISavesCollector saves)
        {
            if(!extension.PreSave(entity))
            {
                return;
            }

            SetMtoFields(entity);
            saves.Save<Policy, Policy>(entity);
        }

        public void Update(Policy entity, Policy existing, ISavesCollector saves)
        {
            if(!extension.PreUpdate(entity, existing))
            {
                Delete(entity, saves);
                return;
            }

            SetMtoFields(entity);
            if (EntityChanged(entity, existing))
            {
                saves.Update<Policy, Policy>(entity, existing);
            }
            else
            {
                saves.NoUpdate<Policy, Policy>(entity, existing);
            }
        }

        public void Delete(Policy entity, ISavesCollector saves)
        {
            if(!extension.PreDelete(entity))
            {
                return;
            }

            DeleteRelations(entity, saves);
            saves.Delete<Policy, Policy>(entity);
        }

        public void SaveRelations(Policy entity, ISavesCollector saves)
        {
            if (entity.Comments != null)
            {
                foreach (var field in entity.Comments)
                {
                    field.PolicyId = entity.PolicyId;
                }
            }

            SaveService.Save<Comment, Comment>(entity.Comments, saves);

            if (entity.Taxes != null)
            {
                foreach (var field in entity.Taxes)
                {
                    field.PolicyId = entity.PolicyId;
                }
            }

            SaveService.Save<Tax, Tax>(entity.Taxes, saves);

            if (entity.Assignments != null)
            {
                foreach (var field in entity.Assignments)
                {
                    field.PolicyId = entity.PolicyId;
                }
            }

            SaveService.Save<Assignment, Assignment>(entity.Assignments, saves);

            extension.ExtendSaveRelations(entity, saves);
        }

        public void UpdateRelations(Policy entity, Policy existing, ISavesCollector saves)
        {
            var populated = (entity as ICloneable<Policy>).GetPopulated();
            if(populated[1])
            {
                if (entity.Comments != null)
                {
                    foreach (var field in entity.Comments)
                    {
                        field.PolicyId = entity.PolicyId;
                    }
                }

                SaveService.Update<Comment, Comment>(entity.Comments, existing.Comments, saves);
            }

            if(populated[2])
            {
                if (entity.Taxes != null)
                {
                    foreach (var field in entity.Taxes)
                    {
                        field.PolicyId = entity.PolicyId;
                    }
                }

                SaveService.Update<Tax, Tax>(entity.Taxes, existing.Taxes, saves);
            }

            if(populated[3])
            {
                if (entity.Assignments != null)
                {
                    foreach (var field in entity.Assignments)
                    {
                        field.PolicyId = entity.PolicyId;
                    }
                }

                SaveService.Update<Assignment, Assignment>(entity.Assignments, existing.Assignments, saves);
            }

            extension.ExtendSaveRelations(entity, saves);
        }

        private void DeleteRelations(Policy entity, ISavesCollector saves)
        {
            SaveService.Delete<Comment, Comment>(entity.Comments, saves);
            SaveService.Delete<Tax, Tax>(entity.Taxes, saves);
            SaveService.Delete<Assignment, Assignment>(entity.Assignments, saves);
        }

        private void SetMtoFields(Policy entity)
        {
            if(entity.Country != null)
            {
                entity.CountryId = entity.Country.CountryId;
            }
        }

        public bool EntityChanged(Policy entity, Policy existing)
        {
            return extension.ExtendEntityChanged(entity, existing)
                || entity.CountryId != existing.CountryId
                || entity.CurrencyId != existing.CurrencyId
                || entity.Name != existing.Name
                || entity.Created != existing.Created
                || entity.Updated != existing.Updated;
        }

        public void Insert(IStormContext context, IList<Policy> entities)
        {
            using (new ConnectionHandler(context.Connection))
            {
                AdoCommands.SplitRun(entities.AsList(), x => RangeInsert(x, context.Connection, context.Transaction), 200);
            }
        }

        private void RangeInsert(List<Policy> entities, DbConnection connection, DbTransaction transaction)
        {
            int i;
            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO model.policy");
            sb.AppendLine("    (country_id, currency_id, name, created, updated)");
            sb.AppendLine("OUTPUT inserted.policy_id");
            sb.AppendLine("VALUES");
            sb.AppendLine("    (@parm1i0, @parm2i0, @parm3i0, @parm4i0, @parm5i0)");
            for (i = 1; i < entities.Count; i++)
            {
                sb.AppendLine("   ,(@parm1i" + i + ", @parm2i" + i + ", @parm3i" + i + ", @parm4i" + i + ", @parm5i" + i + ")");
            }

            var parameters = new List<SqlParameter>(entities.Count*5);
            for (i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                parameters.Add(new SqlParameter("@parm1i" + i, entity.CountryId ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter("@parm2i" + i, entity.CurrencyId));
                parameters.Add(new SqlParameter("@parm3i" + i, entity.Name ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter("@parm4i" + i, entity.Created));
                parameters.Add(new SqlParameter("@parm5i" + i, entity.Updated));
            }

            i = 0;
            AdoCommands.RunCommand(sb.ToString(), parameters.ToArray(), connection, transaction, r => entities[i++].PolicyId = r.GetInt32(0));
        }
    }
}