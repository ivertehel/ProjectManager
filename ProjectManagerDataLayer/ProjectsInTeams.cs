using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class ProjectsInTeams
    {

        public Guid Id;
        public ProjectsInTeams()
        {
            Id = Guid.NewGuid();
        }

        private Guid _teamId;
        public Team Team
        {
            get
            {
                return (from items in Team.Items where items.Id == _teamId select items).FirstOrDefault();
            }
            set
            {
                _teamId = value.Id;
            }
        }
        private Guid _projectId;
        public Project Project
        {
            get
            {
                return (from items in Project.Items where items.Id == _projectId select items).FirstOrDefault();
            }
            set
            {
                _projectId = value.Id;
            }
        }
    }
}
