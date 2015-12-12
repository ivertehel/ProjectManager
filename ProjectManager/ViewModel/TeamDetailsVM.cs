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
        private ObservableCollection<User_TeamVM> _employeesCollection = new ObservableCollection<User_TeamVM>();

        private ObservableCollection<SkillVM> _skillsCollection = new ObservableCollection<SkillVM>();

        private ObservableCollection<PositionVM> _positionsCollection = new ObservableCollection<PositionVM>();

        private ObservableCollection<PositionVM> _positionsToAddCollection = new ObservableCollection<PositionVM>();

        private ObservableCollection<OrderVM> _ordersCollection = new ObservableCollection<OrderVM>();

        private ProjectsUserControlVM _projectsUserControlVM;

        private TeamVM _currentTeam;

        private UserVM _selectedEmployee;

        public TeamDetailsVM(TeamVM team, ProjectsUserControlVM control)
        {
            if (team == null)
                return;

            _projectsUserControlVM = control;
            CurrentTeam = team;
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public UserVM SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; }
        }

        public TeamVM CurrentTeam
        {
            get { return _currentTeam; }
            set { _currentTeam = value; }
        }

        public string Name
        {
            get { return CurrentTeam.Name; }
            set { CurrentTeam.Name = value; _projectsUserControlVM.OnPropertyChanged("TeamsCollection"); }
        }

        public string Description
        {
            get { return CurrentTeam.Description; }
            set { CurrentTeam.Description = value; }
        }

        public ObservableCollection<OrderVM> OrdersCollection
        {
            get
            {
                _ordersCollection.Clear();
                var orders = from items in Team_Project.Items where items.Team == CurrentTeam.Team select items.Project.Order;
                foreach (var item in orders)
                {
                    if ((from items in orders where items == item select items).Count() == 1)
                    {
                        _ordersCollection.Add(new OrderVM(item));
                    }
                }

                return _ordersCollection;
            }
        }

        public ObservableCollection<PositionVM> PositionsCollection
        {
            get
            {
                _positionsCollection.Clear();
                foreach (var item in _employeesCollection)
                {
                    if (item.User == SelectedEmployee.User)
                    {
                        foreach (var position in item.Positions)
                        {
                            _positionsCollection.Add(new PositionVM(position));
                        }
                    }
                }
                return _positionsCollection;
            }
        }

        public ObservableCollection<PositionVM> PositionsToAddCollection
        {
            get
            {
                _positionsToAddCollection.Clear();
                foreach (var position in Position.Items)
                {
                    bool exist = false;
                    foreach (var item in _positionsCollection)
                    {
                        if (item.Position.Name == position.Name)
                        {
                            exist = true;
                        }
                    }
                    if (!exist)
                        _positionsToAddCollection.Add(new PositionVM(position));
                }
                return _positionsToAddCollection;
            }
        }

        public ObservableCollection<User_TeamVM> EmployeesCollection
        {
            get
            {
                var users = CurrentTeam.Users.ToList();
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
                        _employeesCollection.Add(new User_TeamVM(users[i]));
                }

                return _employeesCollection;
            }
        }

        public ObservableCollection<SkillVM> SkillsCollection
        {
            get
            {
                _skillsCollection.Clear();
                foreach (var employee in _employeesCollection)
                {
                    var skills = from items in User_Skill.Items where items.User.Id == employee.User.Id select items;
                    foreach (var item in skills)
                    {
                        while (_skillsCollection.All(items => item.Skill != items.Skill))
                            _skillsCollection.Add(new SkillVM(item.Skill));
                    }
                }
                return _skillsCollection;
            }
        }

        public void AddPosition(PositionVM position)
        {
            if (SelectedEmployee == null)
                return;
            if (position == null)
                return;
            var pos = (from items in User_Team.Items where items.Team == CurrentTeam.Team && items.User == SelectedEmployee.User && items.Position.Name == position.Name select items).FirstOrDefault();

            User_Team ut = new User_Team()
            {
                IsLeader = true,
                Position = position.Position,
                Team = CurrentTeam.Team,
                User = SelectedEmployee.User
            };

            User_Team.Items.Add(ut);
            LoadData();
        }

        public void RemovePosition(PositionVM position)
        {
            if (SelectedEmployee == null)
                return;
            if (position == null)
                return;
            if (PositionsCollection.Count == 1)
            {
                throw new Exception("At least one position should be exist");
            }

            var ut = (from items in User_Team.Items where items.User == SelectedEmployee.User && items.Position.Name == position.Name select items).FirstOrDefault();
            User_Team.Items.Remove(ut);
            LoadData();
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadData()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            OnPropertyChanged("OrdersCollection");
            OnPropertyChanged("EmployeesCollection");
            OnPropertyChanged("SkillsCollection");
            ChangePositions();
        }

        public void ChangePositions()
        {
            OnPropertyChanged("PositionsCollection");
            OnPropertyChanged("PositionsToAddCollection");
        }
    }
}
