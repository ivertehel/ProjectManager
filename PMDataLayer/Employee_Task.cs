using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Employee_Task : Base<Employee_Task>
    {
        public double Hours { get; set; }
        public string Description { get; set; }

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
