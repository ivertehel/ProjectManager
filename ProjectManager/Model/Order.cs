using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class Order : Entity<Order>, ICloneable
    {
        public enum Statuses
        {
            Done,
            InProgress,
            Open,
            Discarded
        }

        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public decimal Price { get; set; }

        [Column]
        public DateTime StartDate { get; set; }

        [Column]
        public DateTime ReleaseDate { get; set; }

        [Column]
        public string Status
        {
            get { return _status.ToString(); }
            set
            {
                if (value == "Discarded")
                {
                    _status = Statuses.Discarded;
                }
                else if (value == "Done")
                {
                    _status = Statuses.Done;
                }
                else if (value == "InProgress")
                {
                    _status = Statuses.InProgress;
                }
                else
                {
                    _status = Statuses.Open;
                }
            }
        }

        public Statuses StatusType
        {
            get { return _status; }
            set { _status = value; }
        }

        [Column]
        public int IsPrivate
        {
            get { return _isPrivate == true ? 1 : 0; }
            set { _isPrivate = value == 1 ? true : false; }
        }

        [Column]
        public Guid Client_Id
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        public Client Client
        {
            get { return Client.Items.Where(item => item.Id == _clientId).FirstOrDefault(); }
            set { _clientId = value.Id; }
        }

        public IEnumerable<Project> Projects
        {
            get { return Project.Items.Where(items => items.Order.Id == Id); }
        }

        public IEnumerable<Report> Reports
        {
            get { return Report.Items.Where(items => items.Order.Id == Id); }
        }

        public object Clone()
        {
            Order newOrder = this.MemberwiseClone() as Order;
            newOrder.Id = new Guid();
            return newOrder;
        }

        private Guid _clientId;
        private Statuses _status;
        private bool _isPrivate;
    }
}
