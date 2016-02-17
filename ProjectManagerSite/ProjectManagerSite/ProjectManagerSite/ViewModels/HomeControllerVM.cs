using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ProjectManagerSite.EF;
using System.Security.Principal;

namespace ProjectManagerSite.ViewModels
{
    public class HomeControllerVM
    {
        private Entities _model;

        public Users User { get; set; }

        public HomeControllerVM(Entities model, IPrincipal user)
        {
            _model = model;
            if (user.Identity.GetUserId() == null)
                User = null;
            else
                User = getUserById(new Guid(user.Identity.GetUserId()));
        }

        public Users GetUserByLogin(string login)
        {
            return _model.Users.FirstOrDefault(item => item.Login == login);
        }

        private Users getUserById(Guid id)
        {
            string stringId = id.ToString();
            var clientProfileUserId = _model.ClientProfiles.FirstOrDefault(item => item.Id == stringId)?.UserId;
            if (clientProfileUserId != null)
            {
                var cp = new Guid(clientProfileUserId);
                return _model.Users.FirstOrDefault(user => user.Id == cp);
            }
            else
                return null;
        }

        public List<string> GetSkills()
        {
            var userSkills = _model.Users_Skills.ToList().FindAll(item => item.UserId == User.Id);
            List<string> skills = new List<string>();
            foreach (var item in userSkills)
            {
                var s = _model.Skills.FirstOrDefault(skill => skill.Id == item.SkillId);
                if (s != null)
                    skills.Add(s.Name);
            }

            return skills;
        }
    }
}
