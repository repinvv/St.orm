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

    internal class CoverageDalRepository : IDalRepository<Coverage, Coverage>
    {
        private IDalRepositoryExtension<Coverage> extension;

        public CoverageDalRepository()
        {
            extension = new EmptyRepositoryExtension<Coverage>();
        }

        public void SetExtension(IDalRepositoryExtension<Coverage> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 4;
        }

        public Coverage Clone(Coverage source)
        {
            var clone = new Coverage(source)
            {
                CoverageId = source.CoverageId,
                PolicyId = source.PolicyId,
                Comment = source.Comment,
                Created = source.Created,
                Updated = source.Updated,
            };
            extension.ExtendClone(clone, source);
            return clone;
        }

        public Coverage Create(IDataReader reader, ILoadService loadService)
        {
            var entity = new Coverage(loadService)
            {
                CoverageId = reader.GetInt32(0),
                PolicyId = reader.GetInt32(1),
                Comment = reader[2] as string,
                Created = reader.GetDateTime(3),
                Updated = reader.GetDateTime(4),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public List<Coverage> Materialize(IQueryable<Coverage> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query as IQueryable<Coverage>,
                reader => Create(reader, loadService),
                context.Connection,
                context.Transaction);
        }
    }
}
