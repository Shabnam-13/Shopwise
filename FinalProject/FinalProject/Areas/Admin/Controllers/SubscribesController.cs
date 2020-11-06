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
    public class SubscribesController : Controller
    {
        // GET: Admin/Subscribes
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Subscribe> subscribes = db.Subscribe.ToList();
            return View(subscribes);
        }

        public ActionResult Delete(int? id)
        {
            Subscribe subs = db.Subscribe.Find(id);
            if (subs == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.Subscribe.Remove(subs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}