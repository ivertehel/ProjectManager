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

        private ObservableCollection<User_TeamVM> _employeesCollection = new ObservableCollection<User_TeamVM>();

        public ProjectModuleEditVM(ILoadData lastScreen)
        {
            _lastScreen = lastScreen;
            _currentOrder = (lastScreen as ProjectsUserControlVM).SelectedOrder;
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

        public void AddEmployeesToTheModuleButton()
        {
            (new AddEmployeeToTheProject(this)).Show();
        }

        ////public ObservableCollection<User_TeamVM> EmployeesCollection
        ////{
        ////    get
        ////    {
        ////        _employeesCollection.Clear();

        ////    }
        ////}

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
