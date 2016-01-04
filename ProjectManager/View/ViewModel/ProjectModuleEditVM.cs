using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMView.View.WrapperVM;
using PMDataLayer;
using System.Collections.ObjectModel;

namespace PMView.View
{
    public class ProjectModuleEditVM : ILoadData
    {
        private string _name;

        private DateTime _startDate;

        private DateTime _releaseDate;

        private OrderVM _currentOrder;

        private ILoadData _lastScreen;

        private List<string> _statuses = new List<string>();

        private ProjectsUserControlVM _projectsUserControlVM;
        private ObservableCollection<UserVM> _employeesCollection = new ObservableCollection<UserVM>();

        private ProjectVM _projectVM = new ProjectVM(new Project());

        public ProjectsUserControlVM ProjectUserControlVM
        {
            get { return _projectsUserControlVM; }
        }

        public ProjectVM ProjectVM
        {
            get { return _projectVM; }
        }

        public ProjectModuleEditVM(ILoadData lastScreen, ProjectsUserControlVM projectsUserControlVM)
        {
            _projectVM.Order = projectsUserControlVM.SelectedOrder.Order;
            _projectsUserControlVM = projectsUserControlVM;
            _lastScreen = lastScreen;
            _currentOrder = projectsUserControlVM.SelectedOrder;
            _startDate = DateTime.Now;
            _releaseDate = DateTime.Now.AddDays(31);
            _statuses.Add(Project.Statuses.Discarded.ToString());
            _statuses.Add(Project.Statuses.Done.ToString());
            _statuses.Add(Project.Statuses.InProgress.ToString());
            _statuses.Add(Project.Statuses.Opened.ToString());
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
        }
    }
}
