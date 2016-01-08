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
    public class TeamVM : BaseVM
    {
        private Team _team;

        public TeamVM(Team team)
        {
            _team = team;
        }

        public TeamVM()
        {

        }

        public Team Team
        {
            get { return _team; }
            set { _team = value; }
        }

        public string Name
        {
            get { return _team.Name; }
            set { _team.Name = value; }
        }

        public string Description
        {
            get { return _team.Description; }
            set { _team.Description = value; }
        }

        public IEnumerable<Project> Projects
        {
            get { return _team.Projects; }
        }

        public IEnumerable<User_Team> Users
        {
            get { return from items in User_Team.Items where items.Team.Id == _team.Id select items; }
        }

        public override string ToString()
        {
            return _team.ToString();
        }
    }
}
