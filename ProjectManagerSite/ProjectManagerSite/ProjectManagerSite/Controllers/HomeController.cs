using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMDataLayer;
using Microsoft.AspNet.Identity.Owin;
using UserStore.BLL.Interfaces;

namespace ProjectManagerSite.Controllers
{
    [Authorize(Roles = "client")]
    public class HomeController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public ActionResult Index()
        {
            return View("MyProfile");
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