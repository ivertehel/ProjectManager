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
    public class UserDetailsVM : INotifyPropertyChanged
    {
        private UserVM _currentEmployee;

        private ObservableCollection<User.States> states = new ObservableCollection<User.States>();

        public UserDetailsVM(UserVM user)
        {
            if (user == null)
                return;
            CurrentEmployee = user;
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public UserVM CurrentEmployee
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value; }
        }

        public string Name
        {
            get { return CurrentEmployee.Name; }
            set { CurrentEmployee.Name = value; }
        }

        public string Surname
        {
            get { return CurrentEmployee.Surname; }
            set { CurrentEmployee.Surname = value; }
        }

        public User.States State
        {
            get { return CurrentEmployee.State; }
            set { CurrentEmployee.State = value; }
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
            get { return CurrentEmployee.Country; }
            set { CurrentEmployee.Country = value; }
        }

        public DateTime Birthday
        {
            get { return CurrentEmployee.Birthday; }
            set { CurrentEmployee.Birthday = value; }
        }

        public string Email
        {
            get { return CurrentEmployee.Email; }
            set { CurrentEmployee.Email = value; }
        }

        public string Skype
        {
            get { return CurrentEmployee.Skype; }
            set { CurrentEmployee.Skype = value; }
        }

        public string Login
        {
            get { return CurrentEmployee.Login; }
            set { CurrentEmployee.Login = value; }
        }

        public string Description
        {
            get { return CurrentEmployee.Description; }
            set { CurrentEmployee.Description = value; }
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

   

        public void LoadData()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Surname");

        }

    }
}
