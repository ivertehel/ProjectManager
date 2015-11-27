using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Message : Base<Message>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }


        private Guid _fromUserId;
        public User FromUser
        {
            get
            {
                return User.Items.Where(items => items.Id == _fromUserId).FirstOrDefault();
            }
            set
            {
                _fromUserId = value.Id;
            }
        }

        private Guid _toUserId;
        public User ToUser
        {
            get
            {
                return User.Items.Where(items => items.Id == _toUserId).FirstOrDefault();
            }
            set
            {
                _toUserId = value.Id;
            }
        }
    }
}
