using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ProjectManagerSite.EF;
using System.Security.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerSite.Models
{
    public class BaseVM
    {
        private Entities _model = new Entities();


        public Entities Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public Users User { get; set; }

        public BaseVM(IPrincipal user)
        {
            if (user == null)
                return;

            if (user.Identity.GetUserId() == null)
                User = null;
            else
                User = getUserById(new Guid(user.Identity.GetUserId()));
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

        public Users GetUserByLogin(string login)
        {
            return _model.Users.FirstOrDefault(item => item.Login == login);
        }
    }
}
