using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMDataLayer;
using PMView.View.WrapperVM;
using System.ComponentModel;

namespace PMView.View
{
    public class AddEmployeeToTheTeamVM : IAddEmployee, INotifyPropertyChanged
    {
        private AttachEmployee _attachEmployee;
        private ILoadDataSender _lastScreen;
        private TeamDetailsVM _teamDetailsVM;
        private string _name;
        private string _surname;
        private string _login;
        private string _email;
        private string _skype;
        private string _country;
        private List<User_TeamVM> _savedPositions;
        private ObservableCollection<string> _employeesPositions = new ObservableCollection<string>();
        private bool _saveButton;
        private bool _savePositionButton;
        private UserVM _selectedEmployeeToDelete;
        private ObservableCollection<UserVM> _employeesToAddCollection = new ObservableCollection<UserVM>();
        private bool _removeButton;
        private bool _addButton;
        private bool _profileButton;
        private List<string> _selectedSkills = new List<string>();
        private User.States _state;
        private User.Statuses _status;
        private ObservableCollection<UserVM> _employeesCollection = new ObservableCollection<UserVM>();
        private ObservableCollection<User.States> _states = new ObservableCollection<User.States>();
        private ObservableCollection<SkillVM> _skillsCollection = new ObservableCollection<SkillVM>();
        private ObservableCollection<User.Statuses> _statuses = new ObservableCollection<User.Statuses>();

