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

        public ProjectModuleEditVM(ILoadData lastScreen, ProjectsUserControlVM projectsUserControlVM)
        {
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
            get
            {
                return _projectsUserControlVM.EmployeesCollection;
            }
        }

        public OrderVM CurrentOrder
        {
            get { return _currentOrder; }
            set { _currentOrder = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set { _releaseDate = value; }
        }

        public void LoadData()
        {
            _lastScreen.LoadData();
        }
    }
}
