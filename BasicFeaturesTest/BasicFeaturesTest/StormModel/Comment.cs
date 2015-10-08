//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace BasicFeaturesTest.StormModel
{
    using System;
    using System.Linq;

    public partial class Comment : IEquatable<Comment>, ICloneable<Comment>, IHaveId, IDbEntity
    {
        public int CommentId { get;set; }

        public int CommentType { get;set; }

        public int? PolicyId { get;set; }

        public int? PremiumId { get;set; }

        public string CommentText { get;set; }

        public int AuthorUserId { get;set; }

        public DateTime Created { get;set; }

        public DateTime Updated { get;set; }

        #region Navigation properties
        #endregion

        #region Private fields
        private readonly ILoadService loadService;
        IQueryable<Comment> sourceQuery;
        private readonly Comment clonedFrom;
        #endregion

        #region Constructors
        public Comment(Comment clonedFrom, IQueryable<Comment> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Comment(IQueryable<Comment> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Comment()
        {
        }
        #endregion

        #region ICloneable implementation
        Comment ICloneable<Comment>.Clone()
        {
            return new Comment(this, sourceQuery, loadService)
            {
                CommentId = CommentId,
                CommentType = CommentType,
                PolicyId = PolicyId,
                PremiumId = PremiumId,
                CommentText = CommentText,
                AuthorUserId = AuthorUserId,
                Created = Created,
                Updated = Updated,
            };
        }

        Comment ICloneable<Comment>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<Comment>.GetPopulated()
        {
            return null;
        }
        #endregion

        #region Equality members
        public override bool Equals(object obj)
        {
            return Equals(obj as Comment);
        }

        public bool Equals(Comment other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return CommentId == other.CommentId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return CommentId.GetHashCode();
            }
        }

        public static bool operator ==(Comment left, Comment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Comment left, Comment right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
