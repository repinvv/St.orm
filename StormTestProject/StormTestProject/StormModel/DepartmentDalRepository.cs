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

    internal class DepartmentDalRepository : IDalRepository<Department, Department>
    {
        private IDalRepositoryExtension<Department> extension;

        public DepartmentDalRepository()
        {
            extension = new EmptyRepositoryExtension<Department>();
        }

        public void SetExtension(IDalRepositoryExtension<Department> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 0;
        }

        public Department Create(IDataReader reader, IQueryable<Department> query, ILoadService loadService)
        {
            var entity = new Department(query, loadService)
            {
                DepartmentId = reader.GetInt32(0),
                Name = reader[1] as string,
                Created = reader.GetDateTime(2),
                Updated = reader.GetDateTime(3),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public Department Clone(Department source)
        {
            var clone = (source as ICloneable<Department>).Clone();
            extension.ExtendClone(clone, source);
            return clone;
        }

        public List<Department> Materialize(IQueryable<Department> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query,
                reader => Create(reader, query, loadService),
                context.Connection,
                context.Transaction);
        }

        public IQueryable<Department> GetByIdQuery(object id, IStormContext context)
        {
            var key = (int)id;
            return context.Set<Department>().Where(x => x.DepartmentId == key);
        }

        public void Save(Department entity, ISavesCollector saves)
        {
            if(!extension.PreSave(entity))
            {
                return;
            }

            SetMtoFields(entity);
            saves.Save<Department, Department>(entity);
        }

        public void Update(Department entity, Department existing, ISavesCollector saves)
        {
            if(!extension.PreUpdate(entity, existing))
            {
                Delete(entity, saves);
                return;
            }

            SetMtoFields(entity);
            if (EntityChanged(entity, existing))
            {
                saves.Update<Department, Department>(entity, existing);
            }
            else
            {
                saves.NoUpdate<Department, Department>(entity, existing);
            }
        }

        public void Delete(Department entity, ISavesCollector saves)
        {
            if(!extension.PreDelete(entity))
            {
                return;
            }

            DeleteRelations(entity, saves);
            saves.Delete<Department, Department>(entity);
        }

        public void SaveRelations(Department entity, ISavesCollector saves)
        {
            extension.ExtendSaveRelations(entity, saves);
        }

        public void UpdateRelations(Department entity, Department existing, ISavesCollector saves)
        {
            var populated = (entity as ICloneable<Policy>).GetPopulated();
            extension.ExtendSaveRelations(entity, saves);
        }

        private void DeleteRelations(Department entity, ISavesCollector saves)
        {
        }

        private void SetMtoFields(Department entity)
        {
        }

        public bool EntityChanged(Department entity, Department existing)
        {
            return extension.ExtendEntityChanged(entity, existing)
                || entity.Name != existing.Name
                || entity.Created != existing.Created
                || entity.Updated != existing.Updated;
        }

        public void Insert(IStormContext context, IList<Department> entities)
        {
            for (int index = 0; index < entities.Count; index++)
            {
                PersistenceEvents.BeforeInsert(entities[index]);
            }

            using (new ConnectionHandler(context.Connection))
            {
                AdoCommands.SplitRun(entities.AsList(), x => RangeInsert(x, context.Connection, context.Transaction), 200);
            }
        }

        private void RangeInsert(List<Department> entities, DbConnection connection, DbTransaction transaction)
        {
            int i;
            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO department");
            sb.AppendLine("    (name, created, updated)");
            sb.AppendLine("OUTPUT inserted.department_id");
            sb.AppendLine("VALUES");
            sb.AppendLine("    (@parm1i0, @parm2i0, @parm3i0)");
            for (i = 1; i < entities.Count; i++)
            {
                sb.AppendLine("   ,(@parm1i" + i + ", @parm2i" + i + ", @parm3i" + i + ")");
            }

            var parameters = new List<SqlParameter>(entities.Count*3);
            for (i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                parameters.Add(new SqlParameter("@parm1i" + i, entity.Name ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter("@parm2i" + i, entity.Created));
                parameters.Add(new SqlParameter("@parm3i" + i, entity.Updated));
            }

            i = 0;
            AdoCommands.RunCommand(sb.ToString(), parameters.ToArray(), connection, transaction, r => entities[i++].DepartmentId = r.GetInt32(0));
        }
    }
}
