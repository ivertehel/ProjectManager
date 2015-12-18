using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Client : Base<Client>
    {
        private Guid _userId;

        public decimal Account { get; set; }

        public User User
        {
            get { return User.Items.Where(items => items.Id == _userId).FirstOrDefault(); }
            set { _userId = value.Id; }
        }

        public IEnumerable<Order> Orders
        {
            get { return from items in Order.Items where items.Client.Id == _userId select items; }
        }
    }
}
