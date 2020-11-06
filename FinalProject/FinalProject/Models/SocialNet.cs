using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class SocialNet
    {
        public int Id { get; set; }

        [Required,MaxLength(50)]
        public string Name { get; set; }

        [Required,MaxLength(250)]
        public string Link { get; set; }

        [Required,MaxLength(50)]
        public string IconClass { get; set; }
    }
}