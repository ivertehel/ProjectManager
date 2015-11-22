using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Employee: Base<Employee>
    {
        public string Status { get; set; }

        private Guid _userId;
        public User User
        {
            get
            {
                return (from items in User.Items where items.Id == _userId select items).FirstOrDefault();
            }
            set
            {
                _userId = value.Id;
            }
        }
        public IEnumerable<Team> Teams
        {
            get
            {
                return from items in Employee_Team.Items where items.Employee.Id == Id select items.Team;
            }
        }
        public IEnumerable<Skill> Skills
        {
            get
            {
                return from items in Employee_Skill.Items where items.Employee.Id == Id select items.Skill;
            }
        }
        public IEnumerable<PositionInCompany> PositionsInCompany
        {
            get
            {
                return from items in Employee_PositionInCompany.Items where items.Employee.Id == Id select items.PositionInCompany;
            }
        }
        public IEnumerable<Employee_Project> EmployeeInProjects
        {
            get
            {
                return from items in Employee_Project.Items where items.Employee.Id == Id select items;
            }
        }

        public IEnumerable<Employee_Task> EmployeeInTasks
        {
            get
            {
                return from items in Employee_Task.Items where items.Employee.Id == Id select items;
            }
        }
    }
}
