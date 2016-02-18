using ProjectManagerSite.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace ProjectManagerSite.Models
{
    public class UserVM : BaseVM
    {
        private Users _authorizeUser;

        public UserVM(IPrincipal user) : base(user)
        {
            _authorizeUser = User;
        }

        public Users AuthorizeUser
        {
            get
            {
                return _authorizeUser;
            }
        }
    }
}
