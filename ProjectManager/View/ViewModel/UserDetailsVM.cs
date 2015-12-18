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
    public class UserDetailsVM : INotifyPropertyChanged
    {
        private UserVM _currentEmployee;

        private string _name;

        private string _surname;

        private User.States _state;

        private string _country;

        private DateTime _birthday;

        private string _email;

        private string _skype;

        private string _login;

        private string _description;

        private bool _buttonsActive = false;

        private ProjectsUserControlVM _projectsUserControlVM;

        private ObservableCollection<User.States> states = new ObservableCollection<User.States>();

        public UserDetailsVM(UserVM user, ProjectsUserControlVM projectsUserControlVM)
        {
            if (user == null)
                return;
            _projectsUserControlVM = projectsUserControlVM;
            CurrentEmployee = user;
            ButtonRetrieveClick();
            Logger.Info("User details screen", "Details of user " + user.Login + " has been loaded");

            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public UserVM CurrentEmployee
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value; }
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

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                ButtonsActive = true;
            }
        }

        public User.States State
        {
            get { return _state; }
            set
            {
                _state = value;
                ButtonsActive = true;
            }
        }

        public ObservableCollection<User.States> States
        {
            get
            {
                states.Clear();
                foreach (User.States state in Enum.GetValues(typeof(User.States)))
                {
                    states.Add(state);
                }
                return states;
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                ButtonsActive = true;
                _country = value;
            }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                ButtonsActive = true;
                _birthday = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                ButtonsActive = true;
                _email = value;
            }
        }

        public string Skype
        {
            get { return _skype; }
            set
            {
                ButtonsActive = true;
                _skype = value;
            }
        }


        public string Login
        {
            get { return _login; }
            set
            {
                ButtonsActive = true;
                _login = value;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                ButtonsActive = true;
                _description = value;
            }
        }

        public List<string> Countries
        {
            get
            {
                return User.Countries;
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ButtonSaveClick()
        {
            Logger.Info("User details screen", "Details of user has been changed:" + Environment.NewLine
            +"Name : "+ CurrentEmployee.Name + "  to " + _name + Environment.NewLine
            + "Surname : " + CurrentEmployee.Surname + "  to " + _surname + Environment.NewLine
            + "State : " + CurrentEmployee.State + "  to " + _state + Environment.NewLine
            + "Country : " + CurrentEmployee.Country + "  to " + _country + Environment.NewLine
            + "Birthday : " + CurrentEmployee.Birthday + "  to " + _birthday + Environment.NewLine
            + "Email : "+ CurrentEmployee.Email + "  to " + _email + Environment.NewLine
            + "Login : " + CurrentEmployee.Login + "  to " + _login + Environment.NewLine
            + "Description : " + CurrentEmployee.Description + "  to " + _description
            );
            CurrentEmployee.Name = _name;
            CurrentEmployee.Surname = _surname;
            CurrentEmployee.State = _state;
            CurrentEmployee.Country = _country;
            CurrentEmployee.Birthday = _birthday;
            CurrentEmployee.Email = _email;
            CurrentEmployee.Login = _login;
            CurrentEmployee.Description = _login;
            LoadData();
        }

        public void ButtonRetrieveClick()
        {
            _name = CurrentEmployee.Name;
            _surname = CurrentEmployee.Surname;
            _state = CurrentEmployee.State;
            _country = CurrentEmployee.Country;
            _birthday = CurrentEmployee.Birthday;
            _email = CurrentEmployee.Email;
            _login = CurrentEmployee.Login;
            _description = CurrentEmployee.Description;
            LoadData();

        }

        public void LoadData()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Surname");
            OnPropertyChanged("State");
            OnPropertyChanged("Country");
            OnPropertyChanged("Birthday");
            OnPropertyChanged("Email");
            OnPropertyChanged("Login");
            OnPropertyChanged("Description");
            ButtonsActive = false;
            _projectsUserControlVM.LoadData();
        }

    }
}
