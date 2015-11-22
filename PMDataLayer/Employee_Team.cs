using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Employee_Team : Base<Employee_Team>
    {
        public bool IsLeader { get; set; }
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
    }
}
