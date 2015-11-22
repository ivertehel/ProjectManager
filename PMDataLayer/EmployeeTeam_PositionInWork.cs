using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class EmployeeTeam_PositionInWork : Base<EmployeeTeam_PositionInWork>
    {
        private Guid _employeeTeamId;
        public Employee_Team EmployeeTeam
        {
            get
            {
                return (from items in Employee_Team.Items where items.Id == _employeeTeamId select items).FirstOrDefault();
            }
            set
            {
                _employeeTeamId = value.Id;
            }
        }

        private Guid _positionInWorkId;
        public PositionInWork PositionInWork
        {
            get
            {
                return (from items in PositionInWork.Items where items.Id == _positionInWorkId select items).FirstOrDefault();
            }
            set
            {
                _positionInWorkId = value.Id;
            }
        }
    }
}
