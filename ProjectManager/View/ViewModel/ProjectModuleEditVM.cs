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
    public class ProjectModuleEditVM : ILoadData, INotifyPropertyChanged
    {
        private OrderVM _currentOrder;

        private ILoadData _lastScreen;

        private List<Project.Statuses> _statuses = new List<Project.Statuses>();

        private ProjectsUserControlVM _projectsUserControlVM;
        private ObservableCollection<UserVM> _employeesCollection = new ObservableCollection<UserVM>();

        private ProjectVM _projectVM = new ProjectVM(new Project());
        private ObservableCollection<UserVM> _leadersCollection = new ObservableCollection<UserVM>();
        private UserVM _selectedLeader;
        private Project.Statuses _status;
        private Project _project;
        private bool _saveButton = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectModuleEditVM(ILoadData lastScreen, ProjectsUserControlVM projectsUserControlVM)
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
        }

        public ProjectModuleEditVM(ILoadData lastScreen, ProjectsUserControlVM projectsUserControlVM, ProjectVM projectVM) : this(lastScreen, projectsUserControlVM)
        {
            _projectVM = projectVM;
            

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

        public UserVM SelectedLeader
        {
            get { return _selectedLeader; }
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

        public void AddProject()
        {
            if (string.IsNullOrEmpty(Name) || Name[0] == ' ')
                throw new Exception("Name can't be empty or start from space");
            if (CurrentOrder == null)
                throw new Exception("Order was deleted");
            if (Status == Project.Statuses.InProgress && SelectedLeader == null)
            {
                throw new Exception("Module or project must contain leader");
            }
            if (_project == null)
            {
                _project = new Project();
                _project.Name = Name;
                _project.Description = Description;
                if (SelectedLeader != null)
                    _project.Leader = SelectedLeader.User;
                _project.StartDate = StartDate;
                _project.ReleaseDate = ReleaseDate;
                _project.Order = CurrentOrder.Order;
                _project.Status = Status;
                Project.Items.Add(_project);
                Project_Project p = new Project_Project()
                {
                    ParrentProject = null,
                    ChildProject = _project
                };
                Project_Project.Items.Add(p);
            }
            else
            {
                _project.Name = Name;
                _project.Description = Description;
                if (SelectedLeader != null)
                    _project.Leader = SelectedLeader.User;
                _project.StartDate = StartDate;
                _project.ReleaseDate = ReleaseDate;
                _project.Order = CurrentOrder.Order;
                _project.Status = Status;
            }
            LoadData();
            SaveButton = true;
        }

        public void LoadData()
        {
            _lastScreen.LoadData();
            OnPropertyChanged("EmployeesCollection");
            OnPropertyChanged("LeadersCollection");
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
