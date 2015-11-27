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

        private Guid _userId;
        public User User
        {
            get
            {
                return User.Items.Where(items => items.Id == _userId).FirstOrDefault();
            }
            set
            {
                _userId = value.Id;
            }
        }

        private Guid _orderId;
        public Order Order
        {
            get
            {
                return Order.Items.Where(items => items.Id == _orderId).FirstOrDefault();
            }
            set
            {
                _orderId = value.Id;
            }
        }
    }
}
