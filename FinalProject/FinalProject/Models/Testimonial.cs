using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required,MaxLength(50)]
        public string FullName { get; set; }

        [Required,MaxLength(50)]
        public string Position { get; set; }

        [Required,MaxLength(500)]
        public string Comment { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}