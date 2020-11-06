using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required,MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Quote { get; set; }

        [Required,MaxLength(5000)]
        public string Content { get; set; }

        public int Read { get; set; }

        public int AdminId { get; set; }

        public Admins Admin { get; set; }

        public int CategoryId { get; set; }

        public BlogCategory Category { get; set; }

        public DateTime CreateDate { get; set; }

        public List<Comment> Comments { get; set; }
        
        public List<BlogImages> Images { get; set; }
        
        public List<Tag> Tags { get; set; }

        [NotMapped]
        public int[] TagId { get; set; }

        [NotMapped]
        public  HttpPostedFileBase[] ImageFile { get; set; }
    }
}