using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class Project : Entity<Project>, ICloneable
    {
        private Guid _orderId;

        private Guid? _leaderId;
        private Statuses _status;

        public enum Statuses
        {
            Done,
            InProgress,
            Opened,
            Discarded
        }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public DateTime StartDate { get; set; }

        [Column]
        public DateTime ReleaseDate { get; set; }

        [Column]
        public string Status
        {
            get { return _status.ToString(); }
            set
            {
                if (value == "Discarded")
                {
                    _status = Statuses.Discarded;
                }
                else if (value == "Done")
                {
                    _status = Statuses.Done;
                }
                else if (value == "InProgress")
                {
                    _status = Statuses.InProgress;
                }
                else
                {
                    _status = Statuses.Opened;
                }
            }
        }

        [Column]
        public Guid Order_Id
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        [Column]
        public Guid? Leader_Id
        {
            get { return _leaderId; }
            set { _leaderId = value; }
        }

        public Order Order
        {
            get { return Order.Items.Where(items => items.Id == _orderId).FirstOrDefault(); }
            set { _orderId = value.Id; }
        }

        public User Leader
        {
            get { return User.Items.Where(items => items.Id == _leaderId).FirstOrDefault(); }
            set { _leaderId = value?.Id; }
        }

        public Statuses StatusType
        {
            get { return _status; }
            set { _status = value; }
        }

        public IEnumerable<Project> ChildProjects
        {
            get { return from items in Project_Project.Items where items.ParrentProject.Id == Id select items.ChildProject; }
        }

        public IEnumerable<Project> ParrentProjects
        {
            get { return from items in Project_Project.Items where items.ChildProject.Id == Id select items.ParrentProject; }
        }

        public IEnumerable<User_Project> Users
        {
            get { return from items in User_Project.Items where items.Project.Id == Id select items; }
        }

        public IEnumerable<Skill> Skills
        {
            get { return from items in Project_Skill.Items where items.Project.Id == Id select items.Skill; }
        }

        public IEnumerable<Team> Teams
        {
            get { return from items in Team_Project.Items where items.Project.Id == Id select items.Team; }
        }

        public IEnumerable<Task> Tasks
        {
            get { return Task.Items.Where(items => items.Project.Id == Id); }
        }

        public object Clone()
        {
            Project newProject = this.MemberwiseClone() as Project;
            newProject.Id = new Guid();
            return newProject;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
