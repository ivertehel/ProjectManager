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
