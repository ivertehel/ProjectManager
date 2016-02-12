using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Message : Entity<Message>
    {
        private Guid _fromUserId;

        private Guid _toUserId;

        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime DateTime { get; set; }

        public User FromUser
        {
            get { return User.Items.Where(items => items.Id == _fromUserId).FirstOrDefault(); }
            set { _fromUserId = value.Id; }
        }

        public User ToUser
        {
            get { return User.Items.Where(items => items.Id == _toUserId).FirstOrDefault(); }
            set { _toUserId = value.Id; }
        }
    }
}
