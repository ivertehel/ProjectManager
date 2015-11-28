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

namespace ProjectManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitDataLayer();
        }

        private void InitDataLayer()
        {
            Order o1 = new Order()
            {
                Name = "Skype",
                Description = "Make a program like skype",
                Price = 1000,
                ReleaseDate = new DateTime(2015, 12, 31),
                StartDate = new DateTime(2015, 11, 29),
                Status = Order.Statuses.Open,
                IsPrivate = false
            };
            Order.Items.Add(o1);

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
            ProjectsDataGrid.ItemsSource = from items in Order.Items
                                           select new { ProjectName = items.Name, Description = items.Description, Price = items.Price, StartDate = items.StartDate, ReleaseDate = items.ReleaseDate, Status = items.Status};
            
        }
    }
}
