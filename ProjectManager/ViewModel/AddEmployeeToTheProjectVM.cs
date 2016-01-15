using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PMDataLayer;
using PMView.View.WrapperVM;

namespace PMView.View
{
    public class AddEmployeeToTheProjectVM : INotifyPropertyChanged, ILoadDataSender, IAddEmployee
    {
        private string _name;

        private string _surname;
        private string _login;
        private string _email;
        private string _skype;
        private ObservableCollection<User.States> _states = new ObservableCollection<User.States>();
        private ObservableCollection<User.Statuses> _statuses = new ObservableCollection<User.Statuses>();
        private ObservableCollection<SkillVM> _skillsCollection = new ObservableCollection<SkillVM>();
        private string _country;
        private User.Statuses _status;
        private User.States _state;
        private List<string> _selectedSkills = new List<string>();
        private ObservableCollection<User_ProjectVM> _employeesToAddCollection = new ObservableCollection<User_ProjectVM>();
        private UserVM _selectedEmployeeToDelete;
        private bool _removeButton;
        private bool _addButton;
        private bool _profileButton;
        private ILoadDataSender _lastScreen;
        private bool _saveButton;
        private ProjectModuleEditVM _projectModuleEditVM;
        private List<User_ProjectVM> _employeesPositions = new List<User_ProjectVM>();
        private bool _savePositionButton;
        private AttachEmployee _screen;

        public AddEmployeeToTheProjectVM(ILoadDataSender lastScreen, ProjectModuleEditVM projectModuleEditVM, AttachEmployee screen)
        {
            _screen = screen;
            foreach (var item in projectModuleEditVM.EmployeesCollection)
            {
                _employeesToAddCollection.Add(item);
            }

            if (projectModuleEditVM.SavedPositions != null)
            {
                foreach (var item in projectModuleEditVM.SavedPositions)
                    _employeesPositions.Add(item);
            }

            _projectModuleEditVM = projectModuleEditVM;
            _lastScreen = lastScreen;
            LoadData(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> SelectedSkills
        {
            get { return _selectedSkills; }
            set { _selectedSkills = value; }
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

        public User.Statuses Status
        {
            get { return _status; }
            set { _status = value; }
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

        public List<string> Countries
        {
            get
            {
                List<string> usedCountries = (from items in User.Items where items.Role == User.Roles.Employee select items.Country).ToList();
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
                foreach (var employee in EmployeesPositions)
                {
                    var skills = from items in User_Skill.Items where items.User.Id == (employee as User_ProjectVM).User.Id select items;
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

        public void SavePositionsClick(List<PositionVM> positions)
        {
            _employeesPositions.RemoveAll(item => item.User.Id == SelectedEmployeeToDelete.User.Id);
            foreach (var item in positions)
            {
                _employeesPositions.Add(new User_ProjectVM(new User_Project() { User = SelectedEmployeeToDelete.User, Project = _projectModuleEditVM.ProjectVM.Project, Position = item.Position }));
            }
        }

        public ObservableCollection<User_ProjectVM> EmployeesToAddCollection
        {
            get
            {
                return _employeesToAddCollection;
            }
        }

        public User.States State
        {
            get { return _state; }
            set { _state = value; }
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

        public ObservableCollection<IEmployee> EmployeesPositions
        {
            get
            {
                ObservableCollection<IEmployee> employeesInProject = new ObservableCollection<IEmployee>();
                foreach (var item in _employeesPositions)
                {
                    employeesInProject.Add(item);
                }

                return employeesInProject;
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void ActivateButtons(IEmployee selectedEmployeeToDelete)
        {
            SelectedEmployeeToDelete = selectedEmployeeToDelete as UserVM;
        }

        public void AddButtonClick(IEmployee user)
        {
            
            if (_employeesToAddCollection.Where(item => item.Equals(user)).Count() == 0)
            {
                _employeesToAddCollection.Add(user as User_ProjectVM);
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

        public void SaveButtonClick()
        {
            foreach (var emp in _employeesToAddCollection)
            {
                if (_employeesPositions.FirstOrDefault(item => item.User.Id == emp.User.Id) == null)
                {
                    throw new Exception("One or more users don't contain any position");
                }
            }

            _projectModuleEditVM.EmployeesCollection.Clear();

            foreach (var item in _employeesToAddCollection)
            {
                _projectModuleEditVM.EmployeesCollection.Add(item);
            }

            if (_employeesPositions != null)
            {
                _projectModuleEditVM.SavedPositions.Clear();

                foreach (var item in _employeesPositions)
                {
                    _projectModuleEditVM.SavedPositions.Add(item);
                }
            }

            SaveButton = false;
            OnPropertyChanged("SaveButton");
            LoadData(this);
        }


        public void RemoveButtonClick(IEmployee employee)
        {
            var user = employee as UserVM;

            if (_employeesToAddCollection.Where(item => item.User.Id == user.User.Id).Count() != 0)
            {
                var toDelete = _employeesToAddCollection.First(item => item.User.Id == user.User.Id);
                _employeesToAddCollection.Remove(toDelete);
                RemoveButton = false;
                ProfileButton = false;
                SaveButton = true;
                _employeesPositions.RemoveAll(item => item.User.Id == user.User.Id);
                OnPropertyChanged("RemoveButton");
                OnPropertyChanged("ProfileButton");
                OnPropertyChanged("SaveButton");
                LoadData(this);
            }
        }

        private void filterEmployeesCollection()
        {
            var employees = new List<UserVM>();
            foreach (var item in _employeesPositions)
                employees.Add(new UserVM((item as User_ProjectVM).User));

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
    }
}
