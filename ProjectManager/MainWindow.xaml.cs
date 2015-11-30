using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Data;

namespace ProjectManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            initDataLayer();
            LoadViewLayer(ViewLayers.Projects);

        }

        public enum ViewLayers
        {
            Projects,
            Clients,
            Employees,
            Teams,
            Reports,
            Messages
        }

        public ViewLayers LoadedLayer { get; set; }

        public void LoadViewLayer(ViewLayers layer)
        {
            if (layer == ViewLayers.Projects)
            {
                ProjectsButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xAC, 0xF0, 0xFF));
                ProjectsDataGrid.ItemsSource = from items in Order.Items select new { ProjectName = items.Name, Description = items.Description, Price = items.Price, StartDate = items.StartDate.ToShortDateString(), ReleaseDate = items.ReleaseDate.ToShortDateString(), Status = items.Status };
            }
        }

        public string GetDataGridCellValue(DataGrid dataGrid, int column)
        {
            var selectedCell = dataGrid.SelectedCells[column];
            var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
            if (cellContent is TextBlock)
            {
                return((cellContent as TextBlock).Text);
            }
            return "";
        }

        private void fillOrderInfo()
        {
            if (ProjectsDataGrid.SelectedItem == null)
                return;
            Order order = (from items in Order.Items where (items.Name == GetDataGridCellValue(ProjectsDataGrid, 0) && items.Price == decimal.Parse(GetDataGridCellValue(ProjectsDataGrid, 2)) && items.Description == GetDataGridCellValue(ProjectsDataGrid, 1)) select items).FirstOrDefault();
            // /* && items.Price == decimal.Parse(GetDataGridCellValue(ProjectsDataGrid, 1))/* && items.Description == GetDataGridCellValue(ProjectsDataGrid, 1)*/*/

            if (order == null)
            {
                MessageBox.Show("Unknown error");
                return;
            }
            
            // Set project name
            ProjectNameDetailsGrid.Content = order.Name;

            // Set project details
            var document = new FlowDocument();
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(order.Description);
            document.Blocks.Add(paragraph);
            ProjectOtherInfoDetailsGrid.Document = document;
            
            // Set project price
            PriceDetailsGrid.Content = "Price: " + order.Price + " USD";

            // Set project start date
            StartDateDetailsGrid.Content = "Start date: " + order.StartDate.ToShortDateString();

            // Set project release date
            ReleaseDateDetailsGrid.Content = "Release date: " + order.ReleaseDate.ToShortDateString();

            // Set project status
            ProjectStatusDetailsGrid.Content = order.Status;

            CustomerNameDetailsGrid.Content = "Customer: " + order.Client.User.Name;
        }

        private void initDataLayer()
        {
            User u1 = new User()
            {
                Name = "Alex",
                Birthday = new DateTime(1993, 11, 11),
                Country = "Kiev",
                Description = ".NET developer",
                Email = "lalka@ex.ua",
                Image = "Hui",
                Login = "Alex_hui",
                Password = "qwerty",
                Role = User.Roles.Employee,
                Skype = "alex_hui",
                Surname = "Alexeevich",
                Status = ""
            };

            User.Items.Add(u1);

            User u2 = new User()
            {
                Name = "Grant",
                Birthday = new DateTime(1990, 4, 11),
                Country = "USA",
                Description = "",
                Email = "lalka@ex.ua",
                Image = "Hui",
                Login = "Grant_hui",
                Password = "qwerty",
                Role = User.Roles.Client,
                Skype = "grant_hui",
                Surname = "Black",
                Status = ""
            };

            User.Items.Add(u2);

            Client c1 = new Client()
            {
                Account = 0m,
                User = u2
            };
            Order o1 = new Order()
            {
                Name = "Skype",
                Description = "Make a program like skype",
                Price = 1000,
                ReleaseDate = new DateTime(2015, 12, 31),
                StartDate = new DateTime(2015, 11, 29),
                Status = Order.Statuses.Open,
                IsPrivate = false,
                Client = c1
            };
            Order.Items.Add(o1);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);
            Order.Items.Add(o1.Clone() as Order);

           

            Project p1 = new Project()
            {
                Name = "Make a WPF app",
                Description = "Add some grids",
                ReleaseDate = new DateTime(2015, 12, 31),
                StartDate = new DateTime(2015, 11, 29),
                Status = Project.Statuses.Open,
                Order = o1,
                Leader = u1
            };
            Project.Items.Add(p1);

        }

        private void ProjectsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            fillOrderInfo();
        }
    }
}
