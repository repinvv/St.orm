namespace StormTest.StormEntities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LinqToDB.Mapping;
    using Storm.Exceptions;
    using Storm.Interfaces;

    [Table(Schema = "models", Name = "company")]
    public class company : IDalEntity<company>
    {
        [PrimaryKey, Identity]
        public int company_id { get; set; }

        [Column, Nullable]
        public string name { get; set; }

        [Association(ThisKey = "company_id", OtherKey = "company_id", CanBeNull = true, IsBackReference = true)]
        public List<department> Departments { get { return GetField0(); } set { pop0 = true; field0 = value; } }

        #region private fields
        private readonly company clonedFrom;
        private readonly ILoadService<company> loadService;

        private bool pop0;
        private List<department> field0;
        #endregion

        #region constructors
        public company(ILoadService<company> loadService)
        {
            this.loadService = loadService;
        }

        public company(company clonedFrom, ILoadService<company> loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
        }

        public company()
        {
            pop0 = true;
            Departments = new List<department>();
        }
        #endregion

        #region private methods
        private List<department> GetField0()
        {
            if (pop0)
            {
                return field0;
            }

            Func<IQueryable<department>> query = () =>
            {
                return loadService.Context.Set<department>()
                    .Join(loadService.Query, x => x.company_id, x => x.company_id, (x, y) => x);
            };
            Func<department, int> indexLambda = x => x.company_id;
            var items = loadService.GetList(0, query, indexLambda, company_id);
            if (clonedFrom == null)
            {
                pop0 = true;
                return field0 = items;
            }

            clonedFrom.Departments = items;
            field0 = new List<department>(items.Count);
            field0.AddRange(items.Select(x => (x as IDalEntity<department>).Clone()));
            pop0 = true;
            return field0;
        }

        #endregion

        #region IDalEntity members
        company IDalEntity<company>.Clone()
        {
            return new company(this, loadService)
            {
                company_id = company_id,
                name = name
            };
        }

        company IDalEntity<company>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool IDalEntity<company>.IsPopulated(int navPropIndex)
        {
            switch (navPropIndex)
            {
                case 0:
                    return pop0;
            }
            
            throw new NavigationPropertyException("Entity company has 1 navigation property, index " 
                + navPropIndex + " is not allowed.");
        }
        #endregion

        #region equality
        public override bool Equals(object obj)
        {
            return Equals(obj as company);
        }

        protected bool Equals(company other)
        {
            return other != null
                   && company_id == other.company_id;
        }

        public override int GetHashCode()
        {
            return company_id.GetHashCode();
        }

        #endregion
    }
}
