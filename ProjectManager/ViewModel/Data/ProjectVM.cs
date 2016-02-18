using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PMDataLayer;

namespace PMView.View.WrapperVM
{
    public class ProjectVM : BaseVM
    {
        private Project _project;

        public ProjectVM()
        {

        }

        public ProjectVM(Project project)
        {
            _project = project;
        }

        public Project Project
        {
            get { return _project; }
            set { _project = value; }
        }

        public string Name
        {
            get { return _project.Name; }
            set { _project.Name = value; }
        }

        public string Description
        {
            get { return _project.Description; }
            set { _project.Description = value; }
        }

        public DateTime StartDate
        {
            get { return _project.StartDate; }
            set { _project.StartDate = value; }
        }

        public DateTime ReleaseDate
        {
            get { return _project.ReleaseDate; }
            set { _project.ReleaseDate = value; }
        }

        public Project.Statuses Status
        {
            get { return _project.StatusType; }
            set { _project.StatusType = value; }
        }

        public Order Order
        {
            get { return _project.Order; }
            set { _project.Order = value; }
        }

        public User Leader
        {
            get { return _project.Leader; }
            set { _project.Leader = value; }
        }

        public IEnumerable<Project> ChildProjects
        {
            get { return _project.ChildProjects; }
        }

        public IEnumerable<Project> ParrentProjects
        {
            get { return _project.ParrentProjects; }
        }

        public IEnumerable<Users_Project> Users
        {
            get { return _project.Users; }
        }

        public IEnumerable<Skill> Skills
        {
            get { return _project.Skills; }
        }

        public IEnumerable<Team> Teams
        {
            get { return _project.Teams; }
        }

        public IEnumerable<Task> Tasks
        {
            get { return _project.Tasks; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
