using FinalProject.Areas.Admin.Filters;
using FinalProject.DAL;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;


namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Admins> adm = db.Admins.ToList();
            return View(adm);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Admins admin)
        {
            if (ModelState.IsValid)
            {
                Admins adm = new Admins();
                if (admin.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + admin.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Admins/"), imageName);

                    admin.ImageFile.SaveAs(imagePath);
                    adm.Image = imageName;
                }
                adm.Name = admin.Name;
                adm.Surname = admin.Surname;
                adm.Email = admin.Email;
                adm.Username = admin.Username;
                adm.Password = Crypto.HashPassword(admin.Password);
                adm.CreatedDate = DateTime.Now;
                db.Admins.Add(adm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        public ActionResult Delete(int? id)
        {
            Admins adm = db.Admins.Find(id);
            if (adm == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.Admins.Remove(adm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}