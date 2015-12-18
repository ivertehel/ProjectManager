using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Team : Base<Team>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Project> Projects
        {
            get { return from items in Team_Project.Items where items.Team.Id == Id select items.Project; }
        }

        public IEnumerable<User_Team> Users
        {
            get { return from items in User_Team.Items where items.Team.Id == Id select items; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
