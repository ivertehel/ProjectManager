using PMDataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMView.View.WrapperVM
{
    public class User_TeamVM : BaseVM
    {
        private Team _team;

        private User _user;

        private Position _position;

        private User_Team _userTeam;

        public User_TeamVM(User_Team  userTeam)
        {
            _userTeam = userTeam;
        }

        public Team Team
        {
            get { return _team; }
            set { _team = value; }
        }

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
