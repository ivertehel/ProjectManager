using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Position : Base<Position>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
