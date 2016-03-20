namespace StormTest.StormEntities
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using Storm.Interfaces;
    internal partial class DepartmentRepository : IAdoRepository<department, department>
    {
        private readonly Func<department> direct = () => new department();

        public List<department> Materialize(IQueryable<department> query,
            DbConnection connection, 
            DbTransaction transaction, 
            Func<department> create)
        {
            var creationFunc = create ?? direct;
            Func<IDataReader, department> creator = reader =>
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
