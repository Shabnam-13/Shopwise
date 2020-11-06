using FinalProject.DAL;
using FinalProject.Areas.Admin.Filters;
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
    public class TestimonialController : Controller
    {
        // GET: Admin/Testimoial
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Testimonial> testimonials = db.Testimonial.ToList();
            return View(testimonials);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                Testimonial testimonial1 = new Testimonial();
                if (testimonial.ImageFile == null)
                {
                    ModelState.AddModelError("", "Image is required");
                    return View(testimonial);
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + testimonial.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Testimonial/"), imageName);

                    testimonial.ImageFile.SaveAs(imagePath);
                    testimonial1.Image = imageName;
                }

                testimonial1.FullName = testimonial.FullName;
                testimonial1.Position = testimonial.Position;
                testimonial1.Comment = testimonial.Comment;
                testimonial1.CreatedDate = DateTime.Now;

                db.Testimonial.Add(testimonial1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testimonial);
        }


        public ActionResult Update(int? id)
        {
            Testimonial testimonial = db.Testimonial.Find(id);
            if (testimonial == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(testimonial);
        }

        [HttpPost]
        public ActionResult Update(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                Testimonial Testimonial = db.Testimonial.Find(testimonial.Id);
                if (testimonial.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + testimonial.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Testimonial/"), imageName);

                    string oldPath = Path.Combine(Server.MapPath("~/Uploads/Testimonial/"), Testimonial.Image);
                    System.IO.File.Delete(oldPath);

                    testimonial.ImageFile.SaveAs(imagePath);
                    Testimonial.Image = imageName;
                }

                Testimonial.FullName = testimonial.FullName;
                Testimonial.Position = testimonial.Position;
                Testimonial.Comment = testimonial.Comment;

                db.Entry(Testimonial).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testimonial);
        }

        public ActionResult Delete(int? id)
        {
            Testimonial testimonial = db.Testimonial.Find(id);
            if (testimonial == null)
            {
                return RedirectToAction("Error", "Home");
            }

            string imgPath = Path.Combine(Server.MapPath("~/Uploads/Testimonial/"), testimonial.Image);
            System.IO.File.Delete(imgPath);

            db.Testimonial.Remove(testimonial);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}