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

    internal class CoveredDalRepository : IDalRepository<Covered, Covered>
    {
        private IDalRepositoryExtension<Covered> extension;

        public CoveredDalRepository()
        {
            extension = new EmptyRepositoryExtension<Covered>();
        }

        public void SetExtension(IDalRepositoryExtension<Covered> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 0;
        }

        public Covered Clone(Covered source)
        {
            var clone = (source as ICloneable<Covered>).Clone();
            extension.ExtendClone(clone, source);
            return clone;
        }

        public Covered Create(IDataReader reader, IQueryable<Covered> query, ILoadService loadService)
        {
            var entity = new Covered(query, loadService)
            {
                CoveredId = reader.GetInt32(0),
                CoveredType = reader.GetInt32(1),
                AssignmentId = reader.GetInt32(2),
                Headcount = reader.GetInt32(3),
                Created = reader.GetDateTime(4),
                Updated = reader.GetDateTime(5),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public List<Covered> Materialize(IQueryable<Covered> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query,
                reader => Create(reader, query, loadService),
                context.Connection,
                context.Transaction);
        }
    }
}
