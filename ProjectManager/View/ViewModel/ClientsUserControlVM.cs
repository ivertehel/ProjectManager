using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMDataLayer;

namespace PMView.View
{
    public class ClientsUserControlVM : INotifyPropertyChanged
    {
        private ObservableCollection<Client> _clientsColletction = new ObservableCollection<Client>();

        public ClientsUserControlVM()
        {
            foreach (var item in Client.Items)
            {
                ClientsCollection.Add(item);
            }

            SelectedClient = ClientsCollection[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Client SelectedClient { get; set; }

        public ObservableCollection<Client> ClientsCollection
        {
            get { return _clientsColletction; }
        }

        public string Account
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                return SelectedClient.Account.ToString();
            }
        }

        public string Name
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                return SelectedClient.User.Name;
            }
        }

        public string Surname
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                return SelectedClient.User.Surname;
            }
        }

        public string Login
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                return SelectedClient.User.Login;
            }
        }

        public string Birthday
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                return SelectedClient.User.Birthday.ToShortDateString();
            }
        }

        public string Email
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                return SelectedClient.User.Email;
            }
        }

        public string Skype
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                return SelectedClient.User.Skype;
            }
        }

        public string Country
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                return SelectedClient.User.Country;
            }
        }

        public User.Roles Role
        {
            get
            {
                return SelectedClient.User.Role;
            }
        }

        public string Desctription
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                return SelectedClient.User.Description;
            }
        }

        public User.States State
        {
            get
            {
                if (SelectedClient == null)
                    return User.States.Male;

                return SelectedClient.User.State;
            }
        }

        public string Orders
        {
            get
            {
                if (SelectedClient == null)
                    return string.Empty;

                string orders = string.Empty;
                var ordersList = SelectedClient.Orders.ToList();
                for (int i = 0; i < ordersList.Count; i++)
                {
                    orders += ordersList[i] + (i < ordersList.Count - 1 ? ", " : " ");
                }

                return orders;
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
