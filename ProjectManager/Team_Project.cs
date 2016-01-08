using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Team_Project : Entity<Team_Project>
    {
        private Guid _projectId;

        private Guid _teamId;

        public Project Project
        {
            get { return Project.Items.Where(items => items.Id == _projectId).FirstOrDefault(); }
            set { _projectId = value.Id; }
        }

        public Team Team
        {
            get { return Team.Items.Where(items => items.Id == _teamId).FirstOrDefault(); }
            set { _teamId = value.Id; }
        }
    }
}
