using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmPass
    {
        [Required]
        public string oldPass { get; set; }

        [Required]
        public string newPass { get; set; }

        [Required]
        public string confirmPass { get; set; }

        public int UserId { get; set; }
    }
}