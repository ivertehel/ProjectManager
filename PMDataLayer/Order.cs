using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Order : Base<Order>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Status { get; set; }

        public bool IsPrivate { get; set; }

        public IEnumerable<Project> Projects
        {
            get { return Project.Items.Where(items => items.Order.Id == Id); }
        }

        public IEnumerable<Report> Reports
        {
            get { return Report.Items.Where(items => items.Order.Id == Id); }
        }
    }
}
