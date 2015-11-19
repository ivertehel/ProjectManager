using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class Task : Base<Task>
    {
        public string Description { get; set; }
        public int Hours { get; set; }

        private Guid _employeeId;
        public Employee Employee
        {
            get
            {
                return (from items in Employee.Items where items.Id == _employeeId select items).FirstOrDefault();
            }
            set
            {
                _employeeId = value.Id;
            }
        }

        private Guid _statusId;
        public TaskStatus Status
        {
            get
            {
                return (from items in TaskStatus.Items where items.Id == _statusId select items).FirstOrDefault();
            }
            set
            {
                _statusId = value.Id;
            }
        }
    }
}
