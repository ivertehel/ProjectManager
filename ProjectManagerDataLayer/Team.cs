using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class Team : Base<Team>
    {
        public string Name;
        private Guid _teamLeadId;
        public Employee TeamLead
        {
            get
            {

                return (from items in Employee.Items where items.Id == _teamLeadId select items).FirstOrDefault();
            }
            set
            {
                _teamLeadId = value.Id;
            }
        }
    }
}
