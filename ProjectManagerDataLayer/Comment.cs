using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class Comment : Base<Comment>
    {
        public string Message { get; set; }
        public string DateTime { get; set; }

        private Guid _projectId;
        public Project Project
        {
            get
            {
                return (from items in Project.Items where items.Id == _projectId select items).FirstOrDefault();
            }
            set
            {
                _projectId = value.Id;
            }
        }

        private Guid _fromUserId;
        public User User
        {
            get
            {
                return (from items in User.Items where items.Id == _fromUserId select items).FirstOrDefault();
            }
            set
            {
                _fromUserId = value.Id;
            }
        }
    }
}
