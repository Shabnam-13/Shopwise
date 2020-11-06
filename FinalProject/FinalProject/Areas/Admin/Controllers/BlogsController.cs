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
    public class BlogsController : Controller
    {
        // GET: Admin/Blogs
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            ViewBag.Category = db.BlogCategory.ToList();
            List<Blog> blogs = db.Blog.Include("Admin").ToList();
            return View(blogs);
        }

        public ActionResult Create()
        {
            ViewBag.Category = db.BlogCategory.ToList();
            ViewBag.Tags = db.BlogTags.ToList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                Blog Blog = new Blog();
                Blog.Title = blog.Title;
                Blog.Quote = blog.Quote;
                Blog.Content = blog.Content;
                Blog.Read = 0;
                Blog.CategoryId = blog.CategoryId;
                Blog.AdminId = (int)Session["LoginnerId"];
                Blog.CreateDate = DateTime.Now;
                db.Blog.Add(Blog);
                db.SaveChanges();


                foreach (HttpPostedFileBase image in blog.ImageFile)
                {
                    string imgName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + image.FileName;
                    string imgPath = Path.Combine(Server.MapPath("~/Uploads/Blog/"), imgName);

                    image.SaveAs(imgPath);
                    BlogImages img = new BlogImages();
                    img.Name = imgName;
                    img.BlogId = Blog.Id;

                    db.BlogImages.Add(img);
                    db.SaveChanges();
                }
                foreach (var item in blog.TagId)
                {
                    Tag tag = new Tag();
                    tag.BlogId = Blog.Id;
                    tag.BlogTagsId = item;

                    db.Tag.Add(tag);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.Category = db.BlogCategory.ToList();
            return View(blog);
        }

        public ActionResult Details(int id)
        {
            Blog blog = db.Blog.Include("Admin").Include("Images")
                               .Include("Category")
                               .Include("Tags")
                               .Include("Tags.blogTags")
                               .FirstOrDefault(i => i.Id == id);
            if(blog == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(blog);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Category = db.BlogCategory.ToList();
            ViewBag.Tags = db.BlogTags.ToList();
            Blog blog = db.Blog.Include("Images")
                                .Include("Tags").Include("Tags.blogTags").FirstOrDefault(i=>i.Id==id);

            if (blog == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(blog);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Blog blog)
        {

            Blog blog1 = db.Blog.Find(blog.Id);
            if (ModelState.IsValid)
            {
                blog1.Title = blog.Title;
                blog1.CategoryId = blog.CategoryId;
                blog1.Quote = blog.Quote;
                blog1.Content = blog.Content;

                db.Entry(blog1).State = EntityState.Modified;

                //foreach (var item in blog.TagId)
                //{
                //    List<Tag> tags = db.Tag.Where(p => p.BlogId == blog.Id).ToList();
                //    foreach(var tag in tags)
                //    {
                //        tag.BlogTagsId = tag.Id;
                //        db.Entry(tag).State = EntityState.Modified;
                //        db.SaveChanges();
                //    }
                //}
                if (blog.ImageFile.Length > 1)
                {
                    List<BlogImages> imgs = db.BlogImages.Where(p => p.BlogId == blog.Id).ToList();

                    foreach (var img in imgs)
                    {
                        if (img != null)
                        {
                            string oldPath = Path.Combine(Server.MapPath("~/Uploads/Blog/"), img.Name);
                            System.IO.File.Delete(oldPath);

                            db.BlogImages.Remove(img);
                            db.SaveChanges();
                        }
                    }
                    foreach (HttpPostedFileBase image in blog.ImageFile)
                    {
                        if (image != null)
                        {
                            string imgName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + image.FileName;
                            string imgPath = Path.Combine(Server.MapPath("~/Uploads/Blog/"), imgName);

                            image.SaveAs(imgPath);

                            BlogImages img = new BlogImages();
                            img.Name = imgName;
                            img.BlogId = blog1.Id;

                            db.BlogImages.Add(img);
                            db.SaveChanges();
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product = db.Product.ToList();
            ViewBag.Color = db.Colors.ToList();
            ViewBag.Size = db.Size.ToList();
            return View(blog);
        }

        public ActionResult Delete(int? id)
        {
            Blog blog = db.Blog.Include("Images").Include("Tags")
                               .FirstOrDefault(i => i.Id == id);

            if (blog == null)
            {
                return RedirectToAction("Error", "Home");
            }
            foreach(var item in db.Tag.ToList())
            {
                if (item.BlogId == id)
                {
                    db.Tag.Remove(item);
                }
            }
            foreach (var item in db.BlogImages.ToList())
            {
                if (item.BlogId == id)
                {
                    string imgPath = Path.Combine(Server.MapPath("~/Uploads/Blog/"), item.Name);

                    System.IO.File.Delete(imgPath);
                    db.BlogImages.Remove(item);
                }
            }
            db.SaveChanges();

            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetTags()
        {
            var tag = db.BlogTags.Select(t=> new{
                t.Id,
                t.Name
            }).ToList(); 
            return Json(tag, JsonRequestBehavior.AllowGet);
        }
    }
}