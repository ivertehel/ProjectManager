using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerSite.Models
{
    public class OrderVM : BaseVM
    {
        public Guid OrderId { get; set; }

        public OrderVM(IPrincipal user, string orderId) : base(user)
        {
            OrderId = new Guid(orderId);
        }

        public OrderVM() : base(null)
        {

        }

    }
}
