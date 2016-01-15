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
    public class User_TeamVM : BaseVM
    {
        private User_Team _userTeam;

        public User_TeamVM()
        {

        }

        public User_TeamVM(User_Team userTeam)
        {
            _userTeam = userTeam;
        }

        public User_Team User_Team
        {
            get { return _userTeam; }
            set { _userTeam = value; }
        }

        public bool IsLeader { get; set; }

        public Team Team
        {
            get { return _userTeam.Team; }
            set { _userTeam.Team = value; }
        }

        public User User
        {
            get { return _userTeam.User; }
            set { _userTeam.User = value; }
        }

        public Position Position
        {
            get { return _userTeam.Position; }
            set { _userTeam.Position = value; }
        }

        public IEnumerable<Position> Positions
        {
            get { return _userTeam.Positions; }
        }

        public override string ToString()
        {
            return _userTeam.ToString();
        }
    }
}
