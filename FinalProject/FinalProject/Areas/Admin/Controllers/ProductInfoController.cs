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
    public class ProductInfoController : Controller
    {
        // GET: Admin/ProductInfo
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            ViewBag.Product = db.Product.ToList();
            ViewBag.Color = db.Colors.ToList();
            ViewBag.Size = db.Size.ToList();
            ViewBag.Sale = db.SaleBanner.ToList();
            List<ProductInfo> productInfos = db.ProductInfos.ToList();
            return View(productInfos);
        }

        public ActionResult Create()
        {
            ViewBag.Product = db.Product.ToList();
            ViewBag.Color = db.Colors.ToList();
            ViewBag.Size = db.Size.ToList();
            ViewBag.Sale = db.SaleBanner.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductInfo pro)
        {
            if (ModelState.IsValid)
            {
                ProductInfo product = new ProductInfo();
                if (pro.ColorId != null)
                {
                    product.ColorId = pro.ColorId;
                }
                product.Price = pro.Price;
                if (pro.SizeId != null)
                {
                    product.SizeId = pro.SizeId;
                }
                product.SalePercent = pro.SalePercent;
                if (pro.SaleBannerId != null)
                {
                    product.SaleBannerId = pro.SaleBannerId;
                }
                product.ProductId = pro.ProductId;
                product.isTopSelling = pro.isTopSelling;
                product.isNew = pro.isNew;
                product.Quantity = pro.Quantity;
                product.CreateDate = DateTime.Now;
                db.ProductInfos.Add(product);
                db.SaveChanges();

                foreach (HttpPostedFileBase image in pro.ImageFile)
                {
                    string imgName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + image.FileName;
                    string imgPath = Path.Combine(Server.MapPath("~/Uploads/Product/"), imgName);

                    image.SaveAs(imgPath);
                    ProductImages img = new ProductImages();
                    img.Name = imgName;
                    img.ProductInfoId = product.Id;

                    db.ProductImages.Add(img);
                    db.SaveChanges();
                }

                for (int i = 0; i < pro.SizeOptKey.Length; i++)
                {
                    SizeOption option = new SizeOption();
                    option.productInfoId = product.Id;
                    option.Key = pro.SizeOptKey[i];
                    option.Value = pro.SizeOptValue[i];
                    db.SizeOption.Add(option);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.Product = db.Product.ToList();
            ViewBag.Color = db.Colors.ToList();
            ViewBag.Size = db.Size.ToList();
            ViewBag.Sale = db.SaleBanner.ToList();

            return View(pro);
        }

        public ActionResult Details(int id)
        {
            ProductInfo pro = db.ProductInfos.Include("Product").Include("SaleBanner")
                               .Include("Images")
                               .Include("sizeOptions")
                               .Include("Size")
                               .Include("Color")
                               .FirstOrDefault(i => i.Id == id);
            if (pro == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(pro);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Product = db.Product.ToList();
            ViewBag.Color = db.Colors.ToList();
            ViewBag.Size = db.Size.ToList();
            ViewBag.Sale = db.SaleBanner.ToList();

            ProductInfo pro = db.ProductInfos.Include("Images").Include("sizeOptions")
                                .Include("Color").Include("Size").Include("Product").Include("SaleBanner").FirstOrDefault(i => i.Id == id);

            if (pro == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(pro);
        }

        [HttpPost]
        public ActionResult Edit(ProductInfo product)
        {
            if (ModelState.IsValid)
            {
                ProductInfo pro = db.ProductInfos.Find(product.Id);

                if (pro.ColorId != null)
                {
                    product.ColorId = pro.ColorId;
                }
                pro.Price = product.Price;
                if (pro.SizeId != null)
                {
                    product.SizeId = pro.SizeId;
                }
                pro.SalePercent = product.SalePercent;
                if (product.SaleBannerId != null)
                {
                    pro.SaleBannerId = product.SaleBannerId;
                }
                pro.ProductId = product.ProductId;
                pro.isTopSelling = product.isTopSelling;
                pro.isNew = product.isNew;
                pro.Quantity = product.Quantity;

                db.Entry(pro).State = EntityState.Modified;

                List<SizeOption> options = db.SizeOption.Where(p => p.productInfoId == pro.Id).ToList();

                //foreach(var opt in options)
                //{
                //    for (int i = 0; i < pro.SizeOptKey.Length; i++)
                //    {
                //        SizeOption option = new SizeOption();
                //        option.productInfoId = product.Id;
                //        option.Key = pro.SizeOptKey[i];
                //        option.Value = pro.SizeOptValue[i];
                //        db.SizeOption.Add(option);
                //        db.SaveChanges();
                //    }
                //}

                if (product.ImageFile.Length > 1)
                {
                    List<ProductImages> imgs = db.ProductImages.Where(p => p.ProductInfoId == pro.Id).ToList();

                    foreach(var img in imgs)
                    {
                        if (img != null)
                        {
                            string oldPath = Path.Combine(Server.MapPath("~/Uploads/Product/"), img.Name);
                            System.IO.File.Delete(oldPath);

                            db.ProductImages.Remove(img);
                            db.SaveChanges();
                        }
                    }

                    foreach (HttpPostedFileBase image in product.ImageFile)
                    {
                        if (image != null)
                        {
                            string imgName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + image.FileName;
                            string imgPath = Path.Combine(Server.MapPath("~/Uploads/Product/"), imgName);

                            image.SaveAs(imgPath);

                            ProductImages img = new ProductImages();
                            img.Name = imgName;
                            img.ProductInfoId = product.Id;

                            db.ProductImages.Add(img);
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
            ViewBag.Sale = db.SaleBanner.ToList();

            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            ProductInfo product = db.ProductInfos.Include("Images").Include("sizeOptions")
                               .FirstOrDefault(i => i.Id == id);

            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }
            foreach (var item in db.SizeOption.ToList())
            {
                if (item.productInfoId == id)
                {
                    db.SizeOption.Remove(item);
                }
            }
            foreach (var item in db.ProductImages.ToList())
            {
                if (item.ProductInfoId == id)
                {
                    string imgPath = Path.Combine(Server.MapPath("~/Uploads/Product/"), item.Name);

                    System.IO.File.Delete(imgPath);
                    db.ProductImages.Remove(item);
                }
            }
            db.SaveChanges();

            db.ProductInfos.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetSize()
        {
            var size = db.Size.Select(t => new {
                t.Id,
                t.Name
            }).ToList();
            return Json(size, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaleBanner()
        {
            var sale = db.SaleBanner.Select(t => new {
                t.Id,
                t.Name
            }).ToList();
            return Json(sale, JsonRequestBehavior.AllowGet);
        }
    }
}