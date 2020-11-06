using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.WebControls;

namespace FinalProject.Controllers
{
    public class BlogsController : Controller
    {
        // GET: Blogs
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];
            HttpCookie cookieCart = Request.Cookies["Cart"];
            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                ViewBag.Wishlist = wishlist;
            }
            if (cookieCart != null)
            {
                List<string> cart = Request.Cookies["Cart"].Value.Split(',').ToList();
                cart.RemoveAt(cart.Count - 1);
                ViewBag.CartCount = cart.Count;
                ViewBag.Cart = cart;
            }
            else
            {
                ViewBag.CartCount = 0;
            }
            #endregion
            VmBlog model = new VmBlog();
            model.Blogs = db.Blog.Include("Tags").Include("Tags.blogTags")
                                 .Include("Category").Include("Images")
                                 .Include("Comments").Include("Comments.Children")
                                 .OrderByDescending(i => i.Id).ToList();
            model.Categories = db.BlogCategory.ToList();
            model.BlogTags = db.BlogTags.ToList();
            model.Images = db.BlogImages.ToList();
            model.saleBanners = db.SaleBanner.Where(s => s.StartDate < DateTime.Now && s.EndDate > DateTime.Now).ToList();
            return View(model);
        }

        public ActionResult Details(int? id,int? commentId, bool? isReply)
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];
            HttpCookie cookieCart = Request.Cookies["Cart"];
            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                ViewBag.Wishlist = wishlist;
            }
            if (cookieCart != null)
            {
                List<string> cart = Request.Cookies["Cart"].Value.Split(',').ToList();
                cart.RemoveAt(cart.Count - 1);
                ViewBag.CartCount = cart.Count;
                ViewBag.Cart = cart;
            }
            else
            {
                ViewBag.CartCount = 0;
            }
            #endregion
            if (id != null)
            {
                VmBlog model = new VmBlog();
                model.Blogs = db.Blog.Include("Tags").Include("Images").Include("Comments")
                                     .Include("Tags.blogTags").Include("Comments.Children")
                                     .Include("Comments.User").OrderByDescending(i => i.Id).ToList();
                model.Categories = db.BlogCategory.ToList();
                model.BlogTags = db.BlogTags.ToList();
                model.comments = db.Comment.ToList();
                if (isReply != null && commentId != null)
                {
                    model.isReply = true;
                    model.CommentId = commentId;
                }
                model.Blog = db.Blog.Find(id);
                if (model.Blog == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                else
                {
                    model.Blog.Read++;
                    Blog post = db.Blog.Find(model.Blog.Id);
                    post.Read = model.Blog.Read;

                    db.Entry(post).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Categories(int? id)
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];
            HttpCookie cookieCart = Request.Cookies["Cart"];
            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                ViewBag.Wishlist = wishlist;
            }
            if (cookieCart != null)
            {
                List<string> cart = Request.Cookies["Cart"].Value.Split(',').ToList();
                cart.RemoveAt(cart.Count - 1);
                ViewBag.CartCount = cart.Count;
                ViewBag.Cart = cart;
            }
            else
            {
                ViewBag.CartCount = 0;
            }
            #endregion
            if (id != null)
            {
                VmBlog model = new VmBlog();
                model.Category = db.BlogCategory.Find(id);
                model.Blogs = db.Blog.Include("Tags").Include("Images")
                                     .Where(c => c.CategoryId == id).ToList();

                if (model.Category == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Tags(int? id)
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];
            HttpCookie cookieCart = Request.Cookies["Cart"];
            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                ViewBag.Wishlist = wishlist;
            }
            if (cookieCart != null)
            {
                List<string> cart = Request.Cookies["Cart"].Value.Split(',').ToList();
                cart.RemoveAt(cart.Count - 1);
                ViewBag.CartCount = cart.Count;
                ViewBag.Cart = cart;
            }
            else
            {
                ViewBag.CartCount = 0;
            }
            #endregion
            if (id != null)
            {
                VmBlog model = new VmBlog();
                model.blogTag = db.BlogTags.Find(id);
                model.Tag = db.Tag.FirstOrDefault(t => t.BlogTagsId == id);
                model.Blogs = db.Blog.Include("Tags").Include("Images")
                                     .Where(t => t.Tags.FirstOrDefault().BlogTagsId == id).ToList();
                if (model.blogTag == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Comments(VmComment comment)
        {
            if(!string.IsNullOrEmpty(comment.Email ) && !string.IsNullOrEmpty(comment.Name) && !string.IsNullOrEmpty(comment.Surname))
            {
                if (!string.IsNullOrEmpty(comment.Content))
                {
                    Comment Comment = new Comment();
                    User user = db.User.FirstOrDefault(e => e.Email == comment.Email);
                    if (user!=null)
                    {
                        Comment.UserId = user.Id;
                    }
                    else
                    {
                        User usr = new User();
                        usr.Name = comment.Name;
                        usr.Surname = comment.Surname;
                        usr.Email = comment.Email;
                        usr.CreatedDate = DateTime.Now;

                        db.User.Add(usr);
                        db.SaveChanges();

                        Comment.UserId = usr.Id;
                    }
                    if (comment.isReply == true && comment.CommentId!=null)
                    {
                        Comment.ParentCommentId = comment.CommentId;
                    }
                    Comment.BlogId = comment.ItemId;
                    Comment.Content = comment.Content;
                    Comment.Createdate = DateTime.Now;

                    db.Comment.Add(Comment);
                    db.SaveChanges();
                    Session["SuccessfullComment"] = true;
                    return RedirectToAction("Details", "Blogs", new { id = comment.ItemId });
                }
                Session["EmptyComment"] = true;
                return RedirectToAction("Details", "Blogs", new { id = comment.ItemId });
            }
            Session["EmptyEmailorNameorSurname"] = true;
            return RedirectToAction("Details", "Blogs", new { id = comment.ItemId });
        }
    }
}