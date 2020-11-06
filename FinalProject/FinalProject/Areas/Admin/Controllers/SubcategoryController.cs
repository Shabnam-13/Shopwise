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
    public class SubcategoryController : Controller
    {
        // GET: Admin/Subcategory
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            ViewBag.Category = db.Category.ToList();
            List<Subcategory> subcategories = db.Subcategory.ToList();
            return View(subcategories);
        }
        public ActionResult Create()
        {
            ViewBag.Category = db.Category.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                if (subcategory.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + subcategory.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Category/"), imageName);

                    subcategory.ImageFile.SaveAs(imagePath);
                    subcategory.Image = imageName;
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                    ViewBag.Category = db.Category.ToList();
                    return View(subcategory);
                }

                db.Subcategory.Add(subcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = db.Category.ToList();
            return View(subcategory);
        }

        public ActionResult Update(int? id)
        {
            Subcategory subcategory = db.Subcategory.Find(id);
            ViewBag.Category = db.Category.ToList();

            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        [HttpPost]
        public ActionResult Update(Subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                Subcategory subcategory1 = db.Subcategory.Find(subcategory.Id);

                if (subcategory.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + subcategory.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Category/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/Category/"), subcategory1.Image);
                    System.IO.File.Delete(oldImagePath);

                    subcategory.ImageFile.SaveAs(imagePath);
                    subcategory1.Image = imageName;
                }
                subcategory1.Name = subcategory.Name;
                subcategory1.IconClass = subcategory.IconClass;
                subcategory1.CategoryId = subcategory.CategoryId;

                db.Entry(subcategory1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subcategory);
        }

        public ActionResult Delete(int? id)
        {
            Subcategory subcategory = db.Subcategory.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }

            string oldPath = Path.Combine(Server.MapPath("~/Uploads/Category/"), subcategory.Image);
            System.IO.File.Delete(oldPath);

            db.Subcategory.Remove(subcategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}