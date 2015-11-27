using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class User_Skill : Base<User_Skill>
    {
        private Guid _skillId;
        public Skill Skill
        {
            get
            {
                return Skill.Items.Where(items => items.Id == _skillId).FirstOrDefault();
            }
            set
            {
                _skillId = value.Id;
            }
        }

        private Guid _userId;
        public User User
        {
            get
            {
                return User.Items.Where(items => items.Id == _skillId).FirstOrDefault();
            }
            set
            {
                _userId = value.Id;
            }
        }
    }
}
