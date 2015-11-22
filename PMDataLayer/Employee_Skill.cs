using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Employee_Skill : Base<Employee_Skill>
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

        private Guid _skillId;
        public Skill Skill
        {
            get
            {
                return (from items in Skill.Items where items.Id == _skillId select items).FirstOrDefault();
            }
            set
            {
                _skillId = value.Id;
            }
        }
    }
}
