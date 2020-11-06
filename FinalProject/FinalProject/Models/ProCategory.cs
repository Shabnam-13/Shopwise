using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class ProCategory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int SubOfSubcategoryId { get; set; }

        public SubOfSubcategory SubOfSubcategory { get; set; }
    }
}