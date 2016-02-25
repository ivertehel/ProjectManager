using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using PMDataLayer;
using Core;
using PMView.View.WrapperVM;
using System.Windows.Media.Imaging;

namespace PMView.View
{
    public class ProjectsUserControlVM : INotifyPropertyChanged, ILoadDataSender
    {
        private ObservableCollection<OrderVM> _ordersCollection = new ObservableCollection<OrderVM>();

        private ObservableCollection<ProjectVM> _projectsCollection = new ObservableCollection<ProjectVM>();

        private ObservableCollection<UserVM> _employeesCollection = new ObservableCollection<UserVM>();

        private ObservableCollection<TeamVM> _teamsCollection = new ObservableCollection<TeamVM>();

        private ObservableCollection<TaskVM> _tasksCollection = new ObservableCollection<TaskVM>();

        private ObservableCollection<SkillVM> _skillsCollection = new ObservableCollection<SkillVM>();

        private OrderVM _selectedOrder;
        private bool _editButton;
        private bool _removeButton;
        private ProjectVM _selectedModule;
        private UserVM _customer;

        public ProjectsUserControlVM()
        {
            Logger.Info("Details screen", "Project details have been loaded");
            if (User.Items.Count == 0)
                GenerateData();

            ////SelectedOrder = OrdersCollection.FirstOrDefault();
        }

        public ProjectsUserControlVM(Order order) : this()
        {
            Logger.Info("Order screen", "Details of " + order.Name + " order have been loaded");

            SelectedOrder.Order = order;
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public OrderVM SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                _customer = new UserVM(_selectedOrder.Client.User);
            }
        }

        public BitmapImage Image
        {
            get { return _customer.BitmapImage; }
        }

        public ObservableCollection<SkillVM> Skills
        {
            get
            {
                if (SelectedOrder == null)
                    return new ObservableCollection<SkillVM>();

                _skillsCollection.Clear();
                
                foreach (var item in SelectedOrder.Order.Projects)
                {
                    IEnumerable<Skill> inProj = from items in item.Skills where _skillsCollection.FirstOrDefault(x => x.Skill.Id == items.Id) == null select items;
                    foreach (var s in inProj)
                        _skillsCollection.Add(new SkillVM(s));
                }

                return _skillsCollection;
            }
        }

