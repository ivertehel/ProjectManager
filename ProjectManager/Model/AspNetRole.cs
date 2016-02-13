using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class AspNetRole : Entity<AspNetRole>
    {
        [Column]
        public string Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Discriminator { get; set; }
    }
}
