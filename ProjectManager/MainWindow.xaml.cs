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
            ProjectDetailsGrid.Visibility = Visibility.Hidden;
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
            User e1 = new User()
            {
                Name = "Ivan",
                Surname = "Vertegel",
                Birthday = new DateTime(1994, 10, 13),
                Country = "Ukraine",
                Description = ".NET developer",
                Email = "ivanvertegel@outlook.com",
                Image = "Assets/MaleAvatar.jpg",
                Login = "vsailor",
                Password = "qwerty",
                Role = User.Roles.Employee,
                Skype = "sirius9764",
                State = User.States.Male,
                Status = User.Statuses.Ready
            };

            User e2 = new User()
            {
                Name = "Denis",
                Surname = "Pikushiy",
                Birthday = new DateTime(1995, 08, 17),
                Country = "Ukraine",
                Description = ".NET developer",
                Email = "datrax@ex.ua",
                Image = "Assets/MaleAvatar.jpg",
                Login = "datrax",
                Password = "qwerty",
                Role = User.Roles.Employee,
                Skype = "dat_rax",
                State = User.States.Male,
                Status = User.Statuses.Ready
            };

            User e3 = new User()
            {
                Name = "Krystyna",
                Surname = "Romanyshyn",
                Birthday = new DateTime(1995, 04, 12),
                Country = "Ukraine",
                Description = "QA Engineer",
                Email = "khrystyna1204@gmail.com",
                Image = "Assets/MaleAvatar.jpg",
                Login = "khrystyna1204",
                Password = "qwerty",
                Role = User.Roles.Employee,
                Skype = "khrystyna1204",
                State = User.States.Female,
                Status = User.Statuses.Ready
            };

            User.Items.Add(e1);
            User.Items.Add(e2);
            User.Items.Add(e3);


            User u1 = new User()
            {
                Name = "Olga",
                Surname = "Karpushin",
                Country = "Israel",
                Description = "Low budget",
                Image = "Assets/MaleAvatar.jpg",
                Login = "sharksoft ",
                Password = "qwerty",
                Role = User.Roles.Client,
                Skype = "sharksoft",
                State = User.States.Female,
            };
            User.Items.Add(u1);

            Client c1 = new Client()
            {
                Account = 0m,
                User = u1
            };

            Client.Items.Add(c1);

            Order o1 = new Order()
            {
                Name = "Unity 3d Eggsckatcher game",
                Description = "I need Unity 3d Eggsckatcher game like this: https://play.google.com/store/apps/details?id=com.nomoc.wolfonfarmI need Unity 3d Eggsckatcher game like this: https://play.google.com/store/apps/details?id=com.nomoc.wolfonfarmI need Unity 3d Eggsckatcher game like this: https://play.google.com/store/apps/details?id=com.nomoc.wolfonfarmI need Unity 3d Eggsckatcher game like this: https://play.google.com/store/apps/details?id=com.nomoc.wolfonfarm",
                Price = 50,
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




        }

        private void ProjectsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ProjectDetailsGrid.Visibility = Visibility.Visible;
            fillOrderInfo();
        }
    }
}
