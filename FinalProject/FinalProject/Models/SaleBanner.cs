using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Models
{
    public class SaleBanner
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required,MaxLength(150)]
        public string Name { get; set; }

        [Required, MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<ProductInfo> productInfos { get; set; }
    }
}