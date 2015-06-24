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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using St.Orm;
    using St.Orm.Interfaces;

    [Table("model.comment")]
    public partial class Comment : ICloneable<Comment>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("comment_id", Order = 1)]
        public int CommentId { get;set; }

        [Column("comment_type", Order = 2)]
        public int CommentType { get;set; }

        [Column("policy_id", Order = 3)]
        public int? PolicyId { get;set; }

        [Column("premium_id", Order = 4)]
        public int? PremiumId { get;set; }

        [Required]
        [Column("comment_text", Order = 5)]
        public string CommentText { get;set; }

        [Column("author_user_id", Order = 6)]
        public int AuthorUserId { get;set; }

        [Column("created", Order = 7)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 8)]
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
    }
}
