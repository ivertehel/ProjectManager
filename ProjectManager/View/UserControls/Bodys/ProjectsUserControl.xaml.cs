using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PMViewModel;

namespace PMView.View
{
    /// <summary>
    /// Interaction logic for ProjectsUserControl.xaml
    /// </summary>
    public partial class ProjectsUserControl : UserControl
    {
        private ProjectsUserControlVM _projectsUserControlVM;

        public ProjectsUserControl()
        {
            InitializeComponent();
            _projectsUserControlVM = new ProjectsUserControlVM();
            DataContext = _projectsUserControlVM;
            ProjectsDataGrid.ItemsSource = from items in _projectsUserControlVM.OrdersCollection select new { Name = items.Name, Description = items.Description, StartDate = items.StartDate.ToShortDateString(), ReleaseDate = items.ReleaseDate.ToShortDateString(), Price = items.Price, Status = items.Status };
        }

        private void ProjectsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _projectsUserControlVM.LoadData(((DataGrid)sender).SelectedIndex);
        }
    }
}
