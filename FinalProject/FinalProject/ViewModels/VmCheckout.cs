using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmCheckout
    {
        public int Id { get; set; }

        [Required, MaxLength(15)]
        public string Phone { get; set; }

        [Required, MaxLength(50)]
        public string Country { get; set; }

        [Required, MaxLength(250)]
        public string Street { get; set; }

        [MaxLength(50)]
        public string AddressDetails { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        public int[] ProductId { get; set; }

        public int[] ProductCount { get; set; }
    }
}