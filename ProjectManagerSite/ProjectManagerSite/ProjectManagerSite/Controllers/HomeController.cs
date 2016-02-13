using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMDataLayer;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using UserStore.BLL.Interfaces;
using ProjectManagerSite.ViewModels;

namespace ProjectManagerSite.Controllers
{
    [Authorize(Roles = "client")]
    public class HomeController : Controller
    {
        private HomeControllerVM _homeControllerVM = new HomeControllerVM();

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
            
            return View("MyProfile");
        }

        public ActionResult UserPage(string id)
        {
            var user = _homeControllerVM.GetUserByLogin(id);
            if (user != null)
            {
                return View("MyProfile", user);
            }
            else
                return HttpNotFound();
        }

        public ActionResult MyProfileEdit()
        {
            return PartialView("MyProfileEdit", PMDataLayer.User.Items[0]);
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