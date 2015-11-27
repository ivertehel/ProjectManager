using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Comment : Base<Comment>
    {
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

        private Guid _userId;
        public User User
        {
            get
            {
                return User.Items.Where(items => items.Id == _userId).FirstOrDefault();
            }
            set
            {
                _userId = value.Id;
            }
        }
        public Owners Owner { get; set; }

        public enum Owners { Order, Project, Task, Report};
    
    }
}
