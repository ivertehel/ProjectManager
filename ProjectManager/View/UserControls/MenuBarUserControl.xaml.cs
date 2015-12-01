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
    /// Interaction logic for HeaderUserControl.xaml
    /// </summary>
    public partial class MenuBarUserControl : UserControl
    {
        public MenuBarUserControl()
        {
            InitializeComponent();
        }

        private void loadUserControl(UIElement userControl)
        {
            Skeleton.Body.Children.Clear();
            Skeleton.Body.Children.Add(userControl);
        }

        private void ProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new ProjectsUserControl());
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new ClientsUserControl());
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new EmployeesUserControl());
        }

        private void TeamsButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new TeamsUserControl());
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new ReportsUserControl());
        }

        private void MessagesButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new MessagesUserControl());
        }
    }
}
