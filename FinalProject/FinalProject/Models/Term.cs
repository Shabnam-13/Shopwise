using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Term
    {
        public int Id { get; set; }

        [Required,MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}