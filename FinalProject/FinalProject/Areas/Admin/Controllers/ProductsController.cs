using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Areas.Admin.Filters;
using FinalProject.DAL;
using FinalProject.Models;

namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class ProductsController : Controller
    {
        private ShopwiseDB db = new ShopwiseDB();

        // GET: Admin/Products
        public ActionResult Index()
        {
            ViewBag.Category = db.SubOfSubcategory.ToList();

            return View(db.Product.Include("ProCategories").Include("ProCategories.SubOfSubcategory").ToList());
        }


        public ActionResult Create()
        {
            ViewBag.Category = db.SubOfSubcategory.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                Product pro = new Product();
                pro.Name = product.Name;
                pro.SmallDesc = product.SmallDesc;
                pro.megaDesc = product.megaDesc;
                pro.CreateDate = DateTime.Now;
                db.Product.Add(pro);
                db.SaveChanges();

                foreach (var item in product.CategoryId)
                {
                    ProCategory cat = new ProCategory();
                    cat.ProductId = pro.Id;
                    cat.SubOfSubcategoryId = item;

                    db.ProCategory.Add(cat);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.Category = db.SubOfSubcategory.ToList();
            return View(product);
        }


        public ActionResult Edit(int? id)
        {
            ViewBag.Category = db.SubOfSubcategory.ToList();
            Product product = db.Product.Include("ProCategories")
                .Include("ProCategories.SubOfSubcategory").FirstOrDefault(i => i.Id == id);

            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            Product pro = db.Product.Find(product.Id);
            if (ModelState.IsValid)
            {
                pro.Name = product.Name;
                pro.SmallDesc = product.SmallDesc;
                pro.megaDesc = product.megaDesc;
                db.Entry(pro).State = EntityState.Modified;

                ProCategory cat = db.ProCategory.FirstOrDefault(p => p.ProductId == product.Id);
                
                foreach (var item in product.CategoryId)
                {
                    cat.SubOfSubcategoryId = item;

                    db.Entry(cat).State = EntityState.Modified;
                }
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            ViewBag.Category = db.SubOfSubcategory.ToList();
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            Product product = db.Product.Include("ProCategories")
                               .FirstOrDefault(i => i.Id == id);

            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }
            foreach (var item in db.ProCategory.ToList())
            {
                if (item.ProductId == id)
                {
                    db.ProCategory.Remove(item);
                }
            }
            db.SaveChanges();

            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult getCategory()
        {
            var category = db.SubOfSubcategory.Select(t => new {
                t.Id,
                t.Name
            }).ToList();
            return Json(category, JsonRequestBehavior.AllowGet);
        }
    }
}
