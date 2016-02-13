using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMDataLayer;

namespace ProjectManagerSite.ViewModels
{
    public class HomeControllerVM
    {
        public User GetUserByLogin(string login)
        {
            User.Update();
            return User.Items.FirstOrDefault(item => item.Login == login);
        }
    }
}
