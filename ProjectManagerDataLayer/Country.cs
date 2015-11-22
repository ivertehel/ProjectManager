using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class Country : Base<Country>
    {
        string Name { get; set; }
        public IEnumerable<User> Users
        {
            get
            {
                return (from items in User.Items where items.Country.Id == Id select items);
            }
        }
    }
}
