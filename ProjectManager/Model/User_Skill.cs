using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class User_Skill : Entity<User_Skill>
    {
        private Guid _skillId;

        private Guid _userId;

        public Skill Skill
        {
            get { return Skill.Items.Where(items => items.Id == _skillId).FirstOrDefault(); }
            set { _skillId = value.Id; }
        }

        public User User
        {
            get { return User.Items.Where(items => items.Id == _userId).FirstOrDefault(); }
            set { _userId = value.Id; }
        }
    }
}
