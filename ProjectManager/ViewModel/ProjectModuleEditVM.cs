using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PMView.View.WrapperVM;
using PMDataLayer;
using System.ComponentModel;

namespace PMView.View
{
    public class ProjectModuleEditVM : INotifyPropertyChanged, ILoadDataSender
    {
        private OrderVM _currentOrder;

        private ILoadDataSender _lastScreen;

        private List<Project.Statuses> _statuses = new List<Project.Statuses>();

        private ProjectsUserControlVM _projectsUserControlVM;
        private ObservableCollection<UserVM> _employeesCollection = new ObservableCollection<UserVM>();

        private ProjectVM _projectVM = new ProjectVM(new Project());

        private ObservableCollection<UserVM> _leadersCollection = new ObservableCollection<UserVM>();
        private UserVM _selectedLeader;
        private Project.Statuses _status;
        private Project _project;
        private bool _saveButton = false;
        private List<User_ProjectVM> _savedPositions = new List<User_ProjectVM>();
        private ObservableCollection<TeamVM> _teamsCollection = new ObservableCollection<TeamVM>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectModuleEditVM(ILoadDataSender lastScreen, ProjectsUserControlVM projectsUserControlVM)
        {
            _projectVM.Order = projectsUserControlVM.SelectedOrder.Order;
            _projectsUserControlVM = projectsUserControlVM;
            _lastScreen = lastScreen;
            _currentOrder = projectsUserControlVM.SelectedOrder;
            _projectVM.StartDate = DateTime.Now;
            _projectVM.ReleaseDate = DateTime.Now.AddDays(31);

            foreach (Project.Statuses status in Enum.GetValues(typeof(Project.Statuses)))
            {
                _statuses.Add(status);
            }

            _status = _statuses.Where(item => item == Project.Statuses.Opened).FirstOrDefault();

        }

        public ProjectModuleEditVM(ILoadDataSender lastScreen, ProjectsUserControlVM projectsUserControlVM, ProjectVM projectVM) : this(lastScreen, projectsUserControlVM)
        {
            _projectVM = projectVM;

            _project = ProjectVM.Project;

            foreach (var item in User_Project.Items)
            {
                if (_employeesCollection.FirstOrDefault(employee => employee.User.Id == item.User.Id) == null && item.Project.Id == _projectVM.Project.Id)
                {
                    _employeesCollection.Add(new UserVM(item.User));
                }

                if (item.Project.Id == _project.Id)
                    _savedPositions.Add(new User_ProjectVM(item));
            }

            foreach (var item in Team_Project.Items)
            {
                if (_teamsCollection.FirstOrDefault(team => team.Team.Id == item.Team.Id) == null && item.Project.Id == _projectVM.Project.Id)
                {
                    _teamsCollection.Add(new TeamVM(item.Team));
                }
            }

            _status = projectVM.Status;
            LoadData();
        }

        public Project.Statuses Status
        {
            get { return _status; }
            set
            {
                _status = value;
                SaveButton = true;
            }
        }

        public List<string> Skills
        {
            get
            {
                List<string> skills = new List<string>();
                if (_project == null)
                    return skills;

                foreach (var item in Project_Skill.Items.Where(skill => skill.Project.Id == _project.Id).ToList())
                {
                    skills.Add(item.Skill.Name);
                }

                return skills;
            }
        }

        public UserVM SelectedLeader
        {
            get
            {
                return _selectedLeader;
            }
            set
            {
                _selectedLeader = value;
                SaveButton = true;
            }
        }

        public ObservableCollection<UserVM> LeadersCollection
        {
            get
            {
                _leadersCollection.Clear();
                foreach (var item in EmployeesCollection)
                {
                    if (_leadersCollection.FirstOrDefault(leader => leader.User.Id == item.User.Id) == null)
                        _leadersCollection.Add(item);
                }

                return _leadersCollection;
            }
        }

        public ProjectsUserControlVM ProjectUserControlVM
        {
            get { return _projectsUserControlVM; }
        }

        public ProjectVM ProjectVM
        {
            get { return _projectVM; }
        }

        public List<Project.Statuses> Statuses
        {
            get
            {
                return _statuses;
            }
        }

        public ObservableCollection<UserVM> EmployeesCollection
        {
            get { return _employeesCollection; }
            set
            {
                _employeesCollection = value;
                SaveButton = true;
            }
        }

        public ObservableCollection<TeamVM> TeamsCollection
        {
            get { return _teamsCollection; }
            set
            {
                _teamsCollection = value;
                SaveButton = true;
            }
        }

