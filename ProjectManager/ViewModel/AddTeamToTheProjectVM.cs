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
    class AddTeamToTheProjectVM : INotifyPropertyChanged, ILoadDataSender
    {
        private AddTeamToTheProject _addTeamToTheProject;
        private ILoadDataSender _lastScreen;
        private ProjectModuleEditVM _projectModuleEditVM;
        private List<string> _selectedSkills = new List<string>();
        private ObservableCollection<TeamVM> _teamsCollection = new ObservableCollection<TeamVM>();
        private string _name;
        private ObservableCollection<TeamVM> _teamsToAddCollection = new ObservableCollection<TeamVM>();
        private bool _addButton;
        private bool _detailsButton;
        private bool _saveButton;
        private bool _removeButton;

        public AddTeamToTheProjectVM(ILoadDataSender lastScreen, ProjectModuleEditVM projectModuleEditVM, AddTeamToTheProject addTeamToTheProject)
        {
            _lastScreen = lastScreen;
            _projectModuleEditVM = projectModuleEditVM;
            _addTeamToTheProject = addTeamToTheProject;
            AddButton = true;
            foreach (var item in projectModuleEditVM.TeamsCollection)
                _teamsToAddCollection.Add(item);
        }

        public List<string> SelectedSkills
        {
            get { return _selectedSkills; }
            set { _selectedSkills = value; }
        }

        public ObservableCollection<TeamVM> TeamsCollection
        {
            get
            {
                _teamsCollection.Clear();
                foreach (var item in Team.Items)
                    _teamsCollection.Add(new TeamVM(item));

                filterTeamsCollection();
                return _teamsCollection;
            }
        }

        public ObservableCollection<TeamVM> TeamsToAddCollection
        {
            get
            {
                return _teamsToAddCollection;
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool AddButton
        {
            get { return _addButton; }
            set
            {
                _addButton = value;
                OnPropertyChanged("AddButton");
            }
        }
        public bool DetailsButton
        {
            get { return _detailsButton; }
            set
            {
                _detailsButton = value;
                OnPropertyChanged("DetailsButton");
            }
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

        public bool RemoveButton
        {
            get { return _removeButton; }
            set
            {
                _removeButton = value;
                OnPropertyChanged("RemoveButton");
            }
        }

        public void RemoveButtonClick(TeamVM team)
        {
            if (_teamsToAddCollection.Where(item => item.Team.Id == team.Team.Id ).Count() != 0)
            {
                var toDelete = _teamsToAddCollection.First(item => item.Team.Id == team.Team.Id);
                _teamsToAddCollection.Remove(toDelete);
                RemoveButton = false;
                DetailsButton = false;
                SaveButton = true;
                OnPropertyChanged("RemoveButton");
                OnPropertyChanged("DetailsButton");
                OnPropertyChanged("SaveButton");
                LoadData(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SaveButton_Click()
        {
            _projectModuleEditVM.TeamsCollection.Clear();
            foreach (var item in TeamsToAddCollection)
            {
                _projectModuleEditVM.TeamsCollection.Add(item);
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void filterTeamsCollection()
        {
            var teams = _teamsCollection.ToList();
            if (!string.IsNullOrEmpty(Name))
                teams.RemoveAll(item => !item.Name.ToUpper().StartsWith(Name.ToUpper()));

            if (_selectedSkills.Count != 0)
            {
                List<string> skillNames = new List<string>();

                foreach (var item in _selectedSkills)
                {
                    teams.RemoveAll(team => team.Skills.Where(skill => skill.Name == item).FirstOrDefault() == null);
                }
            }

            _teamsCollection.Clear();
            foreach (var item in teams)
            {
                _teamsCollection.Add(item);
            }
        }

        public void ActivateButtons(TeamVM selectedTeamToAdd)
        {
            if (selectedTeamToAdd != null)
            {
                if (_teamsToAddCollection.Where(item => item.Name == selectedTeamToAdd.Name).Count() > 0)
                {
                    RemoveButton = true;
                    AddButton = false;
                }
                else
                {
                    RemoveButton = false;
                    AddButton = true;
                }

                DetailsButton = true;
                OnPropertyChanged("RemoveButton");
                OnPropertyChanged("AddButton");
                OnPropertyChanged("DetailsButton");
            }
        }

        public void AddButtonClick(TeamVM team)
        {
            if (_teamsToAddCollection.Where(item => item.Equals(team)).Count() == 0)
            {
                _teamsToAddCollection.Add(team);
                AddButton = false;
                DetailsButton = false;
                SaveButton = true;
                OnPropertyChanged("AddButton");
                OnPropertyChanged("DetailsButton");
                OnPropertyChanged("SaveButton");
                LoadData(this);
            }
            else
            {
                throw new Exception("This team is already exist");
            }
        }

        public void LoadData(object sender)
        {
            OnPropertyChanged("TeamsToAddCollection");
            OnPropertyChanged("TeamsCollection");
        }

    }
}
