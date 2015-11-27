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
                return User.Items.Where(items => items.Id == _userId).FirstOrDefault();
            }
            set
            {
                _userId = value.Id;
            }
        }
      
    }
}
