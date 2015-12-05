using PMDataLayer;
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
            ProjectsDataGrid.ItemsSource = _projectsUserControlVM.OrdersCollection;
            SubProjectsDataGrid.ItemsSource = _projectsUserControlVM.ProjectsCollection;
            EmployeesListBox.ItemsSource = _projectsUserControlVM.EmployeesCollection;
            TeamsListBox.ItemsSource = _projectsUserControlVM.TeamsCollection;
            TasksDataGrid.ItemsSource = _projectsUserControlVM.TasksCollection;
        }

        private void ProjectsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _projectsUserControlVM.LoadData((Order)((DataGrid)sender).SelectedItem);
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Task");
        }

        private void TaskButton_MouseEnter(object sender, MouseEventArgs e)
        {
            TaskButton.Opacity = 1;
        }

        private void TaskButton_MouseLeave(object sender, MouseEventArgs e)
        {
            TaskButton.Opacity = 0.8;
        }

        private void TeamsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamsListBox.SelectedItem != null)
            (new TeamsDetails(TeamsListBox.SelectedItem as Team)).Show();

            TeamsListBox.SelectedItem = null;
        }
    }
}
