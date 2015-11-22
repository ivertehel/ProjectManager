using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Comment : Base<Comment>
    {
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

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

        private Guid _orderId;
        public Order Order
        {
            get
            {
                return (from items in Order.Items where items.Id == _orderId select items).FirstOrDefault();
            }
            set
            {
                _orderId = value.Id;
            }
        }


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

        private Guid _tasktId;
        public Task Task
        {
            get
            {
                return (from items in Task.Items where items.Id == _tasktId select items).FirstOrDefault();
            }
            set
            {
                _tasktId = value.Id;
            }
        }

        private Guid _reportId;
        public Report Report
        {
            get
            {
                return (from items in Report.Items where items.Id == _reportId select items).FirstOrDefault();
            }
            set
            {
                _reportId = value.Id;
            }
        }
    }
}
