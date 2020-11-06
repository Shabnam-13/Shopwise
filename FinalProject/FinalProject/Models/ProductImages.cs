using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class ProductImages
    {
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        public int ProductInfoId { get; set; }

        public ProductInfo ProductInfo { get; set; }
    }
}