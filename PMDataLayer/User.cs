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

        public byte[] Image { get; set; }

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
            if (_adapter == null)
                createAdapter("SELECT * FROM Users");

            User.Items.Clear();

            _adapter.Fill(_dataSet, "Users");

            foreach (DataRow row in _dataSet.Tables["Users"].Rows)
            {
                User user = new User();
                user.Id = (Guid)row["Id"];
                user.Name = row["Name"].ToString();
                user.Surname = row["Surname"].ToString();
                user.Password = row["Password"].ToString();
                user.Login = row["Login"].ToString();
                user.Birthday = Convert.ToDateTime(row["Birthday"]);
                user.Email = row["Email"].ToString();
                user.Skype = row["Skype"].ToString();
                user.Country = row["Country"].ToString();
                user.Image = Entity<User>.GetBytes(row["Image"].ToString());
                string role = row["Role"].ToString();
                user.Role = role == "Administrator" ? Roles.Administrator : role == "Client" ? Roles.Client : Roles.Employee;
                string status = row["Status"].ToString();
                user.Status = status == null || status == string.Empty ? Statuses.Ready : status == "InWork" ? Statuses.InWork :
                    status == "NotReady" ? Statuses.NotReady : status == "Ready" ? Statuses.Ready : Statuses.UnInvited;
                user.Description = row["Description"].ToString();
                user.State = row["State"].ToString() == "Male" ? States.Male : States.Female;
                User.Items.Add(user);
            }
        }

        public static void Insert(User user)
        {
            if (_adapter == null)
                createAdapter("SELECT * FROM Users");

            _adapter.Fill(_dataSet, "Users");

            DataRow newUsersRow = _dataSet.Tables["Users"].NewRow();
            newUsersRow["Id"] = user.Id;
            newUsersRow["Name"] = user.Name;
            newUsersRow["Surname"] = user.Surname;
            newUsersRow["Password"] = user.Password;
            newUsersRow["Login"] = user.Login;
            newUsersRow["Birthday"] = user.Birthday;
            newUsersRow["Email"] = user.Email;
            newUsersRow["Skype"] = user.Skype;
            newUsersRow["Country"] = user.Country;
            newUsersRow["Image"] = user.Image;
            newUsersRow["Role"] = user.Role;
            newUsersRow["Status"] = user.Status;
            newUsersRow["Description"] = user.Description;
            newUsersRow["State"] = user.State;

            _dataSet.Tables["Users"].Rows.Add(newUsersRow);
            _adapter.Update(_dataSet.Tables["Users"]);

        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
