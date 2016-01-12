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
    class AddTeamToTheProjectVM : INotifyPropertyChanged
    {
        private AddTeamToTheProject _addTeamToTheProject;
        private ILoadDataSender _lastScreen;
        private ProjectModuleEditVM _projectModuleEditVM;
        private List<string> _selectedSkills = new List<string>();
        private ObservableCollection<TeamVM> _teamsCollection = new ObservableCollection<TeamVM>();
        private string _name;

        public AddTeamToTheProjectVM(ILoadDataSender lastScreen, ProjectModuleEditVM projectModuleEditVM, AddTeamToTheProject addTeamToTheProject)
        {
            _lastScreen = lastScreen;
            _projectModuleEditVM = projectModuleEditVM;
            _addTeamToTheProject = addTeamToTheProject;
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

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

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
    }
}
