using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private Button[] _menuBarButtons;

        public MenuBarUserControl()
        {
            InitializeComponent();
            _menuBarButtons = new[] { ProjectsButton, ClientsButton, EmployeesButton, TeamsButton, ReportsButton, MessagesButton };
            _loadProjectsUserControl();
        }

        private void loadUserControl(UIElement userControl, Button button)
        {
            Skeleton.Body.Children.Clear();
            Skeleton.Body.Children.Add(userControl);
            foreach (var item in _menuBarButtons)
            {
                if (item != button)
                item.Background = new SolidColorBrush(Colors.SkyBlue);
            }

            button.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xAC, 0xF0, 0xFF));
        }

        private void ProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            _loadProjectsUserControl();
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new ClientsUserControl(), ClientsButton);
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new EmployeesUserControl(), EmployeesButton);
        }

        private void TeamsButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new TeamsUserControl(), TeamsButton);
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new ReportsUserControl(), ReportsButton);
        }

        private void MessagesButton_Click(object sender, RoutedEventArgs e)
        {
            loadUserControl(new MessagesUserControl(), MessagesButton);
        }

        private void _loadProjectsUserControl()
        {
            try
            {
                var path = Environment.CurrentDirectory + @"\ProjectsScreenPlugin.dll";
                var DLL = Assembly.LoadFile(path);
                var classType = DLL.GetType("ProjectsScreenPlugin.ProjectsUserControl");
                dynamic c = Activator.CreateInstance(classType);
                loadUserControl(c as UIElement, ProjectsButton);
            }
            catch
            {
                loadUserControl(new ProjectsUserControl(), ProjectsButton);
            }
        }
    }
}
