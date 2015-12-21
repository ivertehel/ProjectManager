using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Client : Entity<Client>
    {
        private Guid _userId;

        public decimal Account { get; set; }

        public User User
        {
            get { return User.Items.Where(items => items.Id == _userId).FirstOrDefault(); }
            set { _userId = value.Id; }
        }

        public IEnumerable<Order> Orders
        {
            get { return from items in Order.Items where items.Client.Id == _userId select items; }
        }

        public static void Update()
        {
            if (_adapter == null)
            {
                createAdapter("SELECT * FROM Clients");

                _adapter.Fill(_dataSet, "Clients");

                foreach (DataRow row in _dataSet.Tables["Clients"].Rows)
                {
                    Client client = new Client();
                    client.Id = (Guid)row["Id"];
                    client.Account = Convert.ToDecimal(row["Account"]);
                    client._userId = (Guid)row["User_Id"];

                    Client.Items.Add(client);
                }
            }
            else
            {
                var rows = _dataSet.Tables["Clients"].Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    rows[i]["Id"] = Client.Items[i].Id;
                    rows[i]["Account"] = Client.Items[i].Account;
                    rows[i]["User_Id"] = Client.Items[i].User.Id;
                }

                _adapter.Update(_dataSet, "Clients");
            }
        }

        public static void Insert(Client client)
        {
            if (_adapter == null)
                createAdapter("SELECT * FROM Clients");

            _adapter.Fill(_dataSet, "Clients");

            DataRow newUsersRow = _dataSet.Tables["Clients"].NewRow();
            newUsersRow["Id"] = client.Id;
            newUsersRow["Account"] = client.Account;
            newUsersRow["User_Id"] = client._userId;

            _dataSet.Tables["Clients"].Rows.Add(newUsersRow);
            _adapter.Update(_dataSet.Tables["Clients"]);
        }
    }
}
