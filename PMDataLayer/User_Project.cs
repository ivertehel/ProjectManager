using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class User_Project : Base<User_Project>
    {

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

        private Guid _projectId;
        public Project Project
        {
            get
            {
                return Project.Items.Where(items => items.Id == _projectId).FirstOrDefault();
            }
            set
            {
                _projectId = value.Id;
            }
        }


        private Guid _positionId;
        public Position Position
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
