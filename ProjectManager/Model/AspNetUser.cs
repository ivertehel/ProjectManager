using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class AspNetUser : Entity<AspNetUser>
    {
        public AspNetUser(string userName)
        {
            UserName = userName;
        }

        public AspNetUser()
        {
        }

        [Column]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Column]
        public string Email { get; set; }

        [Column]
        public bool EmailConfirmed { get; set; }

        [Column]
        public string PasswordHash { get; set; }

        [Column]
        public string SecurityStamp { get; set; }

        [Column]
        public string PhoneNumber { get; set; }

        [Column]
        public bool PhoneNumberConfirmed { get; set; }

        [Column]
        public bool TwoFactorEnabled { get; set; }

        [Column]
        public DateTime? LockoutEndDateUtc { get; set; } = null;

        [Column]
        public bool LockoutEnabled { get; set; }

        [Column]
        public int AccessFailedCount { get; set; }

        [Column]
        public string UserName { get; set; }
    }
}
