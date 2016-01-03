using System;
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
        private ObservableCollection<User_TeamVM> _employeesCollection = new ObservableCollection<User_TeamVM>();

        private ObservableCollection<SkillVM> _skillsCollection = new ObservableCollection<SkillVM>();

        private ObservableCollection<OrderVM> _ordersCollection = new ObservableCollection<OrderVM>();

        private ProjectsUserControlVM _projectsUserControlVM;

        private List<string> _employeesPositions = new List<string>();

        private string _name;

        private string _description;

        private bool _buttonsActive = false;

        private TeamVM _currentTeam;

        private UserVM _selectedEmployee;

        public TeamDetailsVM(TeamVM team, ProjectsUserControlVM control)
        {
            if (team == null)
                return;

            Logger.Info("Team details screen", "Team details have been loaded");
            _projectsUserControlVM = control;
            CurrentTeam = team;
            ButtonRetrieveClick();
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            OnPropertyChanged("OrdersCollection");
            OnPropertyChanged("EmployeesCollection");
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
            get
            {
                return _buttonsActive;
            }
            set
            {
                _buttonsActive = value;
                OnPropertyChanged("ButtonsActive");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                ButtonsActive = true;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                ButtonsActive = true;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (Name == string.Empty)
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
                        if (Description == string.Empty)
                        {
                            error = "Description can't be empty";
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

        public List<string> EmployeesPositions
        {
            get
            {
                _employeesPositions.Clear();
                var user = _employeesCollection.FirstOrDefault(item => item.User == SelectedEmployee.User);
                foreach (var item in Position.Items)
                {
                    if(!user.Positions.Contains(item))
                        _employeesPositions.Add(item.Name);
                }
                return _employeesPositions;             
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

        public string Error
        {
            get
            {
                throw new NotImplementedException();
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
            if (error == string.Empty)
                error = this["Description"];
            if (error != string.Empty)
            {
                throw new Exception(error);
            }
            Logger.Info("Team details screen", $@"Details of team has been changed: {Environment.NewLine} 
            Name : {CurrentTeam.Name} to {_name} {Environment.NewLine} 
            Description : {CurrentTeam.Description} to {_description}");
            _savePositions(positions);
            CurrentTeam.Name = _name;
            CurrentTeam.Description = _description;
            _projectsUserControlVM.OnPropertyChanged("TeamsCollection");
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            ButtonsActive = false;
        }

        private void _savePositions(string[] positions)
        {
            if (positions.Count() == 0)
                throw new Exception("Employee must have at least one position");
            User_Team.Items.RemoveAll(items => items.User == SelectedEmployee.User);
            for (int i = 0; i < positions.Count(); i++)
            {
                User_Team ut = new User_Team()
                {
                    IsLeader = false,
                    Position = Position.Items.Where(item => item.Name == positions[i]).FirstOrDefault(),
                    Team = CurrentTeam.Team,
                    User = SelectedEmployee.User
                };
                User_Team.Items.Add(ut);
            }
        }

        public void ButtonRetrieveClick()
        {
            Name = CurrentTeam.Name;
            Description = CurrentTeam.Description;
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
    }
}
