using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Required,MaxLength(1000)]
        public string Content { get; set; }

        public DateTime Createdate { get; set; }

        [ForeignKey("ParentComment")]
        public int? ParentCommentId { get; set; }

        public virtual  Comment ParentComment { get; set; }

        public virtual ICollection<Comment> Children  { get; set; }
    }
}