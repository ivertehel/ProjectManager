using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class User_Team : Entity<User_Team>
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

        public Position Position
        {
            get { return Position.Items.Where(items => items.Id == _positionId).FirstOrDefault(); }
            set { _positionId = value.Id; }
        }

        public IEnumerable<Position> Positions
        {
            get { return from items in User_Team.Items where items._userId == _userId select items.Position; }
        }

        public override string ToString()
        {
            return User.ToString();
        }

        public override bool Equals(object obj)
        {
            var item = obj as User_Team;
            if (item == null)
                return false;
            if (item.Id == Id)
                return true;
            return false;
        }
    }
}
