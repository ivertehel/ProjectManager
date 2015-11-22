using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Team_Task : Base<Team_Task>
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
    }
}
