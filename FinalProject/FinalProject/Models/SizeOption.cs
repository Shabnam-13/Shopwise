using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class SizeOption
    {
        public int Id { get; set; }

        [Required,MaxLength(50)]
        public string Key { get; set; }

        [Required,MaxLength(50)]
        public string Value { get; set; }

        public int productInfoId { get; set; }

        public ProductInfo productInfo { get; set; }
    }
}