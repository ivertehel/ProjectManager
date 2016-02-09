using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProjectManagerSite.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectManagerSite.Filters;

namespace ProjectManagerSite.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (PMDataLayer.User.Items.FirstOrDefault(item => item.Email == model.Email && item.Password == model.Password) != null)
            {
                PMSession session = new PMSession();
                session.SaveCookies(model.Email, model.RememberMe, Response);
                PMSession.Insert(session);
            }

            return View();
        }
        
    }
}