using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Order : Base<Order>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Status { get; set; }
        public bool IsPrivate { get; set; }

        private Guid _clientId;
        public Client Client
        {
            get
            {
                return (from items in Client.Items where items.Id == _clientId select items).FirstOrDefault();
            }
            set
            {
                _clientId = value.Id;
            }
        }

        public IEnumerable<Report> Reports
        {
            get
            {
                return from items in Report.Items where items.Order.Id == Id select items;
            }
        }

        public IEnumerable<Comment> Comments
        {
            get
            {
                return from items in Comment.Items where items.Order.Id == Id select items;
            }
        }

        public IEnumerable<Project> Projects
        {
            get
            {
                return from items in Project.Items where items.Order.Id == Id select items;
            }
        }
    }
}
