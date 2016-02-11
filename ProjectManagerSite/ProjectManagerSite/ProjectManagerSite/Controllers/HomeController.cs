using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMDataLayer;

namespace ProjectManagerSite.Controllers
{
    public class HomeController : Controller
    {
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