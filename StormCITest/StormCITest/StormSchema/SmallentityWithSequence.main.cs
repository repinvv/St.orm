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

    public partial class SmallentityWithSequence : IEquatable<SmallentityWithSequence>
    {	
        public int Id { get; set; }

        public string AChar { get; set; }

        public string AVarchar { get; set; }

        public string AText { get; set; }

        #region equality
        public override bool Equals(object obj)
        {
            return this == obj as SmallentityWithSequence;		  
        }
			  
        public bool Equals(SmallentityWithSequence other)
        {		
		    return this == other;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(SmallentityWithSequence left, SmallentityWithSequence right)
        {
            return ReferenceEquals(left, right)
                || (left as object) != null 
                && (right as object) != null
                && left.Id != default(int)
                && left.Id == right.Id
		    ;
        }

        public static bool operator !=(SmallentityWithSequence left, SmallentityWithSequence right)
        {
            return !(left == right);
        }
        #endregion
    }
}
