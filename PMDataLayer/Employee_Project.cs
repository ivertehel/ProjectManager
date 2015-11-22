using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Employee_Project : Base<Employee_Project>
    {

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


        private Guid _positionId;
        public PositionInWork PositionInWork
        {
            get
            {
                return (from items in PositionInWork.Items where items.Id == _positionId select items).FirstOrDefault();
            }
            set
            {
                _positionId = value.Id;
            }
        }
    }
}
