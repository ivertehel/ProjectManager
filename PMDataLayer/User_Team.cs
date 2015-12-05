using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class User_Team : Base<User_Team>
    {
        private Guid _teamId;

        private Guid _userId;

        private Guid _positionId;

        public bool IsLeader { get; set; }

        public Team Team
        {
            get { return Team.Items.Where(items => items.Id == _teamId).FirstOrDefault(); }
            set { _teamId = value.Id; }
        }

        public User User
        {
            get { return User.Items.Where(items => items.Id == _userId).FirstOrDefault(); }
            set { _userId = value.Id; }
        }

        public Position Postion
        {
            get { return Position.Items.Where(items => items.Id == _positionId).FirstOrDefault(); }
            set { _positionId = value.Id; }
        }

        public override string ToString()
        {
            return User.ToString();
        }
    }
}
