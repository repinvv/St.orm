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

        public Department Clone(Department source)
        {
            var clone = new Department(source)
            {
                DepartmentId = source.DepartmentId,
                Name = source.Name,
                Created = source.Created,
                Updated = source.Updated,
            };
            extension.ExtendClone(clone, source);
            return clone;
        }

        public Department Create(IDataReader reader, ILoadService loadService)
        {
            var entity = new Department(loadService)
            {
                DepartmentId = reader.GetInt32(0),
                Name = reader[1] as string,
                Created = reader.GetDateTime(2),
                Updated = reader.GetDateTime(3),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public List<Department> Materialize(IQueryable<Department> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query as IQueryable<Department>,
                reader => Create(reader, loadService),
                context.Connection,
                context.Transaction);
        }
    }
}
