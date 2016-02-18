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

        public ActionResult Skills()
        {
            var skillVM = new SkillsVM(User);
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
            }

            if (model.User != null)
            {
                return View("Profile", model);
            }
            else
                return HttpNotFound();
        }

        public ActionResult MyProfileEdit()
        {
            var model = new BaseVM(User);

            return PartialView("MyProfileEdit", model.User);
        }

        [HttpPost]
        public ActionResult MyProfileEdit(Users user, SkillsVM skillVM)
        {
            
            var model = new UserVM(User);
            return View("Profile", model);
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
            ViewBag.TeamsPage = "active";
            return View("MyTeams");
        }

        public ActionResult MyProjects()
        {
            ViewBag.ProjectsPage = "active";
            return View("MyProjects");
        }

        public ActionResult MyTasks()
        {
            ViewBag.TasksPage = "active";
            return View("MyTasks");
        }
    }
}