using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class Position : Entity<Position>
    {
        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }

        public IEnumerable<Users_Team> UsersInTeams
        {
            get { return Users_Team.Items.Where(items => items.Position.Id == Id); }
        }

        public IEnumerable<Users_Project> UsersInProjects
        {
            get { return Users_Project.Items.Where(items => items.Position.Id == Id); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
