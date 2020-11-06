using FinalProject.Areas.Admin.Filters;
using FinalProject.DAL;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class UsersController : Controller
    {
        // GET: Admin/Admin
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<User> usr = db.User.ToList();
            return View(usr);
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Delete(int? id)
        {
            User usr = db.User.Find(id);
            if (usr == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.User.Remove(usr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}