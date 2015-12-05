using PMDataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PMView.View
{
    public class TeamDetailsVM : INotifyPropertyChanged
    {
        private ObservableCollection<User_Team> _employeesCollection = new ObservableCollection<User_Team>();

        public event PropertyChangedEventHandler PropertyChanged;

        public TeamDetailsVM(Team team)
        {
            CurrentTeam = team;
            var users = team.Users.ToList();
            for (int i=0; i<users.Count; i++)
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
                _employeesCollection.Add(users[i]);
            }

            LoadData();
        }

        public Team CurrentTeam { get; private set; }

        public string Name
        {
            get { return CurrentTeam.Name; }
        }

        public string Description
        {
            get { return CurrentTeam.Description; }
        }

        public IEnumerable<User_Team> Employees
        {
            get { return CurrentTeam.Users; }
        }

        public Team SelectedTeam { get; set; }

        public ObservableCollection<User_Team> EmployeesCollection
        {
            get { return _employeesCollection; }
        }

        private void LoadData()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            OnPropertyChanged("Employees");
        }


        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
