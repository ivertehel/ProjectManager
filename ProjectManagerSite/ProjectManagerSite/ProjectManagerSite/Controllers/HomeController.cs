using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using UserStore.BLL.Interfaces;
using ProjectManagerSite.EF;
using ProjectManagerSite.Models;
using System.IO;
using System.Net;

namespace ProjectManagerSite.Controllers
{
    [Authorize(Roles = "client, admin, employee")]
    public class HomeController : Controller
    {
        private Entities _model = new Entities();

        private IUserService UserService
        {
            get
            {

                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return RedirectToAction("UserPage");
        }

        public ActionResult Skills(string Login)
        {
            var skillVM = new SkillsVM(Login);
            return PartialView(skillVM.UserSkills);
        }

        public ActionResult UserPage(string login)
        {
            var model = new UserVM(User);
            if (login == string.Empty)
            {
                if (model.User == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                return Redirect(@"/" + model.User.Login);
            }
            else
            {
                model.User = model.GetUserByLogin(login);
                var dir = Directory.GetFiles(HttpContext.Server.MapPath("~/WebImages/"));
                var toDelete = (from items in dir where items.Contains(model.User.Id.ToString()) select items).ToList();
                for (int i = 0; i < toDelete.Count(); i++)
                {
                    System.IO.File.Delete(toDelete[i]);
                }
            }

            if (model.User != null)
            {
                return View("Profile", model);
            }
            else
                return HttpNotFound();
        }

        [HttpGet]
        public ActionResult MyProfileEdit()
        {
            return PartialView("MyProfileEdit", new UserVM(User));
        }

        [HttpPost]
        public ActionResult MyProfileEdit(UserVM user, SkillsVM skillVM, HttpPostedFileBase Image, string WebcamImage)
        {
            if (WebcamImage != "null" && WebcamImage != string.Empty)
            {
                var path = HttpContext.Server.MapPath("~/WebImages/" + WebcamImage + ".jpg");
                user.Avatar = System.IO.File.ReadAllBytes(path);
            }

            if (Image != null && !IsImage(Image))
            {
                return Json(new { result = "Error", message = "Wrong image" });
            }
            if (ModelState.IsValid)
            {

                if (Image != null)
                {
                    using (var binaryReader = new BinaryReader(Image.InputStream))
                    {
                        user.Avatar = binaryReader.ReadBytes(Image.ContentLength);
                    }
                }

                var model = new UserVM(User, user, skillVM);
                System.IO.File.Delete(HttpContext.Server.MapPath("~/WebImages/" + WebcamImage + ".jpg"));
                model.SaveChanges();
                return Json(new { result = "Redirect", url = "/" + model.User.Login });
            }



            return PartialView("MyProfileEdit", user);
        }

        public ActionResult ViewImage(string id)
        {
            UserVM item;
            try
            {
                item = new UserVM(id);
                byte[] buffer = item.Avatar;
                return File(buffer, "image/jpg", string.Format("{0}.jpg", id));
            }
            catch
            {
                return new EmptyResult();
            }
        }

        public ActionResult SkillsEdit()
        {
            var skillsVM = new SkillsVM(User);


            return PartialView(skillsVM);
        }

        public ActionResult MyProfileSave()
        {
            return View("Profile");
        }

        public ActionResult MyTeams()
        {
            return View("MyTeams");
        }

        public ActionResult MyProjects()
        {
            var model = new UserVM(User);
            return View("MyProjects", model);
        }

        public ActionResult MyTasks()
        {
            ViewBag.TasksPage = "active";
            return View("MyTasks");
        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".jpeg" }; // add more if u like...

            // linq from Henrik Stenbæk
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        [HttpPost]
        public ActionResult Capture(string imgId)
        {
            var stream = Request.InputStream;
            string dump;
            string path = string.Empty;
            using (var reader = new StreamReader(stream))
            {
                dump = reader.ReadToEnd();
                DateTime nm = DateTime.Now;
                string date = nm.ToString("yyyymmddMMss");
                path = Server.MapPath("~/WebImages/" + imgId + ".jpg");
                System.IO.File.WriteAllBytes(path, convertStringToBytes(dump));
            }
            return Json(new { result = "imagePath", imagePath = path });
        }

        private byte[] convertStringToBytes(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];
            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }
            return bytes;
        }
    }
}