using PMDataLayer;
using PMView.View.WrapperVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMView.View
{
    public class AddEmployeeToTheProjectVM : INotifyPropertyChanged
    {
        private ObservableCollection<UserVM> _employeesCollection = new ObservableCollection<UserVM>();

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

        public event PropertyChangedEventHandler PropertyChanged;

        public AddEmployeeToTheProjectVM()
        {

        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
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

        public List<string> Countries
        {
            get
            {
                List<string> usedCountries = (from items in User.Items where items.Role == User.Roles.Employee select items.Country).ToList();
                usedCountries.RemoveAll(item => usedCountries.Count(value => value == item) == 0 && item != "NotChoosen");
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
                var users = User.Items.Where(item => item.Role == User.Roles.Employee);
                _employeesCollection.Clear();
                foreach (var item in users)
                {
                    _employeesCollection.Add(new UserVM(item));
                }
                filterEmployeesCollection();
                return _employeesCollection;
            }
        }

        public User.States State
        {
            get { return _state; }
            set { _state = value; }
        }

        private void filterEmployeesCollection()
        {
            var employees = _employeesCollection.ToList();
            if (!string.IsNullOrEmpty(Name))
                employees.RemoveAll(item => !item.Name.StartsWith(Name));

            if (!string.IsNullOrEmpty(Surname))
                employees.RemoveAll(item => !item.Surname.StartsWith(Surname));

            if (!string.IsNullOrEmpty(Login))
                employees.RemoveAll(item => !item.Login.StartsWith(Login));

            if (!string.IsNullOrEmpty(Skype))
                employees.RemoveAll(item => !item.Skype.StartsWith(Skype));

            if (!string.IsNullOrEmpty(Email))
                employees.RemoveAll(item => !item.Email.StartsWith(Email));

            if (!string.IsNullOrEmpty(Country))
                employees.RemoveAll(item => item.Country != Country);

            if (Status != User.Statuses.NotChoosen)
                employees.RemoveAll(item => item.Status != Status);

            if (State != User.States.NotChoosen)
                employees.RemoveAll(item => item.State != State);

            _employeesCollection.Clear();
            foreach (var item in employees)
            {
                _employeesCollection.Add(item);
            }
        }
    }
}