        public OrderVM CurrentOrder
        {
            get { return _currentOrder; }
            set { _currentOrder = value; }
        }

        public string Name
        {
            get { return _projectVM.Name; }
            set
            {
                _projectVM.Name = value;
                SaveButton = true;
            }
        }

        public string Description
        {
            get { return _projectVM.Description; }
            set
            {
                _projectVM.Description = value;
                SaveButton = true;
            }
        }

        public DateTime StartDate
        {
            get { return _projectVM.StartDate; }
            set
            {
                _projectVM.StartDate = value;
                _projectVM.ReleaseDate = value;
                OnPropertyChanged("ReleaseDate");
                SaveButton = true;
            }
        }

        public DateTime ReleaseDate
        {
            get { return _projectVM.ReleaseDate; }
            set
            {
                _projectVM.ReleaseDate = value;
                SaveButton = true;
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

        public List<User_ProjectVM> SavedPositions
        {
            get { return _savedPositions; }
            set { _savedPositions = value; }
        }

        public void AddProject(string[] skills)
        {
            if (string.IsNullOrEmpty(Name) || Name[0] == ' ')
                throw new Exception("Name can't be empty or start from space");
            if (CurrentOrder == null)
                throw new Exception("Order was deleted");
            if (ReleaseDate < StartDate)
                throw new Exception("Release date can't be before stat date");
            if (Status == Project.Statuses.InProgress && SelectedLeader == null)
            {
                throw new Exception("Module or project must contain leader");
            }
            if (_project == null)
            {
                _project = new Project();
                _project.Name = Name;
                _project.Description = Description;
                _project.StartDate = StartDate;
                _project.ReleaseDate = ReleaseDate;
                _project.Order = CurrentOrder.Order;
                _project.Status = Status;
                if (SelectedLeader != null)
                    _project.Leader = SelectedLeader.User;
                Project.Items.Add(_project);
                Project_Project p = new Project_Project()
                {
                    ParrentProject = null,
                    ChildProject = _project
                };
                Project_Project.Items.Add(p);

                foreach (var item in _savedPositions)
                {
                    User_Project user_project = new User_Project()
                    {
                        Position = item.Position,
                        Project = _project,
                        User = item.User
                    };

                    User_Project.Items.Add(user_project);
                }



            }
            else
            {
                Guid id = _project.Id;
                User_Project.Items.RemoveAll(item => item.Project.Id == id);
                Project_Project.Items.Remove(Project_Project.Items.FirstOrDefault(item => item.ChildProject.Id == _project.Id));
                Project_Skill.Items.RemoveAll(item => item.Project.Id == id);
                Project.Items.Remove(Project.Items.FirstOrDefault(item => item.Id == _project.Id));
                _project = new Project();
                _project.Id = id;
                _project.Name = Name;
                _project.Description = Description;
                if (SelectedLeader != null)
                    _project.Leader = SelectedLeader.User;

                _project.StartDate = StartDate;
                _project.ReleaseDate = ReleaseDate;
                _project.Order = CurrentOrder.Order;
                _project.Status = Status;

                User_Project.Items.RemoveAll(item => item.Project.Id == id);
                foreach (var item in _savedPositions)
                {
                    User_Project user_project = new User_Project()
                    {
                        Position = item.Position,
                        Project = _project,
                        User = item.User
                    };

                    User_Project.Items.Add(user_project);
                }

                Project.Items.Add(_project);
                Project_Project.Items.Add(new Project_Project() { ChildProject = _project });
                

            }

            foreach (var item in skills)
            {
                Project_Skill project_skill = new Project_Skill()
                {
                    Project = _project,
                    Skill = Skill.Items.FirstOrDefault(skill => skill.Name == item)
                };

                Project_Skill.Items.Add(project_skill);
            }



            LoadData();
            SaveButton = true;
        }

        public void LoadData()
        {
            OnPropertyChanged("EmployeesCollection");
            OnPropertyChanged("LeadersCollection");
            OnPropertyChanged("SelectedLeader");
            OnPropertyChanged("SavedPositions");
            OnPropertyChanged("TeamsCollection");
            _lastScreen.LoadData(this);
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void LoadData(object sender)
        {
            OnPropertyChanged("EmployeesCollection");
            OnPropertyChanged("LeadersCollection");
            OnPropertyChanged("SelectedLeader");
            OnPropertyChanged("SavedPositions");
            var l = (_lastScreen as ILoadDataSender);
            if (l != null)
                l.LoadData(sender);
        }
    }
}
