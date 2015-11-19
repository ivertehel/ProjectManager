using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class TasksInProjects
    {
        public Guid Id;
        public TasksInProjects()
        {
            Id = Guid.NewGuid();
        }

        private Guid _taskId;
        public Task Task
        {
            get
            {
                return (from items in Task.Items where items.Id == _taskId select items).FirstOrDefault();
            }
            set
            {
                _taskId = value.Id;
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
    }
}
