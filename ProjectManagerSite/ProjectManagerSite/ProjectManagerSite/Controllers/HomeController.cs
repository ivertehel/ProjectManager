using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using UserStore.BLL.Interfaces;
using ProjectManagerSite.ViewModels;
using ProjectManagerSite.EF;
using ProjectManagerSite.Models;

namespace ProjectManagerSite.Controllers
{
    [Authorize(Roles = "client")]
    public class HomeController : Controller
    {
        private HomeControllerVM _homeControllerVM;

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
            _homeControllerVM = new HomeControllerVM(_model, User);
            return PartialView(_homeControllerVM.GetSkills());
        }

        public ActionResult UserPage(string id)
        {
            _homeControllerVM = new HomeControllerVM(_model, User);

            if (id == string.Empty)
            {
                return Redirect(@"/" + _homeControllerVM.User.Login);
            }
            else
            {
                _homeControllerVM.User = _homeControllerVM.GetUserByLogin(id);
            }

            if (_homeControllerVM.User != null)
            {
                return View("MyProfile", _homeControllerVM.User);
            }
            else
                return HttpNotFound();
        }

        public ActionResult MyProfileEdit()
        {
            _homeControllerVM = new HomeControllerVM(_model, User);
            return PartialView("MyProfileEdit", _homeControllerVM.User);
        }

        public ActionResult SkillsEdit()
        {
            _homeControllerVM = new HomeControllerVM(_model, User);
            ViewBag.User = User;
            var skillsVm = new SkillsVM(_model, _homeControllerVM.User);
            
                
            return PartialView("SkillEdit", null);
        }

        public ActionResult MyProfileSave()
        {
            return View("MyProfile");
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