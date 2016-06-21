//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject.StormModel
{
	using System;

    public partial class EntityWithId : IEquatable<EntityWithId>
    {	
        public int Id { get; set; }

        public long? ABigint { get; set; }

        public int AInt { get; set; }

        public decimal? ANumeric { get; set; }

        public bool? ABit { get; set; }

        public short? ASmallint { get; set; }

        public decimal? ADecimal { get; set; }

        public decimal? ASmallmoney { get; set; }

        public byte? ATinyint { get; set; }

        public decimal? AMoney { get; set; }

        #region equality
        public override bool Equals(object obj)
        {
            return Equals(obj as EntityWithId);		  
        }
			  
        public bool Equals(EntityWithId other)
        {		
            return ReferenceEquals(this, other)
                || (other as object) != null
				&& Id != default(int)
                && Id == other.Id
		    ;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityWithId left, EntityWithId right)
        {
            return (left as object) == null ? right.Equals(left) : left.Equals(right);
        }

        public static bool operator !=(EntityWithId left, EntityWithId right)
        {
            return !(left == right);
        }
        #endregion
    }
}
