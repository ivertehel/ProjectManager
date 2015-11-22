using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Skill : Base<Skill>
    {
        public string Name { get; set; }
        public IEnumerable<Employee> Employees
        {
            get
            {
                return from items in Employee_Skill.Items where items.Skill.Id == Id select items.Employee;
            }
        }
        public IEnumerable<Project> Projects
        {
            get
            {
                return from items in Project_Skill.Items where items.Skill.Id == Id select items.Project;
            }
        }
    }
}
