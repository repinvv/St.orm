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

    internal class CurrencyDalRepository : IDalRepository<Currency, Currency>
    {
        private IDalRepositoryExtension<Currency> extension;

        public CurrencyDalRepository()
        {
            extension = new EmptyRepositoryExtension<Currency>();
        }

        public void SetExtension(IDalRepositoryExtension<Currency> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 1;
        }

        public Currency Clone(Currency source)
        {
            var clone = (source as ICloneable<Currency>).Clone();
            extension.ExtendClone(clone, source);
            return clone;
        }

        public Currency Create(IDataReader reader, IQueryable<Currency> query, ILoadService loadService)
        {
            var entity = new Currency(query, loadService)
            {
                CurrencyId = reader.GetInt32(0),
                Name = reader[1] as string,
                CurrencyCode = reader[2] as string,
                Created = reader.GetDateTime(3),
                Updated = reader.GetDateTime(4),
            };
            extension.ExtendCreate(entity, reader);
            return entity;
        }

        public List<Currency> Materialize(IQueryable<Currency> query, ILoadService loadService)
        {
            var context = loadService.Context;
            return AdoCommands.Materialize(query,
                reader => Create(reader, query, loadService),
                context.Connection,
                context.Transaction);
        }
    }
}
