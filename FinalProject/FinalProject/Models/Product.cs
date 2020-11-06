using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(500)]
        public string SmallDesc { get; set; }

        [Required, MaxLength(1000)]
        public string megaDesc { get; set; }

        public DateTime CreateDate { get; set; }

        public List<Review> Reviews { get; set; }

        public List<ProductInfo> productInfos { get; set; }

        public List<ProCategory> ProCategories { get; set; }

        public int[] CategoryId { get; set; }
    }
}