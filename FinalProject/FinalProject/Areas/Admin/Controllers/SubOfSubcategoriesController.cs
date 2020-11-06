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
    public class SubOfSubcategoriesController : Controller
    {
        // GET: Admin/SubOfSubcategories
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            ViewBag.Subcategory = db.Subcategory.ToList();
            List<SubOfSubcategory> subofsubcategories = db.SubOfSubcategory.ToList();
            return View(subofsubcategories);
        }
        public ActionResult Create()
        {
            ViewBag.Subcategory = db.Subcategory.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubOfSubcategory subofsubcategories)
        {
            if (ModelState.IsValid)
            {
                if (subofsubcategories.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + subofsubcategories.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Category/"), imageName);

                    subofsubcategories.ImageFile.SaveAs(imagePath);
                    subofsubcategories.Image = imageName;
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                    ViewBag.Subcategory = db.Subcategory.ToList();
                    return View(subofsubcategories);
                }

                db.SubOfSubcategory.Add(subofsubcategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Subcategory = db.Subcategory.ToList();
            return View(subofsubcategories);
        }

        public ActionResult Update(int? id)
        {
            SubOfSubcategory subofsubcategories = db.SubOfSubcategory.Find(id);
            ViewBag.Subcategory = db.Subcategory.ToList();

            if (subofsubcategories == null)
            {
                return HttpNotFound();
            }
            return View(subofsubcategories);
        }

        [HttpPost]
        public ActionResult Update(SubOfSubcategory subofsubcategories)
        {
            if (ModelState.IsValid)
            {
                SubOfSubcategory subofsubcategories1 = db.SubOfSubcategory.Find(subofsubcategories.Id);

                if (subofsubcategories.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + subofsubcategories.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Category/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/Category/"), subofsubcategories1.Image);
                    System.IO.File.Delete(oldImagePath);

                    subofsubcategories.ImageFile.SaveAs(imagePath);
                    subofsubcategories1.Image = imageName;
                }
                subofsubcategories1.Name = subofsubcategories.Name;
                subofsubcategories1.IconClass = subofsubcategories.IconClass;
                subofsubcategories1.SubcategoryId = subofsubcategories.SubcategoryId;

                db.Entry(subofsubcategories1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subofsubcategories);
        }

        public ActionResult Delete(int? id)
        {
            SubOfSubcategory subofsubcategories = db.SubOfSubcategory.Find(id);
            if (subofsubcategories == null)
            {
                return HttpNotFound();
            }

            string oldPath = Path.Combine(Server.MapPath("~/Uploads/Category/"), subofsubcategories.Image);
            System.IO.File.Delete(oldPath);

            db.SubOfSubcategory.Remove(subofsubcategories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}