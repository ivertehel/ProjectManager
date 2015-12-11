using PMDataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMView.View.WrapperVM
{
    public class OrderVM : BaseVM, INotifyPropertyChanged
    {
        private Order _order;

        public OrderVM(Order order)
        {
            _order = order;
        }

        public Order Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public string Name
        {
            get
            {
                return _order.Name;
            }
            set
            {
                _order.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get
            {
                return _order.Description;
            }
            set
            {
                _order.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public decimal Price
        {
            get
            {
                return _order.Price;
            }
            set
            {
                _order.Price = value;
                OnPropertyChanged("Price");
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _order.StartDate;
            }
            set
            {
                _order.StartDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                return _order.ReleaseDate;
            }
            set
            {
                _order.ReleaseDate = value;
                OnPropertyChanged("ReleaseDate");
            }
        }

        public Order.Statuses Status
        {
            get
            {
                return _order.Status;
            }
            set
            {
                _order.Status = value;
                OnPropertyChanged("Status");
            }
        }

        public bool IsPrivate
        {
            get
            {
                return _order.IsPrivate;
            }
            set
            {
                _order.IsPrivate = value;
                OnPropertyChanged("IsPrivate");
            }
        }

        public Client Client
        {
            get
            {
                return _order.Client;
            }
            set
            {
                _order.Client = value;
                OnPropertyChanged("Client");
            }
        }
    }
}
