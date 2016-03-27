namespace StormTest.StormEntities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LinqToDB.Mapping;
    using Storm.Exceptions;
    using Storm.Interfaces;

    [Table(Schema = "models", Name = "department")]
    public class department : IDalEntity<department>
    {
        [PrimaryKey, Identity]
        public int department_id { get; set; }

        [Column, NotNull]
        public int company_id { get; set; }

        [Column, Nullable]
        public int? country_id { get; set; }

        [Column, NotNull]
        public string name { get; set; }

        #region private fields
        private readonly department clonedFrom;
        private readonly bool[] populated;
        #endregion

        #region constructors
        public department()
        {
            populated = new bool[0];
        }

        public department(department clonedFrom)
        {
            this.clonedFrom = clonedFrom;
            populated = new bool[0];
        }
        #endregion


        #region IDalEntity members
        department IDalEntity<department>.Clone()
        {
            return new department(this)
            {
                department_id = department_id,
                company_id = company_id,
                country_id = country_id,
                name = name
            };
        }

        department IDalEntity<department>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool IDalEntity<department>.IsPopulated(int navPropIndex)
        {
            throw new NavigationPropertyException("Entity department has no navigation properties.");
        }
        #endregion

        #region equality
        public override bool Equals(object obj)
        {
            return Equals(obj as department);
        }

        protected bool Equals(department other)
        {
            return other != null
                   && department_id == other.department_id;
        }

        public override int GetHashCode()
        {
            return department_id.GetHashCode();
        }

        #endregion
    }
}
