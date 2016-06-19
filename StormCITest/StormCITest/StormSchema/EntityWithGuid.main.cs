//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Storm
{
	using System;

    public partial struct EntityWithGuid : IEquatable<EntityWithGuid>
    {	
        public Guid Id { get; set; }

        public float? AFloat { get; set; }

        public double AReal { get; set; }

        public DateTime? ADate { get; set; }

        public DateTime? ATime { get; set; }

        public DateTimeOffset? AOffset { get; set; }

        public DateTime? ADatetime { get; set; }

        public DateTime? ADatetime2 { get; set; }

        public DateTime? ASmalldatetime { get; set; }

        #region equality
        public override bool Equals(object obj)
        {
		    return obj is EntityWithGuid && Equals((EntityWithGuid)obj);
        }

        public bool Equals(EntityWithGuid other)
        {     
		    var equals = true   
                && Id != default(Guid)
                && Id == other.Id
			    ;
            return equals;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode();
            }
        }

        public static bool operator ==(EntityWithGuid left, EntityWithGuid right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EntityWithGuid left, EntityWithGuid right)
        {
            return !left.Equals(right);
        }
        #endregion
    }
}
