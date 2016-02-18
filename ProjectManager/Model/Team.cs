using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class Team : Entity<Team>
    {
        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }

        public IEnumerable<Project> Projects
        {
            get { return from items in Team_Project.Items where items.Team.Id == Id select items.Project; }
        }

        public IEnumerable<Users_Team> Users
        {
            get { return from items in Users_Team.Items where items.Team.Id == Id select items; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
