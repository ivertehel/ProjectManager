using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Client : Base<Client>
    {
        public decimal Account { get; set; }

        private Guid _userId;
        public User User
        {
            get
            {
                return (from items in User.Items where items.Id == _userId select items).FirstOrDefault();
            }
            set
            {
                _userId = value.Id;
            }
        }
        public IEnumerable<Order> Orders
        {
            get
            {
                return from items in Order.Items where items.Client.Id == Id select items;
            }
        }
    }
}
