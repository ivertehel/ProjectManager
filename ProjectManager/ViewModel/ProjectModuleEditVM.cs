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
        private ObservableCollection<User_ProjectVM> _employeesCollection = new ObservableCollection<User_ProjectVM>();

        private ProjectVM _projectVM = new ProjectVM(new Project());

        private UserVM _selectedLeader;
        private Project.Statuses _status;
        private bool _saveButton = false;
        private ObservableCollection<User_ProjectVM> _savedPositions = new ObservableCollection<User_ProjectVM>();
        private ObservableCollection<TeamVM> _teamsCollection = new ObservableCollection<TeamVM>();
        private ObservableCollection<UserVM> _leadersCollection = new ObservableCollection<UserVM>();

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
            foreach (var item in User_Project.Items)
            {
                if (item.Project.Id == ProjectVM.Project.Id)
                    SavedPositions.Add(new User_ProjectVM(item));
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
                if (ProjectVM.Project == null)
                    return skills;

                foreach (var item in Project_Skill.Items.Where(skill => skill.Project.Id == ProjectVM.Project.Id).ToList())
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
                foreach (var item in _savedPositions)
                {
                    if (_leadersCollection.FirstOrDefault(leader => leader.User.Id == item.User.Id) == null)
                        _leadersCollection.Add(new UserVM(item.User));
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

        public ObservableCollection<User_ProjectVM> EmployeesCollection
        {
            get
            {
                return _employeesCollection;
            }
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

        public ObservableCollection<UserVM> Employees
        {
            get
            {
                ObservableCollection<UserVM> employees = new ObservableCollection<UserVM>();
                foreach (var item in SavedPositions)
                {
                    if (employees.FirstOrDefault(emp => emp.User.Id == item.User.Id) == null)
                        employees.Add(new UserVM(item.User));
                }

                return employees;
            }
        }

        public ObservableCollection<User_ProjectVM> SavedPositions
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
            if (ProjectVM.Project == null)
            {
                ProjectVM.Project = new Project();
                ProjectVM.Project.Name = Name;
                ProjectVM.Project.Description = Description;
                ProjectVM.Project.StartDate = StartDate;
                ProjectVM.Project.ReleaseDate = ReleaseDate;
                ProjectVM.Project.Order = CurrentOrder.Order;
                ProjectVM.Project.Status = Status;
                if (SelectedLeader != null)
                    ProjectVM.Project.Leader = SelectedLeader.User;
                Project.Items.Add(ProjectVM.Project);
                Project_Project p = new Project_Project()
                {
                    ParrentProject = null,
                    ChildProject = ProjectVM.Project
                };
                Project_Project.Items.Add(p);

                foreach (var item in _savedPositions)
                {
                    User_Project user_project = new User_Project()
                    {
                        Position = item.Position,
                        Project = ProjectVM.Project,
                        User = item.User
                    };

                    User_Project.Items.Add(user_project);
                }



            }
            else
            {
                Guid id = ProjectVM.Project.Id;
                User_Project.Items.RemoveAll(item => item.Project.Id == id);
                Project_Project.Items.Remove(Project_Project.Items.FirstOrDefault(item => item.ChildProject.Id == ProjectVM.Project.Id));
                Project_Skill.Items.RemoveAll(item => item.Project.Id == id);
                ProjectVM.Project.Id = id;
                ProjectVM.Project.Name = Name;
                ProjectVM.Project.Description = Description;
                if (SelectedLeader != null)
                    ProjectVM.Project.Leader = SelectedLeader.User;

                ProjectVM.Project.StartDate = StartDate;
                ProjectVM.Project.ReleaseDate = ReleaseDate;
                ProjectVM.Project.Order = CurrentOrder.Order;
                ProjectVM.Project.Status = Status;

                User_Project.Items.RemoveAll(item => item.Project.Id == id);
                foreach (var item in _savedPositions)
                {
                    User_Project user_project = new User_Project()
                    {
                        Position = item.Position,
                        Project = ProjectVM.Project,
                        User = item.User
                    };

                    User_Project.Items.Add(user_project);
                }

                Project.Items.Add(ProjectVM.Project);
                Project_Project.Items.Add(new Project_Project() { ChildProject = ProjectVM.Project });
                

            }

            foreach (var item in skills)
            {
                Project_Skill project_skill = new Project_Skill()
                {
                    Project = ProjectVM.Project,
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
            OnPropertyChanged("Employees");
            var l = (_lastScreen as ILoadDataSender);
            if (l != null)
                l.LoadData(sender);
        }
    }
}
