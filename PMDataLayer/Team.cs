using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Team : Base<Team>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Employee_Team> EmployeesInTeam
        {
            get
            {
                return from items in Employee_Team.Items where items.Team.Id == Id select items;
            }
        }
        public IEnumerable<Project> Projects
        {
            get
            {
                return from items in Team_Project.Items where items.Team.Id == Id select items.Project;
            }
        }
        public IEnumerable<Team_Task> Tasks
        {
            get
            {
                return from items in Team_Task.Items where items.Team.Id == Id select items;
            }
        }
    }
}
