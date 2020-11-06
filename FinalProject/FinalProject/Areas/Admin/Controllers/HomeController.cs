using FinalProject.DAL;
using FinalProject.Areas.Admin.Filters;
using FinalProject.Models;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using FinalProject.Areas.Admin.ViewModel;

namespace FinalProject.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        ShopwiseDB db = new ShopwiseDB();

        [Logout]
        public ActionResult Index()
        {
            VmDashboard model = new VmDashboard();
            model.Blogs = db.Blog.ToList();
            model.BlogCategories = db.BlogCategory.ToList();
            model.Tags = db.BlogTags.ToList();
            model.Comments = db.Comment.ToList();
            model.Testimonials = db.Testimonial.ToList();
            model.Subscribes = db.Subscribe.ToList();
            model.Messages = db.Message.ToList();
            return View(model);
        }

        [Logout]
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VMLogin login)
        {
            if (ModelState.IsValid)
            {
                Admins admin = db.Admins.FirstOrDefault(u => u.Username == login.Username);

                if (admin != null)
                {
                    if (Crypto.VerifyHashedPassword(admin.Password, login.Password))
                    {
                        Session["Loginner"] = admin;
                        Session["LoginnerId"] = admin.Id;

                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect Password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username");
                }
            }

            return View(login);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Admins admin)
        {
            if (ModelState.IsValid)
            {
                if (db.Admins.Any(a => a.Email == admin.Email))
                {
                    ModelState.AddModelError("", "This email already exists");
                    return View(admin);
                }
                if (db.Admins.Any(a => a.Username == admin.Username))
                {
                    ModelState.AddModelError("", "This username already exists");
                    return View(admin);
                }
                Admins adm = new Admins();
                adm.Name = admin.Name;
                adm.Surname = admin.Surname;
                adm.Email = admin.Email;
                adm.Username = admin.Username;
                adm.Password = Crypto.HashPassword(admin.Password);
                adm.CreatedDate = DateTime.Now;

                db.Admins.Add(adm);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(admin);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}