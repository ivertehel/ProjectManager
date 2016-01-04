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

        public List<string> Countries
        {
            get
            {
                return User.Countries;
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
                foreach (var item in users)
                {
                    _employeesCollection.Add(new UserVM(item));
                }
                return _employeesCollection;
            }
        }
    }
}
