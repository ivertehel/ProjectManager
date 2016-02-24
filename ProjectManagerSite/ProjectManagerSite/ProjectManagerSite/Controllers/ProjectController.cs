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
            return View("Order", model);
        }
    }
}