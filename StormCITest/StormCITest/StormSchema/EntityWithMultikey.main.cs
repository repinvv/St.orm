//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject.StormSchema
{
	using System;

    public partial class EntityWithMultikey : IEquatable<EntityWithMultikey>
    {	
        public int Id1 { get; set; }

        public string Id2 { get; set; }

        public string Content { get; set; }

        #region equality
        public override bool Equals(object obj)
        {
            return this == obj as EntityWithMultikey;		  
        }
			  
        public bool Equals(EntityWithMultikey other)
        {		
		    return this == other;
        }

        public override int GetHashCode()
        {
            return new int[]
            {
                Id1.GetHashCode(),
                Id2.GetHashCode(),
            }.CombineHashcodes();
        }

        public static bool operator ==(EntityWithMultikey left, EntityWithMultikey right)
        {
            return ReferenceEquals(left, right)
                || (left as object) != null 
                && (right as object) != null
                && left.Id1 != default(int)
                && left.Id2 != string.Empty
                && left.Id1 == right.Id1
                && left.Id2 == right.Id2
		    ;
        }

        public static bool operator !=(EntityWithMultikey left, EntityWithMultikey right)
        {
            return !(left == right);
        }
        #endregion
    }
}
