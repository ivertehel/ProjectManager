using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMDataLayer;

namespace PMViewModel
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public string StartDate { get; set; }

        public string ReleaseDate { get; set; }

        public string Price { get; set; }

        public Order.Statuses Status { get; set; }
    }
}
