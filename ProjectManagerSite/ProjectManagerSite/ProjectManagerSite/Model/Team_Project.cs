using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class Team_Project : Entity<Team_Project>
    {
        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        private Guid _projectId;

        private Guid _teamId;

        [Column]
        public Guid TeamId
        {
            get { return _teamId; }
            set { _teamId = value; }
        }

        [Column]
        public Guid ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }

        public Project Project
        {
            get { return Project.Items.Where(items => items?.Id == _projectId).FirstOrDefault(); }
            set { _projectId = value.Id; }
        }

        public Team Team
        {
            get { return Team.Items.Where(items => items.Id == _teamId).FirstOrDefault(); }
            set { _teamId = value.Id; }
        }
    }
}