        public string CustomerName
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;
                return "Customer: " + SelectedOrder.Client.User.Name + " " + SelectedOrder.Client.User.Surname;
            }
        }

        public ProjectVM SelectedModule
        {
            get { return _selectedModule; }
            set
            {
                _selectedModule = value;
                if (_selectedModule != null)
                {
                    EditButton = true;
                    RemoveButton = true;
                }
                else
                {
                    EditButton = false;
                    RemoveButton = false;
                }
            }
        }

        public Order.Statuses ProjectStatus
        {
            get
            {
                if (SelectedOrder == null)
                    return Order.Statuses.Open;

                return SelectedOrder.Status;
            }
        }

        public string ReleaseDate
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return "Release date: " + SelectedOrder.ReleaseDate.ToShortDateString();
            }
        }

        public string StartDate
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return "Start date: " + SelectedOrder.StartDate.ToShortDateString();
            }
        }

        public string Price
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return "Price: " + SelectedOrder.Price + " USD";
            }
        }

        public string Description
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return SelectedOrder.Description;
            }
        }

        public string Name
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return SelectedOrder.Name;
            }
        }

        public bool EditButton
        {
            get { return _editButton; }
            set
            {
                _editButton = value;
                OnPropertyChanged("EditButton");
            }
        }

        public bool RemoveButton
        {
            get { return _removeButton; }
            set
            {
                _removeButton = value;
                OnPropertyChanged("RemoveButton");
            }
        }

        public ObservableCollection<OrderVM> OrdersCollection
        {
            get
            {
                _ordersCollection.Clear();
                foreach (var item in Order.Items)
                {
                    _ordersCollection.Add(new OrderVM(item));
                }

                return _ordersCollection;
            }
        }

        public ObservableCollection<ProjectVM> ProjectsCollection
        {
            get
            {
                _projectsCollection.Clear();
                var projects = from items in Project_Project.Items where items.ParrentProject == null && items.ChildProject.Order == SelectedOrder.Order select items.ChildProject;
                foreach (var item in projects)
                {
                    if (_projectsCollection.FirstOrDefault(proj => proj.Project.Id == item.Id) == null)
                        _projectsCollection.Add(new ProjectVM(item));
                }

                return _projectsCollection;
            }
        }

        public ObservableCollection<UserVM> EmployeesCollection
        {
            get
            {
                _employeesCollection.Clear();
                if (SelectedOrder == null)
                    return _employeesCollection;

                _employeesCollection.Clear();
                List<User> e = new List<User>();
                foreach (var item in SelectedOrder.Order.Projects)
                {
                    IEnumerable<User> inProj = from items in item.Users where !e.Exists(x => x.Id == items.Id) select items.User;
                    e.AddRange(inProj);
                }

                _employeesCollection.Clear();
                foreach (var item in e)
                {
                    if (_employeesCollection.FirstOrDefault(user => user.User.Id == item.Id) == null)
                        _employeesCollection.Add(new UserVM(item));
                }

                return _employeesCollection;
            }
        }

        public ObservableCollection<TeamVM> TeamsCollection
        {
            get
            {
                if (SelectedOrder == null)
                    return _teamsCollection;

                List<Team> t = new List<Team>();
                foreach (var item in SelectedOrder.Order.Projects)
                {
                    List<Team> inProj = new List<Team>();
                    foreach (var team in item.Teams)
                    {
                        if (t.FirstOrDefault(x => x.Id == team.Id) == null)
                            inProj.Add(team);
                    }

                    if (inProj != null)
                    foreach (var i in inProj)
                        t.Add(i);
                }

                List<TeamVM> teams = new List<TeamVM>();

                foreach (var item in t)
                {
                    teams.Add(new TeamVM(item));
                }

                _teamsCollection.Clear();

                foreach (var item in teams)
                {
                    if (!_teamsCollection.Contains(item))
                        _teamsCollection.Add(item);
                }

                return _teamsCollection;
            }
        }

        public void RemoveProject(ProjectVM projectVM)
        {
            Users_Project.Items.RemoveAll(item => item.Project.Id == projectVM.Project.Id);
            Project_Project.Items.Remove(Project_Project.Items.FirstOrDefault(item => item.ChildProject.Id == projectVM.Project.Id));
            Team_Project.Items.RemoveAll(item => item.Project.Id == projectVM.Project.Id);
            Projects_Skill.Items.RemoveAll(item => item.Project.Id == projectVM.Project.Id);
            List<Guid> generalProjects = new List<Guid>();
            foreach (var item in Project_Project.Items)
                if (item.ParrentProject == null)
                    generalProjects.Add(item.Id);

            Project.Items.Remove(Project.Items.FirstOrDefault(item => item.Id == projectVM.Project.Id));
            var toDelete = (from item in Project_Project.Items
                            where item.ParrentProject == null
                            select item).ToList();

            foreach (var item in generalProjects)
                toDelete.RemoveAll(p => p.Id == item);

            while (toDelete.Count() > 0)
            {
                toDelete = (from item in Project_Project.Items
                            where item.ParrentProject == null
                            select item).ToList();

                foreach (var item in generalProjects)
                    toDelete.RemoveAll(p => p.Id == item);

                foreach (var item in toDelete)
                {
                    Projects_Skill.Items.RemoveAll(ps => ps.Project.Id == item.ChildProject.Id);
                    Team_Project.Items.RemoveAll(tp => tp.Project.Id == item.ChildProject.Id);
                    Users_Project.Items.RemoveAll(user => user.Project.Id == item.ChildProject.Id);
                    Project.Items.Remove(Project.Items.FirstOrDefault(p => p.Id == item.ChildProject.Id));
                    Project_Project.Items.RemoveAll(project => project.Id == item.Id);
                }
            }

            EditButton = false;
            RemoveButton = false;
            LoadData();
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void LoadData()
        {
            if (SelectedOrder == null)
                return;

            LoadDetails();
        }

        public void LoadDetails()
        {
            Logger.Info("Order screen", "Details of " + SelectedOrder.Name + " have been loaded");
            OnPropertyChanged("TeamsCollection");
            OnPropertyChanged("Image");
            OnPropertyChanged("EmployeesCollection");
            OnPropertyChanged("ProjectsCollection");
            ////OnPropertyChanged("TasksCollection");
            OnPropertyChanged("Skills");
            OnPropertyChanged("Teams");
            OnPropertyChanged("Employees");
            OnPropertyChanged("CustomerName");
            OnPropertyChanged("ProjectStatus");
            OnPropertyChanged("ReleaseDate");
            OnPropertyChanged("StartDate");
            OnPropertyChanged("Price");
            OnPropertyChanged("Description");
            OnPropertyChanged("Name");
        }

        public void GenerateData()
        {
            User.Update();
            Client.Update();
            Order.Update();
            Project.Update();
            Project_Project.Update();
            Team.Update();
            Position.Update();
            Users_Team.Update();
            Skill.Update();
            Users_Project.Update();
            Projects_Skill.Update();
        }

        public void LoadData(object sender)
        {
            LoadData();
        }
    }
}
