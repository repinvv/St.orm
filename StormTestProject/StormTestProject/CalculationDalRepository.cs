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

    internal class CalculationDalRepository : IDalRepository<Calculation, Calculation>
    {
        private IDalRepositoryExtension<Calculation> extension;

        public CalculationDalRepository()
        {
            extension = new EmptyRepositoryExtension<Calculation>();
        }

        public void SetExtension(IDalRepositoryExtension<Calculation> extension)
        {
            this.extension = extension;
        }

        public int RelationsCount()
        {
            return extension.RelationsCount() ?? 1;
        }

        public Calculation Clone(Calculation source)
        {
            var clone = new Calculation(source)
            {
                CalculationId = source.CalculationId,
                Name = source.Name,
                DueDate = source.DueDate,
            };
            extension.ExtendClone(clone, source);
            return clone;
        }

        public Calculation Create(IDataReader reader)
        {
        }

        public List<Calculation> Materialize(IQueryable<Calculation> query, ILoadService loadService)
        {
        }
    }
}
