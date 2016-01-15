using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMDataLayer;

namespace PMView.View.WrapperVM
{
    public class User_ProjectVM : BaseVM
    {
        private User_Project _userProject;

        public User_ProjectVM(User_Project userProject)
        {
            _userProject = userProject;
        }

        public User_ProjectVM()
        {

        }

        public User_Project User_Project
        {
            get { return _userProject; }
            set { _userProject = value; }
        }

        public User User
        {
            get { return _userProject.User; }
            set { _userProject.User = value; }
        }

        public Project Project
        {
            get { return _userProject.Project; }
            set { _userProject.Project = value; }
        }

        public Position Position
        {
            get { return _userProject.Position; }
            set { _userProject.Position = value; }
        }

        public IEnumerable<Position> Positions
        {
            get { return _userProject.Positions; }
        }

        public override string ToString()
        {
            return _userProject.ToString();
        }
    }
}
