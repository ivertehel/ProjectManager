using ProjectManagerSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagerSite.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LoadOrder(string OrderId)
        {
            var model = new OrderVM(User, OrderId);
            if (model.Order == null)
                return HttpNotFound();
            return View("Order", model);
        }

        public ActionResult LoadProject(string OrderId, string ProjectId, string ParrentProjectId)
        {
            var model = new ProjectVM(User, OrderId, ProjectId);
            if (model.CurrentProject == null)
                return HttpNotFound();
            return View("Project", model);
        }
    }
}