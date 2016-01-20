using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMDataLayer;
using System.Windows.Media.Imaging;
using System.IO;

namespace PMView.View.WrapperVM
{
    public class ClientVM : BaseVM
    {
        private Client _client;

        public ClientVM(Client client)
        {
            _client = client;
        }

        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public decimal Account
        {
            get { return _client.Account; }
            set { _client.Account = value; }
        }

        public string Name
        {
            get { return _client.User.Name; }
            set { _client.User.Name = value; }
        }

        public string Surname
        {
            get { return _client.User.Surname; }
            set { _client.User.Surname = value; }
        }

        public string Password
        {
            get { return _client.User.Password; }
            set { _client.User.Password = value; }
        }

        public string Login
        {
            get { return _client.User.Login; }
            set { _client.User.Login = value; }
        }

        public DateTime Birthday
        {
            get { return _client.User.Birthday; }
            set { _client.User.Birthday = value; }
        }

        public string Email
        {
            get { return _client.User.Email; }
            set { _client.User.Email = value; }
        }

        public string Skype
        {
            get { return _client.User.Skype; }
            set { _client.User.Skype = value; }
        }

        public string Country
        {
            get { return _client.User.Country; }
            set { _client.User.Country = value; }
        }

        public BitmapImage BitmapImage
        {
            get
            {
                return (new UserVM(_client.User)).BitmapImage;
            }
        }

    }
}
