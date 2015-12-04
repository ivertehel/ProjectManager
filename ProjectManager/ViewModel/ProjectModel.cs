using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMDataLayer;

namespace PMView.View
{
    public class ProjectModel
    {
        public Order Order { get; set; }

        public string Name { get; set; }

        public string StartDate { get; set; }

        public string ReleaseDate { get; set; }

        public string Price { get; set; }

        public Order.Statuses Status { get; set; }
    }
}
