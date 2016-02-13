using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class User : Entity<User>
    {
        private static List<string> _countries;
        private Roles _role;
        private Statuses _status;
        private States _state;

        public enum Roles
        {
            NotChosen,
            Administrator,
            Client,
            Employee
        }

        public enum States
        {
            NotChosen,
            Male,
            Female
        }

        public enum Statuses
        {
            NotChosen,
            InWork,
            NotReady,
            Ready,
            UnInvited
        }

        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column]
        public string Name { get; set; }

        [Column]
        public string Surname { get; set; }

        [Column]
        public string Password { get; set; }

        [Column]
        public string Login { get; set; }

        [Column]
        public DateTime Birthday { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public string Skype { get; set; }

        [Column]
        public string Country { get; set; }

        [Column]
        public byte[] Image { get; set; }

        [Column]
        public string Role
        {
            get { return _role.ToString(); }
            set
            {
                if (value == "Administrator")
                {
                    _role = Roles.Administrator;
                }
                else if (value == "Client")
                {
                    _role = Roles.Client;
                }
                else if (value == "Employee")
                {
                    _role = Roles.Employee;
                }
                else
                { 
                    _role = Roles.NotChosen;
                }
            }
        }

        public Roles RoleType
        {
            get { return _role; }
            set { _role = value; }
        }

        [Column]
        public string Status
        {
            get { return _status.ToString(); }
            set
            {
                if (value == "InWork")
                {
                    _status = Statuses.InWork;
                }
                else if (value == "NotReady")
                {
                    _status = Statuses.NotReady;
                }
                else if (value == "Ready")
                {
                    _status = Statuses.Ready;
                }
                else if (value == "UnInvited")
                {
                    _status = Statuses.UnInvited;
                }
                else
                {
                    _status = Statuses.NotChosen;
                }
            }
        }

        public Statuses StatusType
        {
            get { return _status; }
            set { _status = value; }
        }

        [Column]
        public string Description { get; set; }

        [Column]
        public string State
        {
            get { return _state.ToString(); }
            set
            {
                if (value == States.Female.ToString())
                    _state = States.Female;
                else if (value == States.Male.ToString())
                    _state = States.Male;
                else
                    _state = States.NotChosen;
            }
        }

        public States StateType
        {
            get { return _state; }
            set { _state = value; }
        }

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

        public void RegisterUser()
        {
            string hash = getPassHash(Password);
            var aspUser = new AspNetUser(Login) { Email = Email, PasswordHash = hash, SecurityStamp = Guid.NewGuid().ToString() };
            AspNetRole.Update();
            AspNetUser.Insert(aspUser);

            ClientProfile.Insert(new ClientProfile(aspUser.Id) { UserId = Id.ToString() });
            AspNetUserRole.Insert(new AspNetUserRole() { UserId = aspUser.Id, RoleId = AspNetRole.Items.FirstOrDefault(item => item.Name == Role.ToLower()).Id });

        }

        public string getPassHash(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static List<string> Countries
        {
            get { return _countries; }
        }

        //public static void Update()
        //{
        //    if (_adapter == null)
        //    {
        //        createAdapter("SELECT * FROM Users");
        //        _adapter.Fill(_dataSet, "Users");

        //        foreach (DataRow row in _dataSet.Tables["Users"].Rows)
        //        {
        //            User user = new User();
        //            user.Id = (Guid)row["Id"];
        //            user.Name = row["Name"].ToString();
        //            user.Surname = row["Surname"].ToString();
        //            user.Password = row["Password"].ToString();
        //            user.Login = row["Login"].ToString();
        //            user.Birthday = Convert.ToDateTime(row["Birthday"]);
        //            user.Email = row["Email"].ToString();
        //            user.Skype = row["Skype"].ToString();
        //            user.Country = row["Country"].ToString();
        //            user.Image = (byte[])row["Image"];
        //            string role = row["Role"].ToString();
        //            user.Role = role == "Administrator" ? Roles.Administrator : role == "Client" ? Roles.Client : Roles.Employee;
        //            string status = row["Status"].ToString();
        //            user.Status = status == null || status == string.Empty ? Statuses.Ready : status == "InWork" ? Statuses.InWork :
        //                status == "NotReady" ? Statuses.NotReady : status == "Ready" ? Statuses.Ready : Statuses.UnInvited;
        //            user.Description = row["Description"].ToString();
        //            user.State = row["State"].ToString() == "Male" ? States.Male : States.Female;
        //            User.Items.Add(user);
        //        }
        //    }
        //    else
        //    {
        //        var rows = _dataSet.Tables["Users"].Rows;
        //        for (int i = 0; i < rows.Count; i++)
        //        {
        //            rows[i]["Id"] = User.Items[i].Id;
        //            rows[i]["Name"] = User.Items[i].Name;
        //            rows[i]["Surname"] = User.Items[i].Surname;
        //            rows[i]["Password"] = User.Items[i].Password;
        //            rows[i]["Login"] = User.Items[i].Login;
        //            rows[i]["Birthday"] = User.Items[i].Birthday;
        //            rows[i]["Email"] = User.Items[i].Email;
        //            rows[i]["Skype"] = User.Items[i].Skype;
        //            rows[i]["Country"] = User.Items[i].Country;
        //            rows[i]["Image"] = User.Items[i].Image;
        //            rows[i]["Role"] = User.Items[i].Role;
        //            rows[i]["Status"] = User.Items[i].Status;
        //            rows[i]["Description"] = User.Items[i].Description;
        //            rows[i]["State"] = User.Items[i].State;
        //        }
                
        //        _adapter.Update(_dataSet, "Users");
        //    }
        //}

        //public static void Insert(User user)
        //{
        //    if (_adapter == null)
        //        createAdapter("SELECT * FROM Users");

        //    _adapter.Fill(_dataSet, "Users");

        //    DataRow newUsersRow = _dataSet.Tables["Users"].NewRow();
        //    newUsersRow["Id"] = user.Id;
        //    newUsersRow["Name"] = user.Name;
        //    newUsersRow["Surname"] = user.Surname;
        //    newUsersRow["Password"] = user.Password;
        //    newUsersRow["Login"] = user.Login;
        //    newUsersRow["Birthday"] = user.Birthday;
        //    newUsersRow["Email"] = user.Email;
        //    newUsersRow["Skype"] = user.Skype;
        //    newUsersRow["Country"] = user.Country;
        //    newUsersRow["Image"] = user.Image;
        //    newUsersRow["Role"] = user.Role;
        //    newUsersRow["Status"] = user.Status;
        //    newUsersRow["Description"] = user.Description;
        //    newUsersRow["State"] = user.State;

        //    _dataSet.Tables["Users"].Rows.Add(newUsersRow);
        //    _adapter.Update(_dataSet.Tables["Users"]);
            
        //}

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
