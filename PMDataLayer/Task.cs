using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Task : Base<Task>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }



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
        public IEnumerable<Employee_Task> EmployeesTasks
        {
            get
            {
                return from items in Employee_Task.Items where items.Task.Id == Id select items;
            }
        }
        public IEnumerable<Team_Task> TeamsTasks
        {
            get
            {
                return from items in Team_Task.Items where items.Task.Id == Id select items;
            }
        }
        public IEnumerable<Comment> Comments
        {
            get
            {
                return from items in Comment.Items where items.Task.Id == Id select items;
            }
        }
    }
}
