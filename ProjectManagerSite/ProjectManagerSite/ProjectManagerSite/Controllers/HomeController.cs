using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using UserStore.BLL.Interfaces;
using ProjectManagerSite.ViewModels;

namespace ProjectManagerSite.Controllers
{
    [Authorize(Roles = "client")]
    public class HomeController : Controller
    {
        private HomeControllerVM _homeControllerVM;

        private PMDataModel _model = new PMDataModel(); 

        private IUserService UserService
        {
            get
            {
                
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public HomeController()
        {
            _homeControllerVM = new HomeControllerVM(_model);
        }

        public ActionResult Index()
        {
            
            return View("MyProfile");
        }

        public ActionResult UserPage(string id)
        {
            Users user;

            if (id == string.Empty)
            {
                user = _homeControllerVM.GetUserById(new Guid(User.Identity.GetUserId()));
            }
            else
                user = _homeControllerVM.GetUserByLogin(id);

            if (user != null)
            {
                return View("MyProfile", user);
            }
            else
                return HttpNotFound();
        }

        public ActionResult MyProfileEdit()
        {
            return null;// PartialView("MyProfileEdit", PMDataLayer.User.Items[0]);
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