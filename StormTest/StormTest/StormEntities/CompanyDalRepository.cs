namespace StormTest.StormEntities
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Storm.Interfaces;
    internal partial class CompanyRepository : IDalRepository<company, company>
    {
        public List<company> Materialize(IQueryable<company> query, ILoadService loadService)
        {
            var connection = loadService.Context.Connection;
            var transaction = loadService.Context.Transaction;
            return Materialize(query, connection, transaction, () => new company(query, loadService));
        }

        public int NavPropsCount()
        {
            return 1;
        }

        public company Clone(company item)
        {
            return (item as IDalEntity<company>).Clone();
        }
    }
}
