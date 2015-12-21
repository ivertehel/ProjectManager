using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Order : Entity<Order>, ICloneable
    {
        public enum Statuses
        {
            Done,
            InProgress,
            Open,
            Discarded
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Statuses Status { get; set; }

        public bool IsPrivate { get; set; }

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

        public static void Update()
        {
            if (_adapter == null)
            {
                createAdapter("SELECT * FROM Orders");

                _adapter.Fill(_dataSet, "Orders");
                Order.Items.Clear();
                foreach (DataRow row in _dataSet.Tables["Orders"].Rows)
                {
                    Order order = new Order();
                    order.Id = (Guid)row["Id"];
                    order.Price = Convert.ToDecimal(row["Price"]);
                    order._clientId = (Guid)row["Client_Id"];
                    order.Description = row["Description"].ToString();
                    order.StartDate = Convert.ToDateTime(row["StartDate"]);
                    order.ReleaseDate = Convert.ToDateTime(row["ReleaseDate"]);
                    string status = row["Status"].ToString();
                    order.Status = status == "Done" ? Statuses.Done : status == "Discarded" ? Statuses.Discarded : status == "Open" ? Statuses.Open : Statuses.InProgress;
                    int isPrivate = Convert.ToInt32(row["IsPrivate"]);
                    order.IsPrivate = isPrivate == 1 ? true : false;
                    order.Name = row["Name"].ToString();
                    Order.Items.Add(order);
                }
            }
            else
            {
                var rows = _dataSet.Tables["Orders"].Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    rows[i]["Id"] = Order.Items[i].Id;
                    rows[i]["Price"] = Order.Items[i].Price;
                    rows[i]["Client_Id"] = Order.Items[i]._clientId;
                    rows[i]["Description"] = Order.Items[i].Description;
                    rows[i]["StartDate"] = Order.Items[i].StartDate;
                    rows[i]["ReleaseDate"] = Order.Items[i].ReleaseDate;
                    rows[i]["Status"] = Order.Items[i].Status;
                    rows[i]["IsPrivate"] = Convert.ToInt32(Order.Items[i].IsPrivate);
                    rows[i]["Name"] = Order.Items[i].Name;
                }
                _adapter.Update(_dataSet, "Orders");
            }
        }

        public static void Insert(Order order)
        {
            if (_adapter == null)
                createAdapter("SELECT * FROM Orders");

            _adapter.Fill(_dataSet, "Orders");
            _adapter.Update(_dataSet.Tables["Orders"]);


            DataRow newOrdersRow = _dataSet.Tables["Orders"].NewRow();
            newOrdersRow["Id"] = order.Id;
            newOrdersRow["Price"] = order.Price;
            newOrdersRow["Client_Id"] = order._clientId;
            newOrdersRow["Description"] = order.Description;
            newOrdersRow["StartDate"] = order.StartDate;
            newOrdersRow["ReleaseDate"] = order.ReleaseDate;
            newOrdersRow["Status"] = order.Status;
            newOrdersRow["IsPrivate"] = order.IsPrivate == true ? 1 : 0;
            newOrdersRow["Name"] = order.Name;

            _dataSet.Tables["Orders"].Rows.Add(newOrdersRow);
            _adapter.Update(_dataSet.Tables["Orders"]);
            Order.Items.Add(order);
        }

        private Guid _clientId;
    }
}
