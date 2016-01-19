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
using PMDataLayer;
using PMView.View.WrapperVM;
using Core;

namespace PMView.View
{
    /// <summary>
    /// Interaction logic for ProjectsUserControl.xaml
    /// </summary>
    public partial class ProjectsUserControl : UserControl
    {
        private ProjectsUserControlVM _projectsUserControlVM = new ProjectsUserControlVM();

        public ProjectsUserControl()
        {
            InitializeComponent();
            DataContext = _projectsUserControlVM;
        }

        public ProjectsUserControl(Order order)
        {
            InitializeComponent();
            _projectsUserControlVM = new ProjectsUserControlVM(order);
            DataContext = _projectsUserControlVM;
            ProjectsDataGrid.DataContext = null;
            ProjectsDataGrid.Visibility = Visibility.Hidden;
        }

        private void ProjectsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _projectsUserControlVM.SelectedOrder = ProjectsDataGrid.SelectedItem as OrderVM;
            _projectsUserControlVM.LoadData();
        }

        private void TeamsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamsListBox.SelectedItem != null)
                (new TeamsDetails(TeamsListBox.SelectedItem as TeamVM, _projectsUserControlVM)).Show();

            TeamsListBox.SelectedItem = null;
        }

        private void CustomerProfileButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (new UsersDetails(new UserVM(_projectsUserControlVM.SelectedOrder.Client.User) as IUser, _projectsUserControlVM)).Show();
        }

        private void CustomerProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CustomerProfileButton.Opacity = 1;
        }

        private void CustomerProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CustomerProfileButton.Opacity = 0.8;
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            (new ProjectModuleEdit(_projectsUserControlVM, _projectsUserControlVM.SelectedOrder)).Show();
        }

        private void EditProject_Click(object sender, RoutedEventArgs e)
        {
            if (ModulesDataGrid.SelectedItem != null)
                (new ProjectModuleEdit(_projectsUserControlVM, _projectsUserControlVM.SelectedOrder, ModulesDataGrid.SelectedItem as ProjectVM, null)).Show();
        }

        private void RemoveProject_Click(object sender, RoutedEventArgs e)
        {
            if (ModulesDataGrid.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure?", "Are you sure?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                {
                    _projectsUserControlVM.RemoveProject(ModulesDataGrid.SelectedItem as ProjectVM);
                }
            }
        }

        private void EmployeesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesListBox.SelectedItem != null)
                (new UsersDetails(EmployeesListBox.SelectedItem as IEmployee, _projectsUserControlVM)).Show();

            EmployeesListBox.SelectedItem = null;
        }
    }
}
