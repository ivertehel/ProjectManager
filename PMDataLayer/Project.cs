using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Project : Base<Project>
    {
        private Guid _orderId;

        private Guid _leaderId;

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Status { get; set; }

        public Order Order
        {
            get { return Order.Items.Where(items => items.Id == _orderId).FirstOrDefault(); }
            set { _orderId = value.Id; }
        }

        public User Leader
        {
            get { return User.Items.Where(items => items.Id == _leaderId).FirstOrDefault(); }
            set { _leaderId = value.Id; }
        }
    }
}
