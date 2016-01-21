﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Entity<T> where T : Entity<T>
    {
        static Entity()
        {
            Items = new List<T>();
        }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public static List<T> Items { get; set; }

        public Guid Id { get; set; }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static void Update()
        {
            if (_adapter == null)
            {
                Type type = typeof(T);
                string tableName = type.Name + "s";

                createAdapter($@"SELECT * FROM {tableName}");

                _adapter.Fill(_dataSet, tableName);

                foreach (DataRow row in _dataSet.Tables[tableName].Rows)
                {
                    object instance = Activator.CreateInstance(type);
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (property.CustomAttributes.FirstOrDefault(atribute => atribute.AttributeType == typeof(ColumnAttribute)) != null)
                        {
                            var prop = type.GetProperty(property.Name);
                            
                            var r = row[property.Name];
                            prop.SetValue(instance, r);
                        }
                    }
                    Items.Add(instance as T);
                    //client.Id = (Guid)row["Id"];
                    //client.Name = (row["Name"]).ToString();
                    //client._userId = (Guid)row["User_Id"];

                    //Client.Items.Add(client);
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
            //if (_adapter == null)
            //    createAdapter("SELECT * FROM Clients");

            //_adapter.Fill(_dataSet, "Clients");

            //DataRow newClientsRow = _dataSet.Tables["Clients"].NewRow();
            //newClientsRow["Id"] = client.Id;
            //newClientsRow["Account"] = client.Account;
            //newClientsRow["User_Id"] = client._userId;

            //_dataSet.Tables["Clients"].Rows.Add(newClientsRow);
            //_adapter.Update(_dataSet.Tables["Clients"]);

        }

        protected static SqlDataAdapter _adapter;

        protected static DataSet _dataSet = new DataSet();

        protected static void createCommand(SqlDataAdapter adapter)
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            adapter.InsertCommand = commandBuilder.GetInsertCommand();
            adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
        }

        protected static void createAdapter(string selectSQL)
        {
            string connectionString = getConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand selectProductCommand = new SqlCommand(selectSQL, connection);

            _adapter = new SqlDataAdapter(selectSQL, connection);

            createCommand(_adapter);
        }

        protected static string getConnectionString()
        {
            return ConfigurationManager.AppSettings["MSSqlConnectionString"];
        }
    }
}
