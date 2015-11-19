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
    }
}
