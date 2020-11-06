using FinalProject.DAL;
using FinalProject.Areas.Admin.Filters;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class SocialNetsController : Controller
    {
        // GET: Admin/SocialNets
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<SocialNet> socials = db.SocialNet.ToList();
            return View(socials);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SocialNet socials)
        {
            if (ModelState.IsValid)
            {
                db.SocialNet.Add(socials);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socials);
        }

        public ActionResult Update(int? id)
        {
            SocialNet socials = db.SocialNet.Find(id);
            if (socials == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(socials);
        }

        [HttpPost]
        public ActionResult Update(SocialNet socials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socials).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socials);
        }

        public ActionResult Delete(int? id)
        {
            SocialNet socials = db.SocialNet.Find(id);
            if (socials == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.SocialNet.Remove(socials);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}