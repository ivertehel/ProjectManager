using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Users_Skill : Entity<Users_Skill>
    {
        private Guid _skillId;

        private Guid _userId;

        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column]
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        [Column]
        public Guid SkillId
        {
            get { return _skillId; }
            set { _skillId = value; }
        }

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
