using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagerSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            return PartialView("MyProfile");
        }

        public ActionResult MyProfileEdit()
        {
            return PartialView("MyProfileEdit");
        }

        public ActionResult MyTeams()
        {
            return PartialView("MyTeams");
        }

        public ActionResult MyProjects()
        {
            return PartialView("MyProjects");
        }

        public ActionResult MyTasks()
        {
            return PartialView("MyTasks");
        }
    }
}