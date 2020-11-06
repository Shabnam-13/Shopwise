using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        [Required]
        public byte Rating { get; set; }

        public DateTime CreateDate { get; set; }
    }
}