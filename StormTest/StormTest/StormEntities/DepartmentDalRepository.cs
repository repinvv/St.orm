namespace StormTest.StormEntities
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Storm.Interfaces;
    internal partial class DepartmentRepository : IDalRepository<department, department>
    {
        public List<department> Materialize(ILoadService<department> loadService)
        {
            var connection = loadService.Context.Connection;
            var transaction = loadService.Context.Transaction;
            return Materialize(loadService.Query, connection, transaction, () => new department());
        }

        public int NavPropsCount()
        {
            return 0;
        }

        public department Clone(department item)
        {
            return (item as IDalEntity<department>).Clone();
        }
    }
}
