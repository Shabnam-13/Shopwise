using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required,MaxLength(20)]
        public string Name { get; set; }

        [Required,MaxLength(20)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MinLength(8),MaxLength(250)]
        public string Password { get; set; }

        [Required,MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }
        
        [Column(TypeName = "bit")]
        public bool isRegistered { get; set; }

        [MaxLength(250)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<Address> Addresses { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Order> Orders { get; set; }
    }
}