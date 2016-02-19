using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Users_Project : Entity<Users_Project>
    {
        private Guid _userId;

        private Guid _projectId;

        private Guid _positionId;

        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column]
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        [Column]
        public Guid ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }

        [Column]
        public Guid PositionId
        { 
            get { return _positionId; }
            set { _positionId = value; }    
        }


        public User User
        {
            get { return User.Items.Where(items => items?.Id == _userId).FirstOrDefault(); }
            set { _userId = value.Id; }
        }

        public Project Project
        {
            get { return Project.Items.Where(items => items?.Id == _projectId).FirstOrDefault(); }
            set { _projectId = value.Id; }
        }

        public Position Position
        {
            get { return Position.Items.Where(items => items.Id == _positionId).FirstOrDefault(); }
            set { _positionId = value.Id; }
        }

        public IEnumerable<Position> Positions
        {
            get { return from items in Users_Project.Items where items.User.Id == _userId select items.Position; }
        }
    }
}
