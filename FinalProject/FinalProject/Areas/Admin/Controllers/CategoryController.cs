using FinalProject.Areas.Admin.Filters;
using FinalProject.DAL;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Category> categories = db.Category.ToList();
            return View(categories);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + category.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Category/"), imageName);

                    category.ImageFile.SaveAs(imagePath);
                    category.Image = imageName;
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                    return View(category);
                }

                db.Category.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Update(int? id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                Category category1 = db.Category.Find(category.Id);

                if (category.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + category.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Category/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/Category/"), category1.Image);
                    System.IO.File.Delete(oldImagePath);

                    category.ImageFile.SaveAs(imagePath);
                    category1.Image = imageName;
                }
                category1.Name = category.Name;
                category1.IconClass = category.IconClass;

                db.Entry(category1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            string oldPath = Path.Combine(Server.MapPath("~/Uploads/Category/"), category.Image);
            System.IO.File.Delete(oldPath);

            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}