namespace StormTest.StormEntities
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using Storm.Interfaces;
    internal partial class CompanyRepository : IAdoRepository<company, company>
    {
        private readonly Func<company> direct = () => new company();

        public List<company> Materialize(IQueryable<company> query, 
            DbConnection connection, 
            DbTransaction transaction,
            Func<company> create)
        {
            var creationFunc = create ?? direct;
            Func<IDataReader, company> creator = reader =>
            {
                var item = creationFunc();
                item.company_id = reader.GetInt32(0);
                item.name = reader[1] as string;
                return item;
            };
            return AdoCommands.Materialize(query, connection, transaction, creator);
        }
    }
}
