using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Subcategory
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [MaxLength(50)]
        public string IconClass { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<SubOfSubcategory> SubOfSubcategories { get; set; }
    }
}