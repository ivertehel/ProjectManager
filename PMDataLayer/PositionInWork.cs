using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class PositionInWork : Base<PositionInWork>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Employee_Project> EmployeeInProjects
        {
            get
            {
                return from items in Employee_Project.Items where items.PositionInWork.Id == Id select items;
            }
        }
        public IEnumerable<Employee_Team> EmployeeInTeamInProjects
        {
            get
            {
                return from items in EmployeeTeam_PositionInWork.Items where items.PositionInWork.Id == Id select items.EmployeeTeam;
            }
        }
    }
}
