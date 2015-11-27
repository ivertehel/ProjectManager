using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Task : Base<Task>
    {
        private Guid _projectId;

        public enum Owners
        {
            Team,
            User
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public int Priority { get; set; }

        public int Hours { get; set; }

        public Project Project
        {
            get { return Project.Items.Where(items => items.Id == _projectId).FirstOrDefault(); }
            set { _projectId = value.Id; }
        }

        public Owners Owner { get; set; }

        public IEnumerable<Task> UsersTasks
        {
            get { return Task.Items.Where(items => items.Owner == Owners.User); }
        }

        public IEnumerable<Task> TeamsTasks
        {
            get { return Task.Items.Where(items => items.Owner == Owners.Team); }
        }
    }
}
