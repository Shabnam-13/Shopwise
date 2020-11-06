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
    public class CommentsController : Controller
    {
        // GET: Admin/Comments
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Comment> com = db.Comment.ToList();
            return View(com);
        }

        public ActionResult Delete(int? id)
        {
            Comment com = db.Comment.Find(id);
            if (com == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.Comment.Remove(com);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}