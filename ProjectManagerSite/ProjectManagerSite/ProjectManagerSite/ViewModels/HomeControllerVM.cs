using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace ProjectManagerSite.ViewModels
{
    public class HomeControllerVM
    {
        private PMDataModel _model;

        public HomeControllerVM(PMDataModel model)
        {
            _model = model;
        }

        public Users GetUserByLogin(string login)
        {
            return _model.Users.FirstOrDefault(item => item.Login == login);
        }

        public Users GetUserById(Guid id)
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
    }
}
