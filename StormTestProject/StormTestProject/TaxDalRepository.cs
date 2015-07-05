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
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using St.Orm;
    using St.Orm.Implementation;
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

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 0;
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
            extension.ExtendSaveRelations(entity, saves);
        }

        public void UpdateRelations(Tax entity, Tax existing, ISavesCollector saves)
        {
            var populated = (entity as ICloneable<Policy>).GetPopulated();
            extension.ExtendSaveRelations(entity, saves);
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

        public void Insert(IStormContext context, IList<Tax> entities)
        {
            using (new ConnectionHandler(context.Connection))
            {
                AdoCommands.SplitRun(entities.AsList(), x => RangeInsert(x, context.Connection, context.Transaction), 200);
            }
        }

        private void RangeInsert(List<Tax> entities, DbConnection connection, DbTransaction transaction)
        {
            int i;
            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO model.tax");
            sb.AppendLine("    (policy_id, amount, created, updated");
            sb.AppendLine("OUTPUT inserted.tax_id");
            sb.AppendLine("VALUES");
            sb.AppendLine("    (@parm1i0, @parm2i0, @parm3i0, @parm4i0)");
            for (i = 1; i < entities.Count; i++)
            {
                sb.AppendLine("   ,(@parm1i" + i + ", @parm2i" + i + ", @parm3i" + i + ", @parm4i" + i + ")");
            }

            var parameters = new List<SqlParameter>(entities.Count*4);
            for (i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                parameters.Add(new SqlParameter("@parm1" + i, entity.PolicyId));
                parameters.Add(new SqlParameter("@parm2" + i, entity.Amount));
                parameters.Add(new SqlParameter("@parm3" + i, entity.Created));
                parameters.Add(new SqlParameter("@parm4" + i, entity.Updated));
            }

            i = 0;
            AdoCommands.RunCommand(sb.ToString(), parameters.ToArray(), connection, transaction, r => entities[i++].TaxId = r.GetInt32(0));
        }
    }
}
