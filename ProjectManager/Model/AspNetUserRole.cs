using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class AspNetUserRole : Entity<AspNetUserRole>
    {
        [Column]
        public string UserId { get; set; }

        [Column]
        public string RoleId { get; set; }
    }
}
