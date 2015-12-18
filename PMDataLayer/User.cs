using System;
using System.Collections.Generic;
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

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
