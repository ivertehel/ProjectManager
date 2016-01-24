using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class Client : Entity<Client>
    {
        private Guid _userId;

        [Column]
        public decimal Account { get; set; }

        [Column]
        public Guid User_Id
        {
            get { return _userId; }
            set { _userId = value; }
        }

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
