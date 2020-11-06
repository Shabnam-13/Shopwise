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
    public class SettingController : Controller
    {
        // GET: Admin/Setting
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Setting> settings = db.Setting.ToList();
            return View(settings);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Setting setting)
        {
            if (ModelState.IsValid)
            {
                if (setting.LogoDarkFile == null)
                {
                    ModelState.AddModelError("", "Logo Dark is required");
                    return View(setting);
                }
                else
                {
                    string darkName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + setting.LogoDarkFile.FileName;
                    string darkPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), darkName);

                    setting.LogoDarkFile.SaveAs(darkPath);
                    setting.LogoDark = darkName;
                }

                if (setting.LogoLightFile == null)
                {
                    ModelState.AddModelError("", "Logo Light is required");
                    return View(setting);
                }
                else
                {
                    string imgName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + setting.LogoLightFile.FileName;
                    string imgPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), imgName);

                    setting.LogoLightFile.SaveAs(imgPath);
                    setting.LogoLight = imgName;
                }

                if (setting.AboutImgFile != null)
                {
                    string imgName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + setting.AboutImgFile.FileName;
                    string imgPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), imgName);

                    setting.AboutImgFile.SaveAs(imgPath);
                    setting.AboutImg = imgName;
                }

                if (setting.HomeImgFile != null)
                {
                    string imgName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + setting.HomeImgFile.FileName;
                    string imgPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), imgName);

                    setting.HomeImgFile.SaveAs(imgPath);
                    setting.HomeImg = imgName;
                }

                db.Setting.Add(setting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Update(int? id)
        {
            Setting setting = db.Setting.Find(id);
            if (setting == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(setting);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Setting setting)
        {
            if (ModelState.IsValid)
            {
                Setting Setting = db.Setting.Find(setting.Id);
                if (setting.LogoDarkFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + setting.LogoDarkFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), imageName);

                    string oldPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), Setting.LogoDark);
                    System.IO.File.Delete(oldPath);

                    setting.LogoDarkFile.SaveAs(imagePath);
                    Setting.LogoDark = imageName;
                }

                if (setting.LogoLightFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + setting.LogoLightFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), imageName);

                    string oldPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), Setting.LogoLight);
                    System.IO.File.Delete(oldPath);

                    setting.LogoLightFile.SaveAs(imagePath);
                    Setting.LogoLight = imageName;
                }

                if (setting.AboutImgFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + setting.AboutImgFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), imageName);

                    if (setting.AboutImg != null)
                    {
                        string oldPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), Setting.AboutImg);
                        System.IO.File.Delete(oldPath);
                    }
                    
                    setting.AboutImgFile.SaveAs(imagePath);
                    Setting.AboutImg = imageName;
                }

                if (setting.HomeImgFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + setting.HomeImgFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), imageName);

                    if (setting.HomeImg != null)
                    {
                        string oldPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), Setting.HomeImg);
                        System.IO.File.Delete(oldPath);
                    }

                    setting.HomeImgFile.SaveAs(imagePath);
                    Setting.HomeImg = imageName;
                }

                Setting.AboutTitle = setting.AboutTitle;
                Setting.AboutSubtitle = setting.AboutSubtitle;
                Setting.AboutContent = setting.AboutContent;
                Setting.VideoLink = setting.VideoLink;
                Setting.Copyright = setting.Copyright;

                db.Entry(Setting).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(setting);
        }

        public ActionResult Delete(int? id)
        {
            Setting setting = db.Setting.Find(id);
            if (setting == null)
            {
                return RedirectToAction("Error", "Home");
            }

            string oldDarkPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), setting.LogoDark);
            System.IO.File.Delete(oldDarkPath);

            string oldLightPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), setting.LogoLight);
            System.IO.File.Delete(oldLightPath);

            if (setting.AboutImg != null)
            {
                string oldAboutPath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), setting.AboutImg);
                System.IO.File.Delete(oldAboutPath);
            }

            if (setting.HomeImg != null)
            {
                string oldHomePath = Path.Combine(Server.MapPath("~/Uploads/Setting/"), setting.HomeImg);
                System.IO.File.Delete(oldHomePath);
            }
            db.Setting.Remove(setting);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}