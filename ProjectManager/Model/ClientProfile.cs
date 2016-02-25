using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class ClientProfile : Entity<ClientProfile>
    {
        public ClientProfile()
        {
        }

        public ClientProfile(string id)
        {
            Id = id;
        }

        [Column]
        public string Id { get; set; }

        [Column]
        public string UserId { get; set; }
    }
}
