using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class Employee : Base<Employee>
    {
        private Guid userId;
        public User User
        {
            get
            {
                return (from items in User.Items where items.Id == userId select items).FirstOrDefault();
            }
            set
            {
                userId = value.Id;
            }
        }

        private Guid _positionId;
        public Position Position
        {
            get
            {
                return (from items in Position.Items where items.Id == _positionId select items).FirstOrDefault();
            }
            set
            {
                _positionId = value.Id;
            }
        }


        private Guid _teamId;
        public Team Team
        {
            get
            {
                return (from items in Team.Items where items.Id == _teamId select items).FirstOrDefault();
            }
            set
            {
                _teamId = value.Id;
            }
        }
        public IEnumerable<Task> Tasks
        {
            get
            {
                return (from items in Task.Items where items.Employee.Id == Id select items);
            }
        }
    }
}
