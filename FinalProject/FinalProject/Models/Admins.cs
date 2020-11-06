using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Admins
    {
        public int Id { get; set; }

        [Required,MaxLength(20)]
        public string Name { get; set; }

        [Required,MaxLength(20)]
        public string Surname { get; set; }

        [Required,MaxLength(50)]
        public string Username { get; set; }

        [Required, MinLength(3),MaxLength(250)]
        public string Password { get; set; }

        [Required,MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}