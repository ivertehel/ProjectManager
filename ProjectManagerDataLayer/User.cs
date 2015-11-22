using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class User : Base<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Image { get; set; }

        private Guid _countryId;
        public Country Country
        {
            get
            {
                return (from items in Country.Items where items.Id == _countryId select items).FirstOrDefault();
            }
            set
            {
                _countryId = value.Id;
            }
        }

        private Guid _roleId;
        public Role Role
        {
            get
            {
                return (from items in Role.Items where items.Id == _roleId select items).FirstOrDefault();
            }
            set
            {
                _roleId = value.Id;
            }
        }
        public IEnumerable<Message> Inbox
        {
            get
            {
                return (from items in Message.Items where items.ToUser.Id == Id select items);
            }
        }
        public IEnumerable<Message> Sentbox
        {
            get
            {
                return (from items in Message.Items where items.FromUser.Id == Id select items);
            }
        }
    }
}
