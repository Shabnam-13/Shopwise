using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmHome
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<SaleBanner> Sales { get; set; }
        public List<ProductInfo> productInfos { get; set; }
        public List<Blog> Blogs { get; set; }
        public Setting setting { get; set; }
    }
}