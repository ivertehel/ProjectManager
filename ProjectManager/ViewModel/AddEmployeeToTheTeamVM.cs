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
        private ObservableCollection<string> _employeesPositions;
        private bool _saveButton;
        private bool _savePositionButton;
        private UserVM _selectedEmployeeToDelete;
        private ObservableCollection<UserVM> _employeesToAddCollection = new ObservableCollection<UserVM>();
        private bool _removeButton;
        private bool _addButton;
        private bool _profileButton;
        private List<string> _selectedSkills;
        private User.States _state;
        private User.Statuses _status;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddEmployeeToTheTeamVM(ILoadDataSender lastScreen, TeamDetailsVM teamDetailsVM, AttachEmployee attachEmployee)
        {
            _lastScreen = lastScreen;
            _teamDetailsVM = teamDetailsVM;
            _attachEmployee = attachEmployee;
        }

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

        public ObservableCollection<string> EmployeesPositions
        {
            get
            {
                if (_savedPositions == null)
                {
                    _savedPositions = new List<User_TeamVM>();

                    foreach (var item in User_Team.Items)
                    {
                        _savedPositions.Add(new User_TeamVM(item));
                    }
                }

                _employeesPositions.Clear();
                foreach (var elem in (from items in _savedPositions where items.User.Id == SelectedEmployeeToDelete.User.Id select items.Position.Name).ToList())
                {
                    _employeesPositions.Add(elem);
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

            _teamDetailsVM.EmployeesCollection.Clear();

            foreach (var item in _employeesToAddCollection)
            {
                _teamDetailsVM.EmployeesCollection.Add(item);
            }

            if (_savedPositions != null)
            {
                _teamDetailsVM.SavedPositions.Clear();

                foreach (var item in _savedPositions)
                {
                    _teamDetailsVM.SavedPositions.Add(item);
                }
            }

            SaveButton = false;
            OnPropertyChanged("SaveButton");
            LoadData(this);
        }

        public void SavePositionsClick(List<string> list)
        {
            throw new NotImplementedException();
        }
    }
}
