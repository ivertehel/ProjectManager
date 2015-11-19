using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class Client : Base<Client>
    {
        public Client()
        {
            var newUser = new User();
            User = newUser;
            User.Items.Add(newUser);
        }
        public decimal Account { get; set; }
        private Guid userId;
        public User User
        {
            get
            {
                return (from items in User.Items where items.Id == userId select items).FirstOrDefault();
            }
            set
            {
                userId = value.Id;
            }
        }
    }
}
