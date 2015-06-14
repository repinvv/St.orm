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
            return extension.RelationsCount() ?? 3;
        }

        public Policy Clone(Policy source)
        {
            var clone = new Policy(source)
            {
                PolicyId = source.PolicyId,
                CountryId = source.CountryId,
                CurrencyId = source.CurrencyId,
                Name = source.Name,
                Created = source.Created,
                Updated = source.Updated,
            };
            extension.ExtendClone(clone, source);
            return clone;
        }

        public Policy Create(IDataReader reader, ILoadService loadService)
        {
            var entity = new Policy(loadService)
            {
                PolicyId = reader.GetInt32(0),
                CountryId = reader.GetInt32(1),
                CurrencyId = reader[2] as int?,
                Name = reader[3] as string,
                Created = reader.GetDateTime(4),
                Updated = reader.GetDateTime(5),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public List<Policy> Materialize(IQueryable<Policy> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query as IQueryable<Policy>,
                reader => Create(reader, loadService),
                context.Connection,
                context.Transaction);
        }
    }
}
