using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Skill : Entity<Skill>
    {
        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column]
        public string Name { get; set; }

        public IEnumerable<User> Users
        {
            get { return from items in User_Skill.Items where items.Skill.Id == Id select items.User; }
        }

        public IEnumerable<Project> Projects
        {
            get { return from items in Projects_Skill.Items where items.Skill.Id == Id select items.Project; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
