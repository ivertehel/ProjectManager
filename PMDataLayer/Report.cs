using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Report : Base<Report>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        private Guid _orderId;
        public Order Order
        {
            get
            {
                return (from items in Order.Items where items.Id == _orderId select items).FirstOrDefault();
            }
            set
            {
                _orderId = value.Id;
            }
        }
        public IEnumerable<Comment> Comments
        {
            get
            {
                return from items in Comment.Items where items.Report.Id == Id select items;
            }
        }
    }
}
