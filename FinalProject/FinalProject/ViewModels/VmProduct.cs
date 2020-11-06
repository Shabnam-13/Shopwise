using FinalProject.Areas.Admin.Controllers;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmProduct
    {
        public List<Product> Products { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public List<ProductInfo> ProductInfos { get; set; }
        public List<ProductImages> Images { get; set; }
        public List<SizeOption> sizeOptions { get; set; }
        public List<Category> categories { get; set; }
        public List<Subcategory> subcategories { get; set; }
        public List<SubOfSubcategory> SubOfSubcategories { get; set; }

        public List<Product> SeenPro { get; set; }
        public List<string> Ids { get; set; }

        public Product product { get; set; }
        public ProductInfo productInfo { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public SaleBanner SaleBanner { get; set; }
        public Category category { get; set; }
        public Subcategory subcat { get; set; }
        public SubOfSubcategory subOfsubcat { get; set; }
    }
}