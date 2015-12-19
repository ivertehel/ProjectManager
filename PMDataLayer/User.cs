using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class User : Entity<User>
    {
        private static List<string> _countries;

        private static SqlDataAdapter _productAdapter;

        private static DataSet _dataSet = new DataSet();

        public enum Roles
        {
            Administrator,
            Client,
            Employee
        }

        public enum States
        {
            Male,
            Female
        }

        public enum Statuses
        {
            InWork,
            NotReady,
            Ready,
            UnInvited
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public string Login { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public string Country { get; set; }

        public string Image { get; set; }

        public Roles Role { get; set; }

        public Statuses Status { get; set; }

        public string Description { get; set; }

        public States State { get; set; }

        public IEnumerable<Skill> Skills
        {
            get { return from items in User_Skill.Items where items.User.Id == Id select items.Skill; }
        }

        public IEnumerable<Report> Reports
        {
            get { return Report.Items.Where(items => items.User.Id == Id); }
        }

        public IEnumerable<Message> Inbox
        {
            get { return Message.Items.Where(items => items.ToUser.Id == Id); }
        }

        public IEnumerable<Message> Sentbox
        {
            get { return Message.Items.Where(items => items.FromUser.Id == Id); }
        }

        public IEnumerable<Project> Projects
        {
            get { return from items in User_Project.Items where items.User.Id == Id select items.Project; }
        }

        public IEnumerable<Comment> Comments
        {
            get { return Comment.Items.Where(items => items.User.Id == Id); }
        }

        public IEnumerable<Team> Teams
        {
            get { return from items in User_Team.Items where items.User.Id == Id select items.Team; }
        }

        static User()
        {
            _countries = new List<string>();
            using (StreamReader reader = new StreamReader("Countries.txt"))
            {
                while (!reader.EndOfStream)
                {
                    _countries.Add(reader.ReadLine());
                }
                reader.Close();
            }
        }

        public static List<string> Countries
        {
            get { return _countries; }
        }

        public static void Update()
        {
            if (_productAdapter == null)
            {
                string connectionString = GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);

                string selectUsersSQL = "SELECT * FROM Users";
                SqlCommand selectProductCommand = new SqlCommand(selectUsersSQL, connection);

                _productAdapter = new SqlDataAdapter(selectUsersSQL, connection);

                CreateUpdateCommand(_productAdapter);
            }

            User.Items.Clear();

            _productAdapter.Fill(_dataSet, "Users");

            foreach (DataRow row in _dataSet.Tables["Users"].Rows)
            {
                User user = new User();
                user.Name = row["Name"].ToString();
                user.Password = row["Password"].ToString();
                user.Email = row["Email"].ToString();
                user.Skype = row["Skype"].ToString();
                user.Surname = row["Surname"].ToString();
                User.Items.Add(user);
            }
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }

        private static string GetConnectionString()
        {
            return @"Data Source=ivan-desktop\sqlexpress;Initial Catalog=ProjectManagerDB;Integrated Security=True";
        }

        private static void CreateUpdateCommand(SqlDataAdapter adapter)
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            adapter.InsertCommand = commandBuilder.GetInsertCommand();
            adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
        }
    }
}
