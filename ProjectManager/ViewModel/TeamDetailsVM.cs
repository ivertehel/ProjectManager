﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using PMDataLayer;
using PMView.View.WrapperVM;
using Core;

namespace PMView.View
{
    public class TeamDetailsVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private ObservableCollection<UserVM> _employeesCollection = new ObservableCollection<UserVM>();

        private ObservableCollection<SkillVM> _skillsCollection = new ObservableCollection<SkillVM>();

        private ObservableCollection<OrderVM> _ordersCollection = new ObservableCollection<OrderVM>();

        private ProjectsUserControlVM _projectsUserControlVM;

        private ILoadDataSender _lastScreen;

        private ObservableCollection<User_TeamVM> _employeesPositions = new ObservableCollection<User_TeamVM>();

        private bool _buttonsActive = false;

        private TeamVM _currentTeam;

        private UserVM _selectedEmployee;
        private List<User_TeamVM> _savedPositions;

        public TeamDetailsVM(TeamVM team, ProjectsUserControlVM control)
        {
            if (team == null)
                return;

            Logger.Info("Team details screen", "Team details have been loaded");
            _projectsUserControlVM = control;
            CurrentTeam = team;
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            OnPropertyChanged("OrdersCollection");
            OnPropertyChanged("EmployeesCollection");
        }

        public TeamDetailsVM(TeamVM team, ILoadDataSender lastScreen)
        {
            if (team == null)
                return;

            Logger.Info("Team details screen", "Team details have been loaded");
            CurrentTeam = team;
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            OnPropertyChanged("OrdersCollection");
            OnPropertyChanged("EmployeesCollection");
            _lastScreen = lastScreen;
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

        public bool ButtonsActive
        {
            get { return _buttonsActive; }
            set
            {
                _buttonsActive = value;
                OnPropertyChanged("ButtonsActive");
            }
        }

        public string Name
        {
            get { return _currentTeam.Name; }
            set
            {
                _currentTeam.Name = value;
                ButtonsActive = true;
            }
        }

        public string Description
        {
            get { return _currentTeam.Description; }
            set
            {
                _currentTeam.Description = value;
                ButtonsActive = true;
            }
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

        public List<User_TeamVM> SavedPositions
        {
            get { return _savedPositions; }
            set { _savedPositions = value; }
        }

        public ObservableCollection<User_TeamVM> EmployeesPositions
        {
            get
            {
                _employeesPositions.Clear();
                var user = _employeesCollection.FirstOrDefault(item => item.User.Id == SelectedEmployee.User.Id);

                foreach (var item in Users_Team.Items)
                {
                    if (_employeesPositions.FirstOrDefault(pos => item.Position.Id == pos.Position.Id) == null
                        && item.User.Id == user.User.Id)
                        _employeesPositions.Add(new User_TeamVM(item));
                }

                return _employeesPositions;             
            }
        }

        public ObservableCollection<UserVM> EmployeesCollection
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
                        _employeesCollection.Add(new UserVM(users[i].User));
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
                    var skills = from items in Users_Skill.Items where items.User.Id == employee.User.Id select items;
                    foreach (var item in skills)
                    {
                        while (_skillsCollection.All(items => item.Skill != items.Skill))
                            _skillsCollection.Add(new SkillVM(item.Skill));
                    }
                }

                return _skillsCollection;
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                        {
                            error = "Name can't be empty";
                        }
                        else if (Name[0] == ' ')
                        {
                            error = "Name can't start off space";
                        }

                        if (Team.Items.Where(items => items.Name == Name && CurrentTeam.Name != Name).Count() != 0)
                        {
                            error = "This team is already exist";
                        }

                        break;
                    case "Description":
                        if (string.IsNullOrEmpty(Description))
                        {
                            error = string.Empty;
                        }
                        else if (Description[0] == ' ')
                        {
                            error = "Description can't start off space";
                        }

                        break;
                }

                return error;
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ButtonSaveClick(string[] positions)
        {
            var error = this["Name"];
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }

            Logger.Info("Team details screen", $@"Details of team has been changed: {Environment.NewLine} 
            Name : {CurrentTeam.Name} to {Name} {Environment.NewLine} 
            Description : {CurrentTeam.Description} to {Description}");
            if (positions != null)
                _savePositions(positions);

            CurrentTeam.Name = Name;
            CurrentTeam.Description = Description;
            if (_projectsUserControlVM != null)
                _projectsUserControlVM.OnPropertyChanged("TeamsCollection");
            else _lastScreen.LoadData(this);
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            ButtonsActive = false;
        }

        public void LoadData()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            OnPropertyChanged("OrdersCollection");
            OnPropertyChanged("EmployeesCollection");
            OnPropertyChanged("SkillsCollection");
        }

        private void _savePositions(string[] positions)
        {
            if (positions.Count() == 0)
                throw new Exception("Employee must have at least one position");
            Users_Team.Items.RemoveAll(items => items.User == SelectedEmployee.User);
            for (int i = 0; i < positions.Count(); i++)
            {
                Users_Team ut = new Users_Team()
                {
                    IsLeader = false,
                    Position = Position.Items.Where(item => item.Name == positions[i]).FirstOrDefault(),
                    Team = CurrentTeam.Team,
                    User = SelectedEmployee.User
                };
                Users_Team.Items.Add(ut);
            }
        }
    }
}
