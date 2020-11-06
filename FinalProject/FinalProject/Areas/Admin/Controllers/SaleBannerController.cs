using FinalProject.Areas.Admin.Filters;
using FinalProject.DAL;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class SaleBannerController : Controller
    {
        // GET: Admin/SaleBanner
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<SaleBanner> saleBanners = db.SaleBanner.ToList();
            return View(saleBanners);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SaleBanner sale)
        {
            if (ModelState.IsValid)
            {
                if (sale.ImageFile == null)
                {
                    ModelState.AddModelError("", "Image is required");
                    return View(sale);
                }
                else
                {
                    string darkName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + sale.ImageFile.FileName;
                    string darkPath = Path.Combine(Server.MapPath("~/Uploads/SaleBanner/"), darkName);

                    sale.ImageFile.SaveAs(darkPath);
                    sale.Image = darkName;
                }
                db.SaleBanner.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sale);
        }

        public ActionResult Update(int? id)
        {
            SaleBanner sale = db.SaleBanner.Find(id);
            if (sale == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(sale);
        }

        [HttpPost]
        public ActionResult Update(SaleBanner sale)
        {
            if (ModelState.IsValid)
            {
                SaleBanner Sale = db.SaleBanner.Find(sale.Id);
                if (sale.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + sale.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/SaleBanner/"), imageName);

                    string oldPath = Path.Combine(Server.MapPath("~/Uploads/SaleBanner/"), Sale.Image);
                    System.IO.File.Delete(oldPath);

                    sale.ImageFile  .SaveAs(imagePath);
                    Sale.Image = imageName;
                }
                Sale.Name = sale.Name;
                Sale.Title = sale.Title;
                Sale.Content = sale.Content;
                Sale.StartDate = sale.StartDate;
                Sale.EndDate = sale.EndDate;
                db.Entry(Sale).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sale);
        }

        public ActionResult Delete(int? id)
        {
            SaleBanner sale = db.SaleBanner.Find(id);
            if (sale == null)
            {
                return RedirectToAction("Error", "Home");
            }
            if (sale.Image != null)
            {
                string oldAboutPath = Path.Combine(Server.MapPath("~/Uploads/SaleBanner/"), sale.Image);
                System.IO.File.Delete(oldAboutPath);
            }
            db.SaleBanner.Remove(sale);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}