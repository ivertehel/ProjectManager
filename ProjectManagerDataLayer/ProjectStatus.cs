using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class ProjectStatus : Base<ProjectStatus>
    {
        public string Name { get; set; }
        public IEnumerable<Project> Projects
        {
            get
            {
                return (from items in Project.Items where items.Status.Id == Id select items);
            }
        }
            
    }
}
