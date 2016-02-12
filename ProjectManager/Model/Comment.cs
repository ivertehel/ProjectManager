using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Comment : Entity<Comment>
    {
        private Guid _userId;

        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        public enum Owners
        {
            /// <summary>
            /// Represents an order
            /// </summary>
            Order,

            /// <summary>
            /// Represents a project
            /// </summary>
            Project,

            /// <summary>
            /// Represents a task
            /// </summary>
            Task,

            /// <summary>
            /// Represents a report
            /// </summary>
            Report
        }

        public string Message { get; set; }

        public DateTime DateTime { get; set; }

        public User User
        {
            get { return User.Items.Where(items => items.Id == _userId).FirstOrDefault(); }
            set { _userId = value.Id; }
        }

        public Owners Owner { get; set; }
    }
}
