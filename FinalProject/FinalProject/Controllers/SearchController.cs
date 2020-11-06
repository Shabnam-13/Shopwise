using FinalProject.DAL;
using FinalProject.Migrations;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Blog()
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

            List<Blog> blogs = TempData["data"] as List<Blog>;
            return View(blogs);
        }

       public ActionResult Product()
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

            List<Product> pro = TempData["data"] as List<Product>;

            return View(pro);
        }

        [HttpPost]
        public ActionResult SearchBlog(VmSearch search)
        {
            if (search.SearchText != null)
            {
                if (search.Page == "Blog")
                {
                    List<Blog> blogs = new List<Blog>();
                    blogs = db.Blog.Include("Images")
                                   .Include("Tags")
                                   .Include("Comments")
                                   .Where(a => a.Title.Contains(search.SearchText)).ToList();

                    TempData["data"] = blogs;
                    return RedirectToAction("Blog");
                }
                else if (search.Page == "Product")
                {
                    List<Product> products = new List<Product>();
                    products = db.Product.Include("productInfos")
                                         .Include("productInfos.Images")
                                         .Where(a => a.Name.Contains(search.SearchText)).ToList();

                    TempData["data"] = products;
                    return RedirectToAction("AllResult");
                }
            }
            return View();
        }
    }
}