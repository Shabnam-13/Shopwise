using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required,MaxLength(250)]
        public string Address { get; set; }

        [Required,MaxLength(250)]
        public string IframeLink { get; set; }

        [Required,MaxLength(250)]
        public string Phone { get; set; }

        [Required,MaxLength(250)]
        public string Email { get; set; }
    }
}