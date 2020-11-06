using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmAddress
    {
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        public string DetailedAddress { get; set; }

        [Required]
        public string Zipcode { get; set; }

        public string isPrimary { get; set; }

        public int userId { get; set; }
    }
}