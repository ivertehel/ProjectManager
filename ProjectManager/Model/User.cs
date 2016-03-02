using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public static List<string> Countries
        {
            get { return _countries; }
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
            get { return from items in Users_Skill.Items where items.User.Id == Id select items.Skill; }
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
            get { return from items in Users_Project.Items where items.User.Id == Id select items.Project; }
        }

        public IEnumerable<Comment> Comments
        {
            get { return Comment.Items.Where(items => items.User.Id == Id); }
        }

        public IEnumerable<Team> Teams
        {
            get { return from items in Users_Team.Items where items.User.Id == Id select items.Team; }
        }

        public void RegisterUser()
        {
            string hash = getPassHash(Password);

            var aspUser = new AspNetUser(Login) { Email = Email, PasswordHash = hash, SecurityStamp = Guid.NewGuid().ToString() };
            var pass = Password;
            Password = string.Empty;
            Insert(this);
            Client.Insert(new Client() { Account = 0, User_Id = Id });
            AspNetRole.Update();
            AspNetUser.Insert(aspUser);
            ClientProfile.Insert(new ClientProfile(aspUser.Id) { UserId = Id.ToString() });
            AspNetUserRole.Insert(new AspNetUserRole() { UserId = aspUser.Id, RoleId = AspNetRole.Items.FirstOrDefault(item => item.Name == Role.ToLower()).Id });
            sendEmail(Email, "Your password from Project Manager", $@"Hi, {Name}! <br/>" + "Your login is: <b>" + Login + "</b><br/>Your password is: <b>"  + pass + "</b>");
            
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

        public override string ToString()
        {
            return Name + " " + Surname;
        }

        private void sendEmail(string emailTo, string subject, string body)
        {
            string smtpAddress = "smtp.mail.yahoo.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = ConfigurationManager.AppSettings["Email"];
            string password = ConfigurationManager.AppSettings["Password"];

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom, "Project Manager");
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;


                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}
