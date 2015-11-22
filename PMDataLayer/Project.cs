using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Project : Base<Project>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Status { get; set; }

        private Guid _orderId;
        public Order Order
        {
            get
            {
                return (from items in Order.Items where items.Id == _orderId select items).FirstOrDefault();
            }
            set
            {
                _orderId = value.Id;
            }
        }
        public IEnumerable<Skill> Skills
        {
            get
            {
                return from items in Project_Skill.Items where items.Project.Id== Id select items.Skill;
            }
        }
        public IEnumerable<Task> Tasks
        {
            get
            {
                return from items in Task.Items where items.Project.Id == Id select items;
            }
        }
        public IEnumerable<Team> Teams
        {
            get
            {
                return from items in Team_Project.Items where items.Project.Id == Id select items.Team;
            }
        }
        public IEnumerable<Employee_Project> EmployeesInProject
        {
            get
            {
                return from items in Employee_Project.Items where items.Project.Id == Id select items;
            }
        }
        public IEnumerable<Comment> Comments
        {
            get
            {
                return from items in Comment.Items where items.Project.Id == Id select items;
            }
        }
        public IEnumerable<Project> Parrents
        {
            get
            {
                return from items in Project_Project.Items where items.ChildProject.Id == Id select items.ParrentProject;
            }
        }
        public IEnumerable<Project> Childs
        {
            get
            {
                return from items in Project_Project.Items where items.ParrentProject.Id == Id select items.ChildProject;
            }
        }
    }
}
