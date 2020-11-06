using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Subscribe
    {
        public int Id { get; set; }

        [Required,MaxLength(250)]
        public string Email { get; set; }

        public DateTime SendDate { get; set; }
    }
}