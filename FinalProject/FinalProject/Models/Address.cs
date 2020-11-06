using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace FinalProject.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required,MaxLength(25)]
        public string Country { get; set; }

        [Required,MaxLength(25)]
        public string City { get; set; }

        [Required,MaxLength(250)]
        public string Street { get; set; }

        [MaxLength(500)]
        public string DetailedAddress { get; set; }

        [Required,MaxLength(10)]
        public string Zipcode { get; set; }

        [Column(TypeName = "bit")]
        public bool IsPrimary { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}