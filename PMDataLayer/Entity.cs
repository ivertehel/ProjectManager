using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        }

        protected static string getConnectionString()
        {
            return ConfigurationManager.AppSettings["MSSqlConnectionString"];
        }
    }
}
