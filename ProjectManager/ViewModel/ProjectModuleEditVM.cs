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
        private ILoadDataSender _lastScreen;

        private List<Project.Statuses> _statuses = new List<Project.Statuses>();

        private ObservableCollection<UserVM> _employeesCollection = new ObservableCollection<UserVM>();

        private ProjectVM _projectVM;

        private ObservableCollection<UserVM> _leadersCollection = new ObservableCollection<UserVM>();
        private UserVM _selectedLeader;
        private Project.Statuses _status;
        private Project _project = new Project();
        private bool _saveButton = false;
        private List<User_ProjectVM> _savedPositions = new List<User_ProjectVM>();
        private ObservableCollection<TeamVM> _teamsCollection = new ObservableCollection<TeamVM>();
        private ObservableCollection<ProjectVM> _projectsCollection = new ObservableCollection<ProjectVM>();
        private ProjectVM _parentProject;
        private bool _editButton;
        private bool _removeButton;
        private ProjectVM _selectedModule;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectModuleEditVM(ILoadDataSender lastScreen, OrderVM order, ProjectVM parentProject = null)
        { 
            _projectVM = new ProjectVM(_project);
            _parentProject = parentProject;
            _projectVM.Order = order.Order;
            _lastScreen = lastScreen;
            _projectVM.StartDate = DateTime.Now;
            _projectVM.ReleaseDate = DateTime.Now.AddDays(31);

            foreach (Project.Statuses status in Enum.GetValues(typeof(Project.Statuses)))
            {
                _statuses.Add(status);
            }

            _status = _statuses.Where(item => item == Project.Statuses.Opened).FirstOrDefault();

        }

        public ProjectModuleEditVM(ILoadDataSender lastScreen, OrderVM order, ProjectVM projectVM, ProjectVM parentProject = null) : this(lastScreen, order, parentProject)
        {
            _parentProject = parentProject;
            _projectVM = projectVM;

            _project = _projectVM.Project;

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
            _selectedLeader = new UserVM(_projectVM.Leader);
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
                _projectVM.Leader = _selectedLeader?.User;
                SaveButton = true;
                OnPropertyChanged("SelectedLeader");
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
                _selectedLeader = new UserVM(ProjectVM.Leader);

                return _leadersCollection;
            }
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

        public ObservableCollection<ProjectVM> ProjectsCollection
        {
            get
            {
                _projectsCollection.Clear();
                var projects = from items in Project_Project.Items where items.ParrentProject?.Id == ProjectVM.Project.Id && items.ChildProject.Order == ProjectVM.Order select items.ChildProject;
                foreach (var item in projects)
                {
                    if (_projectsCollection.FirstOrDefault(proj => proj.Project.Id == item.Id) == null)
                        _projectsCollection.Add(new ProjectVM(item));
                }

                return _projectsCollection;
            }
        }

        public bool EditButton
        {
            get { return _editButton; }
            set
            {
                _editButton = value;
                OnPropertyChanged("EditButton");
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


        public ProjectVM SelectedModule
        {
            get { return _selectedModule; }
            set
            {
                _selectedModule = value;
                if (_selectedModule != null)
                {
                    EditButton = true;
                    RemoveButton = true;
                }
                else
                {
                    EditButton = false;
                    RemoveButton = false;
                }
            }
        }

        public void RemoveProject(ProjectVM projectVM)
        {
            User_Project.Items.RemoveAll(item => item.Project.Id == projectVM.Project.Id);
            Project_Project.Items.Remove(Project_Project.Items.FirstOrDefault(item => item.ChildProject.Id == projectVM.Project.Id));
            Team_Project.Items.RemoveAll(item => item.Project.Id == projectVM.Project.Id);
            Project_Skill.Items.RemoveAll(item => item.Project.Id == projectVM.Project.Id);
            List<Guid> generalProjects = new List<Guid>();
            foreach (var item in Project_Project.Items)
                if (item.ParrentProject == null)
                    generalProjects.Add(item.Id);

            Project.Items.Remove(Project.Items.FirstOrDefault(item => item.Id == projectVM.Project.Id));
            var toDelete = (from item in Project_Project.Items
                            where item.ParrentProject == null
                            select item).ToList();

            foreach (var item in generalProjects)
                toDelete.RemoveAll(p => p.Id == item);

            while (toDelete.Count() > 0)
            {
                toDelete = (from item in Project_Project.Items
                            where item.ParrentProject == null
                            select item).ToList();

                foreach (var item in generalProjects)
                    toDelete.RemoveAll(p => p.Id == item);

                foreach (var item in toDelete)
                {
                    Project_Skill.Items.RemoveAll(ps => ps.Project.Id == item.ChildProject.Id);
                    Team_Project.Items.RemoveAll(tp => tp.Project.Id == item.ChildProject.Id);
                    User_Project.Items.RemoveAll(user => user.Project.Id == item.ChildProject.Id);
                    Project.Items.Remove(Project.Items.FirstOrDefault(p => p.Id == item.ChildProject.Id));
                    Project_Project.Items.RemoveAll(project => project.Id == item.Id);
                }
            }

            LoadData();
        }

        public void AddProject(string[] skills)
        {
            if (string.IsNullOrEmpty(Name) || Name[0] == ' ')
                throw new Exception("Name can't be empty or start from space");
            if (ProjectVM.Order == null)
                throw new Exception("Order was deleted");
            if (ReleaseDate < StartDate)
                throw new Exception("Release date can't be before stat date");
            ////if (Status == Project.Statuses.InProgress && SelectedLeader == null)
            ////{
            ////    throw new Exception("Module or project must contain leader");
            ////}
            if (_project == null)
            {
                _project.Name = Name;
                _project.Description = Description;
                _project.StartDate = StartDate;
                _project.ReleaseDate = ReleaseDate;
                _project.Order = ProjectVM.Order;
                _project.StatusType = Status;
                if (SelectedLeader != null)
                    _project.Leader = _selectedLeader.User;
                Project.Items.Add(_project);
                Project_Project p = new Project_Project()
                {
                    ParrentProject = _parentProject?.Project,
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


                foreach (var item in TeamsCollection)
                {
                    Team_Project.Items.Add(new Team_Project() { Project = _project, Team = item.Team });
                }

            }
            else
            {
                Guid id = _project.Id;
                User_Project.Items.RemoveAll(item => item.Project.Id == id);
                Project_Project.Items.Remove(Project_Project.Items.FirstOrDefault(item => item.ChildProject.Id == _project.Id));
                Project_Skill.Items.RemoveAll(item => item.Project.Id == id);
                Team_Project.Items.RemoveAll(item => item.Project.Id == _project.Id);
                Project.Items.Remove(Project.Items.FirstOrDefault(item => item.Id == _project.Id));
                _project = new Project();
                _project.Id = id;
                _project.Name = Name;
                _project.Description = Description;
                if (SelectedLeader != null)
                    _project.Leader = _selectedLeader.User;

                _project.StartDate = StartDate;
                _project.ReleaseDate = ReleaseDate;
                _project.Order = ProjectVM.Order;
                _project.StatusType = Status;

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
                Project_Project.Items.Add(new Project_Project() { ChildProject = _project, ParrentProject = _parentProject?.Project });


                foreach (var item in TeamsCollection)
                {
                    Team_Project.Items.Add(new Team_Project() { Project = _project, Team = item.Team });
                }
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
            OnPropertyChanged("ProjectsCollection");

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
            OnPropertyChanged("SelectedLeader");

            OnPropertyChanged("LeadersCollection");
            OnPropertyChanged("SavedPositions");
            OnPropertyChanged("ProjectsCollection");

            var l = (_lastScreen as ILoadDataSender);
            if (l != null)
                l.LoadData(sender);
        }
    }
}
