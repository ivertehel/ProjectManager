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

        public IEnumerable<Skill> AllSkills
        {
            get { return Skill.Items; }
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

        public static List<string> Countries
        {
            get { return _countries; }
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
