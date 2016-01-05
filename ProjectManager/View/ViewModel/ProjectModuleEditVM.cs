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

        private List<string> _statuses = new List<string>();

        private ProjectsUserControlVM _projectsUserControlVM;
        private ObservableCollection<UserVM> _employeesCollection = new ObservableCollection<UserVM>();

        private ProjectVM _projectVM = new ProjectVM(new Project());

        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectModuleEditVM(ILoadData lastScreen, ProjectsUserControlVM projectsUserControlVM)
        {
            _projectVM.Order = projectsUserControlVM.SelectedOrder.Order;
            _projectsUserControlVM = projectsUserControlVM;
            _lastScreen = lastScreen;
            _currentOrder = projectsUserControlVM.SelectedOrder;
            _projectVM.StartDate = DateTime.Now;
            _projectVM.ReleaseDate = DateTime.Now.AddDays(31);
            _statuses.Add(Project.Statuses.Discarded.ToString());
            _statuses.Add(Project.Statuses.Done.ToString());
            _statuses.Add(Project.Statuses.InProgress.ToString());
            _statuses.Add(Project.Statuses.Opened.ToString());
        }

        public ProjectsUserControlVM ProjectUserControlVM
        {
            get { return _projectsUserControlVM; }
        }

        public ProjectVM ProjectVM
        {
            get { return _projectVM; }
        }

        public List<string> Statuses
        {
            get
            {
                return _statuses;
            }
        }

        public ObservableCollection<UserVM> EmployeesCollection
        {
            get { return _employeesCollection; }
            set { _employeesCollection = value; }
        }

        public OrderVM CurrentOrder
        {
            get { return _currentOrder; }
            set { _currentOrder = value; }
        }

        public string Name
        {
            get { return _projectVM.Name; }
            set { _projectVM.Name = value; }
        }

        public DateTime StartDate
        {
            get { return _projectVM.StartDate; }
            set { _projectVM.StartDate = value; }
        }

        public DateTime ReleaseDate
        {
            get { return _projectVM.ReleaseDate; }
            set { _projectVM.ReleaseDate = value; }
        }

        public void LoadData()
        {
            _lastScreen.LoadData();
            OnPropertyChanged("EmployeesCollection");
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
