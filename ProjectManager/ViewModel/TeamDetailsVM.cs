using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using PMDataLayer;
using PMView.View.WrapperVM;

namespace PMView.View
{
    public class TeamDetailsVM : INotifyPropertyChanged
    {
        private ObservableCollection<User_Team> _employeesCollection = new ObservableCollection<User_Team>();

        private ObservableCollection<Skill> _skillsCollection = new ObservableCollection<Skill>();

        private ObservableCollection<Position> _positionsCollection = new ObservableCollection<Position>();

        private ObservableCollection<Position> _positionsToAddCollection = new ObservableCollection<Position>();

        private ObservableCollection<Order> _ordersCollection = new ObservableCollection<Order>();

        private ProjectsUserControlVM _projectsUserControlVM;

        private TeamVM _currentTeam;

        public TeamDetailsVM(Team team, ProjectsUserControlVM control)
        {
            if (team == null)
                return;

            _projectsUserControlVM = control;
            CurrentTeam = new TeamVM(team);
            OnPropertyChanged("Name");
            EmployeesCollection = new ObservableCollection<User_Team>();

            foreach (var employee in _employeesCollection)
            {
                var skills = from items in User_Skill.Items where items.User.Id == employee.User.Id select items;
                foreach (var item in skills)
                {
                    while (_skillsCollection.All(items => item.Skill != items))
                    _skillsCollection.Add(item.Skill);
                }
            }

            //LoadOrdersCollection();
            //LoadPositions();
            //LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public User SelectedEmployee { get; set; }

        public TeamVM CurrentTeam
        {
            get
            {
                return _currentTeam;
            }
            set
            {
                _currentTeam = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("Description");
            }
        }

        public string Name
        {
            get { return CurrentTeam.Name; }
            set
            {
                CurrentTeam.Name = value;
                OnPropertyChanged("Name");
                _projectsUserControlVM.OnPropertyChanged("TeamsCollection");

                //     _projectsUserControlVM.LoadData();    
            }
        }

        public string Description
        {
            get { return CurrentTeam.Description; }
            set
            {
                CurrentTeam.Description = value;
                _projectsUserControlVM.LoadData();
            }
        }

        public ObservableCollection<Order> OrdersCollection
        {
            get { return _ordersCollection; }
        }

        public ObservableCollection<Position> PositionsCollection
        {
            get { return _positionsCollection; }
        }

        public ObservableCollection<Position> PositionsToAddCollection
        {
            get { return _positionsToAddCollection; }
        }

        public IEnumerable<User_Team> Employees
        {
            get { return CurrentTeam.Users; }
        }

        public ObservableCollection<User_Team> EmployeesCollection
        {
            get { return _employeesCollection; }
            set
            {
                var users = CurrentTeam.Users.ToList();
                if (_employeesCollection.Count != 0)
                    _employeesCollection.Clear();

                for (int i = 0; i < users.Count; i++)
                {
                    bool exist = false;
                    for (int j = 0; j < _employeesCollection.Count; j++)
                    {
                        if (_employeesCollection[j].User == users[i].User)
                        {
                            exist = true;
                        }
                    }

                    if (!exist)
                        _employeesCollection.Add(users[i]);
                }
            }
        }

        public ObservableCollection<Skill> SkillsCollection
        {
            get { return _skillsCollection; }
        }

        public void LoadOrdersCollection()
        {
            //_ordersCollection.Clear();
            //var orders = from items in Team_Project.Items where items.Team == CurrentTeam select items.Project.Order;
            //foreach (var item in orders)
            //{
            //    if ((from items in orders where items == item select items).Count() == 1)
            //    {
            //        _ordersCollection.Add(item);
            //    }
            //}
        }

        public void AddPosition(Position position)
        {
            //if (SelectedEmployee == null)
            //    return;
            //if (position == null)
            //    return;
            //var pos = (from items in User_Team.Items where items.Team == CurrentTeam && items.User == SelectedEmployee && items.Position == position select items).FirstOrDefault();
            //if (pos != null)
            //    throw new Exception("This position is already exist");

            //User_Team ut = new User_Team()
            //{
            //    IsLeader = true,
            //    Position = position,
            //    Team = CurrentTeam,
            //    User = SelectedEmployee
            //};

            //User_Team.Items.Add(ut);
            //EmployeesCollection = new ObservableCollection<User_Team>();
            //LoadPositions();
            //LoadData();
        }

        public void RemovePosition(Position position)
        {
            if (SelectedEmployee == null)
                return;
            if (position == null)
                return;
            if (PositionsCollection.Count == 1)
            {
                throw new Exception("At least one position should be exist");
            }

            var ut = (from items in User_Team.Items where items.User == SelectedEmployee && items.Position == position select items).FirstOrDefault();
            User_Team.Items.Remove(ut);
     
            EmployeesCollection = new ObservableCollection<User_Team>();
            LoadPositions();
            LoadData();
        }

        public void LoadPositions()
        {
            _positionsCollection.Clear();
            foreach (var item in _employeesCollection)
            {
                if (item.User == SelectedEmployee)
                {
                    foreach (var position in item.Positions)
                    {
                        _positionsCollection.Add(position);
                    }
                }
            }

            _positionsToAddCollection.Clear();
            foreach (var item in Position.Items)
            {
                if (!_positionsCollection.Contains(item))
                    _positionsToAddCollection.Add(item);
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadData()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            OnPropertyChanged("Employees");
        }
    }
}
