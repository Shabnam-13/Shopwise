using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ViewModels
{
    public class VmSearch
    {
        public List<Blog> Blogs { get; set; }
        public List<Product> Products { get; set; }

        public string Page { get; set; }
        public string SearchText { get; set; }
    }
}