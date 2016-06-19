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

    public partial class EntityWithoutKey : IEquatable<EntityWithoutKey>
    {	
        public int Value { get; set; }

        public string Content { get; set; }

        #region equality
        public override bool Equals(object obj)
        {
            return Equals(obj as EntityWithoutKey);		  
        }
			  
        public bool Equals(EntityWithoutKey other)
        {		
            if (other == null) return false;
			var equals = ReferenceEquals(this, other)
                && Value == other.Value
                && Content == other.Content
			    ;
            return equals;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Value.GetHashCode();
                hash = (hash * 397) ^ Content.GetHashCode();
                return hash; 
            }
        }

        public static bool operator ==(EntityWithoutKey left, EntityWithoutKey right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(EntityWithoutKey left, EntityWithoutKey right)
        {
            return !(left == right);
        }
        #endregion
    }
}
