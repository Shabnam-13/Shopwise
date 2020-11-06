using FinalProject.DAL;
using FinalProject.Areas.Admin.Filters;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class PartnersController : Controller
    {
        // GET: Admin/Partners
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Partner> partners = db.Partner.ToList();
            return View(partners);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Partner partner)
        {
            if (ModelState.IsValid)
            {
                if (partner.ImageFile == null)
                {
                    ModelState.AddModelError("", "Image is required");
                    return View(partner);
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + partner.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Partner/"), imageName);

                    partner.ImageFile.SaveAs(imagePath);
                    partner.Image = imageName;
                }

                db.Partner.Add(partner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partner);
        }


        public ActionResult Update(int? id)
        {
            Partner partner = db.Partner.Find(id);
            if (partner == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(partner);
        }

        [HttpPost]
        public ActionResult Update(Partner partner)
        {
            if (ModelState.IsValid)
            {
                Partner Partner = db.Partner.Find(partner.Id);
                if (partner.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + partner.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Partner/"), imageName);

                    string oldPath = Path.Combine(Server.MapPath("~/Uploads/Partner/"), Partner.Image);
                    System.IO.File.Delete(oldPath);

                    partner.ImageFile.SaveAs(imagePath);
                    Partner.Image = imageName;
                }

                Partner.Name = partner.Name;

                db.Entry(Partner).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partner);
        }

        public ActionResult Delete(int? id)
        {
            Partner partner = db.Partner.Find(id);
            if (partner == null)
            {
                return RedirectToAction("Error", "Home");
            }

            string imgPath = Path.Combine(Server.MapPath("~/Uploads/Partner/"), partner.Image);
            System.IO.File.Delete(imgPath);

            db.Partner.Remove(partner);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}