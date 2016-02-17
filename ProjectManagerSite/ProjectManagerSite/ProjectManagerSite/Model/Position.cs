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

        public IEnumerable<User_Team> UsersInTeams
        {
            get { return User_Team.Items.Where(items => items.Position.Id == Id); }
        }

        public IEnumerable<User_Project> UsersInProjects
        {
            get { return User_Project.Items.Where(items => items.Position.Id == Id); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
