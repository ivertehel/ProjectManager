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
    public class UserDetailsVM : INotifyPropertyChanged, IDataErrorInfo, ILoadData
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

        private ILoadData _lastScreen;

        private ObservableCollection<User.States> _states = new ObservableCollection<User.States>();

        public UserDetailsVM(IUser user, ILoadData lastScreen)
        {
            if (user == null)
                return;

            _lastScreen = lastScreen;
            CurrentUser = user as UserVM;
            ButtonRetrieveClick();
            Logger.Info("User details screen", "Details of user " + CurrentUser.Login + " has been loaded");
            User.Update();
            LoadData();
            ButtonsActive = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ButtonsActive
        {
            get { return _buttonsActive; }
            set
            {
                _buttonsActive = value;
                OnPropertyChanged("ButtonsActive");
            }
        }

        public UserVM CurrentUser
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value; }
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

        public User.States State
        {
            get { return _state; }
            set { _state = value; }
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

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
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

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public List<string> Countries
        {
            get
            {
                return User.Countries;
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
                        error = _checkNameTemplate(columnName, Name);
                        break;
                    case "Surname":
                        error = _checkNameTemplate(columnName, Surname);
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

        public void OneOrMoreFieldsWereUpdated()
        {
            ButtonsActive = true;
        }

        public void ButtonSaveClick()
        {
            var error = this["Name"];
            if (error == string.Empty)
                error = this["Surname"];
            if (error != string.Empty)
            {
                throw new Exception(error);
            }

            Logger.Info("User details screen",
                $@"Details of user has been changed:{Environment.NewLine}
                Name : {CurrentUser.Name} to {_name}{Environment.NewLine}
                Surname : {CurrentUser.Surname} to {_surname}{Environment.NewLine}
                State : {CurrentUser.State} to {_state}{Environment.NewLine}
                Country : {CurrentUser.Country} to {_country}{Environment.NewLine}
                Birthday : {CurrentUser.Birthday} to {_birthday}{Environment.NewLine}
                Email : {CurrentUser.Email} to {_email}{Environment.NewLine}
                Login : {CurrentUser.Login} to {_login}{Environment.NewLine}
                Description : {CurrentUser.Description} to {_description}"
            );

            CurrentUser.Name = _name;
            CurrentUser.Surname = _surname;
            CurrentUser.State = _state;
            CurrentUser.Country = _country;
            CurrentUser.Birthday = _birthday;
            CurrentUser.Email = _email;
            CurrentUser.Login = _login;
            CurrentUser.Skype = _skype;
            CurrentUser.Description = _description;
            User.Update();
            LoadData();
        }

        public void ButtonRetrieveClick()
        {
            _name = CurrentUser.Name;
            _surname = CurrentUser.Surname;
            _state = CurrentUser.State;
            _skype = CurrentUser.Skype;
            _country = CurrentUser.Country;
            _birthday = CurrentUser.Birthday;
            _email = CurrentUser.Email;
            _login = CurrentUser.Login;
            _description = CurrentUser.Description;
            LoadData();
        }

        public void LoadData()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Surname");
            OnPropertyChanged("State");
            OnPropertyChanged("Skype");
            OnPropertyChanged("Country");
            OnPropertyChanged("Birthday");
            OnPropertyChanged("Email");
            OnPropertyChanged("Login");
            OnPropertyChanged("Description");
            _lastScreen.LoadData();
            ButtonsActive = false;
        }

        private string _checkNameTemplate(string fieldName, string value)
        {
            if (value == string.Empty)
                return $"{fieldName} can't be empty";
            if (value[0] == ' ')
                return $"{fieldName} can't start off space";
            if (value.Length < 2)
                return $"{fieldName} must contains 2 or more letters";
            if (Name.ToUpper()[0] != Name[0])
                return $"{fieldName} must start from upper letter";
            foreach (var item in value)
            {
                if (!(item >= 'A' && item <= 'Z') && !(item >= 'a' && item <= 'z') && !(item >= 'А' && item <= 'Я') && !(item >= 'а' && item <= 'я'))
                {
                    return $"{fieldName} can contain letter only";
                }
            }

            return string.Empty;
        }
    }
}