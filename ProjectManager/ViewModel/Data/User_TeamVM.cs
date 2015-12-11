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
        private User_Team _userteam;

        public User_TeamVM(User_Team userteam)
        {
            _userteam = userteam;
        }

        public User_Team UserTeam
        {
            get { return _userteam; }
            set { _userteam = value; }
        }

        public Position Position
        {
            get
            {
                return _userteam.Position;
            }
            set
            {
                _userteam.Position = value;
                OnPropertyChanged("Position");
            }
        }

        public Team Team
        {
            get
            {
                return _userteam.Team;
            }
            set
            {
                _userteam.Team = value;
                OnPropertyChanged("Team");
            }
        }

        public User User
        {
            get
            {
                return _userteam.User;
            }
            set
            {
                _userteam.User = value;
                OnPropertyChanged("User");
            }
        }

        public IEnumerable<Position> Positions
        {
            get { return _userteam.Positions; }
        }
    }
}
