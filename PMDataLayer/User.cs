using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class User : Base<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }

        public IEnumerable<Comment> Comments
        {
            get
            {
                return from items in Comment.Items where items.User.Id == Id select items;
            }
        }
        public IEnumerable<Employee> Employees
        {
            get
            {
                return from items in Employee.Items where items.User.Id == Id select items;
            }
        }
        public IEnumerable<Client> Clients
        {
            get
            {
                return from items in Client.Items where items.User.Id == Id select items;
            }
        }
        public IEnumerable<Message> Inbox
        {
            get
            {
                return from items in Message.Items where items.ToUser.Id == Id select items;
            }
        }
        public IEnumerable<Message> Sentbox
        {
            get
            {
                return from items in Message.Items where items.FromUser.Id == Id select items;
            }
        }
    }
}
