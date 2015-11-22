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
                return (from items in User.Items where items.Id == _fromUserId select items).FirstOrDefault();
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
                return (from items in User.Items where items.Id == _toUserId select items).FirstOrDefault();
            }
            set
            {
                _toUserId = value.Id;
            }
        }
    }
}
