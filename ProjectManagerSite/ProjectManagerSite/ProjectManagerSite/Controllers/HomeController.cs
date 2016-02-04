using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagerSite.Controllers
{
    public class HomeController : Controller
    {
        private string _activePage = "active";

        public ActionResult Index()
        {
            ViewBag.ProfilePage = _activePage;
            return View();
        }

        public ActionResult Teams()
        {
            ViewBag.TeamsPage = _activePage;
            return View();
        }

        public ActionResult Projects()
        {
            ViewBag.ProjectsPage = _activePage;
            return View();
        }

        public ActionResult Tasks()
        {
            ViewBag.TasksPage = _activePage;
            return View();
        }
    }
}