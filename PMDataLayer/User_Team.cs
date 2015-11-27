using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class User_Team : Base<User_Team>
    {
        public bool IsLeader { get; set; }
        private Guid _teamId;
        public Team Team
        {
            get
            {
                return Team.Items.Where(items => items.Id == _teamId).FirstOrDefault();
            }
            set
            {
                _teamId = value.Id;
            }
        }

        private Guid _userId;
        public User User
        {
            get
            {
                return User.Items.Where(items => items.Id == _userId).FirstOrDefault();
            }
            set
            {
                _userId = value.Id;
            }
        }

        private Guid _positionId;
        public Position Postion
        {
            get
            {
                return Position.Items.Where(items => items.Id == _positionId).FirstOrDefault();
            }
            set
            {
                _positionId = value.Id;
            }
        }
    }
}
