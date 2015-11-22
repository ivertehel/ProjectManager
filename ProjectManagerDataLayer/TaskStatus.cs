using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class TaskStatus : Base<TaskStatus>
    {
        public string Name { get; set; }
        public IEnumerable<Task> Tasks
        {
            get
            {
                return (from items in Task.Items where items.Status.Id == Id select items);
            }
        }
    }
}