        public AddEmployeeToTheTeamVM(ILoadDataSender lastScreen, TeamDetailsVM teamDetailsVM, AttachEmployee attachEmployee)
        {
            _lastScreen = lastScreen;
            _teamDetailsVM = teamDetailsVM;
            _attachEmployee = attachEmployee;
            foreach (var item in _teamDetailsVM.EmployeesCollection)
                _employeesToAddCollection.Add(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Skype
        {
            get { return _skype; }
            set { _skype = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public ObservableCollection<UserVM> EmployeesToAddCollection
        {
            get
            {
                return _employeesToAddCollection;
            }
        }

        public List<string> Countries
        {
            get
            {
                List<string> usedCountries = (from items in User.Items where items.RoleType == User.Roles.Employee select items.Country).ToList();
                usedCountries.RemoveAll(item => usedCountries.Count(value => value == item) == 0);
                usedCountries.Add("NotChosen");
                usedCountries.Sort();

                // repeats deleting
                for (int i = 0; i < usedCountries.Count; i++)
                {
                    var save = usedCountries[i];
                    if (usedCountries.Count(items => items == save) > 1)
                    {
                        usedCountries.RemoveAll(items => items == save);
                        usedCountries.Add(save);
                    }
                }

                return usedCountries;
            }
        }

        public ObservableCollection<User.Statuses> Statuses
        {
            get
            {
                if (_statuses.Count == 0)
                    foreach (User.Statuses status in Enum.GetValues(typeof(User.Statuses)))
                    {
                        if (status != User.Statuses.UnInvited)
                            _statuses.Add(status);
                    }

                return _statuses;
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

        public ObservableCollection<User.States> States
        {
            get
            {
                if (_states.Count == 0)
                    foreach (User.States state in Enum.GetValues(typeof(User.States)))
                    {
                        _states.Add(state);
                    }

                return _states;
            }
        }

        public ObservableCollection<UserVM> EmployeesCollection
        {
            get
            {
                var users = User.Items.Where(item => item.RoleType == User.Roles.Employee);
                _employeesCollection.Clear();
                foreach (var item in users)
                {
                    _employeesCollection.Add(new UserVM(item));
                }

                filterEmployeesCollection();
                return _employeesCollection;
            }
        }

        public ObservableCollection<string> EmployeesPositions
        {
            get
            {
                if (_savedPositions == null)
                {
                    _savedPositions = new List<User_TeamVM>();

                    foreach (var item in Users_Team.Items)
                    {
                        if (item.Team.Id == _teamDetailsVM.CurrentTeam.Team.Id)
                            _savedPositions.Add(new User_TeamVM(item));
                    }
                }

                _employeesPositions.Clear();
                if (SelectedEmployeeToDelete != null)
                {
                    foreach (var elem in _savedPositions)
                    {
                        if (elem.User.Id == SelectedEmployeeToDelete.User.Id)
                            _employeesPositions.Add(elem.Position.Name);
                    }
                }

                return _employeesPositions;
            }
        }

        public bool RemoveButton
        {
            get { return _removeButton; }
            set { _removeButton = value; }
        }

        public bool AddButton
        {
            get { return _addButton; }
            set { _addButton = value; }
        }

        public bool SaveButton
        {
            get { return _saveButton; }
            set
            {
                _saveButton = value;
                OnPropertyChanged("SaveButton");
            }
        }

        public bool ProfileButton
        {
            get { return _profileButton; }
            set { _profileButton = value; }
        }

        public bool SavePositionButton
        {
            get { return _savePositionButton; }
            set
            {
                _savePositionButton = value;
                OnPropertyChanged("SavePositionButton");
            }
        }

        public UserVM SelectedEmployeeToDelete
        {
            get { return _selectedEmployeeToDelete; }
            set
            {
                if (value != null)
                {
                    if (_employeesToAddCollection.Where(item => item.User.Id == value.User.Id).Count() > 0)
                    {
                        RemoveButton = true;
                        AddButton = false;
                    }
                    else
                    {
                        RemoveButton = false;
                        AddButton = true;
                    }

                    ProfileButton = true;
                    OnPropertyChanged("RemoveButton");
                    OnPropertyChanged("AddButton");
                    OnPropertyChanged("ProfileButton");
                }

                _selectedEmployeeToDelete = value;
            }
        }

        public List<string> SelectedSkills
        {
            get { return _selectedSkills; }
            set { _selectedSkills = value; }
        }

        public User.States State
        {
            get { return _state; }
            set { _state = value; }
        }

        public User.Statuses Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public void ActivateButtons(UserVM selectedEmployeeToDelete)
        {
            SelectedEmployeeToDelete = selectedEmployeeToDelete;
        }

        public void AddButtonClick(UserVM user)
        {
            if (_employeesToAddCollection.Where(item => item.Equals(user)).Count() == 0)
            {
                _employeesToAddCollection.Add(user);
                AddButton = false;
                ProfileButton = false;
                SaveButton = true;
                OnPropertyChanged("AddButton");
                OnPropertyChanged("ProfileButton");
                OnPropertyChanged("SaveButton");
                LoadData(this);
            }
            else
            {
                throw new Exception("This employee is already exist");
            }
        }

        public void LoadData(object sender)
        {
            OnPropertyChanged("EmployeesPositions");
            OnPropertyChanged("EmployeesToAddCollection");
            OnPropertyChanged("EmployeesCollection");
            OnPropertyChanged("SkillsCollection");
            if (_lastScreen != null)
                _lastScreen.LoadData(sender);
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RemoveButtonClick(UserVM select)
        {
            throw new NotImplementedException();
        }

        public void SaveButtonClick()
        {
            foreach (var emp in _employeesToAddCollection)
            {
                if (_savedPositions.FirstOrDefault(item => item.User.Id == emp.User.Id) == null)
                {
                    throw new Exception("One or more users don't contain any position");
                }
            }

            if (_savedPositions != null)
            {
                Users_Team.Items.RemoveAll(item => item.Team.Id == _teamDetailsVM.CurrentTeam.Team.Id);
                foreach (var item in _savedPositions)
                    Users_Team.Items.Add(item.User_Team);
            }

            SaveButton = false;
            OnPropertyChanged("SaveButton");
            LoadData(this);
        }

        public void SavePositionsClick(List<string> positions)
        {
            _savedPositions.RemoveAll(item => item.User.Id == SelectedEmployeeToDelete.User.Id);
            foreach (var item in positions)
            {
                _savedPositions.Add(new User_TeamVM(new Users_Team() { Position = Position.Items.FirstOrDefault(pos => pos.Name == item), IsLeader = false, Team = _teamDetailsVM.CurrentTeam.Team, User = SelectedEmployeeToDelete.User }));
            }
        }

        private void filterEmployeesCollection()
        {
            var employees = _employeesCollection.ToList();
            if (!string.IsNullOrEmpty(Name))
                employees.RemoveAll(item => !item.Name.ToUpper().StartsWith(Name.ToUpper()));

            if (!string.IsNullOrEmpty(Surname))
                employees.RemoveAll(item => !item.Surname.ToUpper().StartsWith(Surname.ToUpper()));

            if (!string.IsNullOrEmpty(Login))
                employees.RemoveAll(item => !item.Login.ToUpper().StartsWith(Login.ToUpper()));

            if (!string.IsNullOrEmpty(Skype))
                employees.RemoveAll(item => !item.Skype.ToUpper().StartsWith(Skype.ToUpper()));

            if (!string.IsNullOrEmpty(Email))
                employees.RemoveAll(item => !item.Email.ToUpper().StartsWith(Email.ToUpper()));

            if (Country != "NotChosen")
                employees.RemoveAll(item => item.Country != Country);

            if (Status != User.Statuses.NotChosen)
                employees.RemoveAll(item => item.Status != Status);

            if (State != User.States.NotChosen)
                employees.RemoveAll(item => item.State != State);

            if (_selectedSkills.Count != 0)
            {
                List<string> skillNames = new List<string>();

                foreach (var item in _selectedSkills)
                {
                    employees.RemoveAll(employee => employee.Skills.Where(skill => skill.Name == item).FirstOrDefault() == null);
                }
            }

            _employeesCollection.Clear();
            foreach (var item in employees)
            {
                _employeesCollection.Add(item);
            }
        }
    }
}
