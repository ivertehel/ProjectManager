using System;
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
    [Table]
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

        [Column]
        public Guid Id { get; set; }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static void Update()
        {
            Type type = typeof(T);
            string tableName = type.Name + "s";

            if (_adapter == null)
            {
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
                            prop.SetValue(instance, r == DBNull.Value ? null : r);
                        }
                    }
                    Items.Add(instance as T);
                }
            }
            else
            {
                var rows = _dataSet.Tables[tableName].Rows;

                for (int i = 0; i < rows.Count; i++)
                {
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (property.CustomAttributes.FirstOrDefault(atribute => atribute.AttributeType == typeof(ColumnAttribute)) != null)
                        {
                            var prop = type.GetProperty(property.Name);
                            var item = Items[i].GetType();
                            rows[i][property.Name] = item.GetProperty(property.Name).GetValue(Items[i]);
                        }
                    }
                }
                _adapter.Update(_dataSet, tableName);
            }
        }

        public static void Insert(T instance)
        {
            Type type = typeof(T);
            string tableName = type.Name + "s";
            if (_adapter == null)
                createAdapter($@"SELECT * FROM {tableName}");

            _adapter.Fill(_dataSet, tableName);

            DataRow newRow = _dataSet.Tables[tableName].NewRow();
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.CustomAttributes.FirstOrDefault(atribute => atribute.AttributeType == typeof(ColumnAttribute)) != null)
                {
                    object prop = type.GetProperty(property.Name).GetValue(instance);
                    if (prop == null)
                    {
                        newRow[property.Name] = DBNull.Value;
                    }
                    else newRow[property.Name] = prop;
                }
            }

            _dataSet.Tables[tableName].Rows.Add(newRow);
            _adapter.Update(_dataSet.Tables[tableName]);
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
