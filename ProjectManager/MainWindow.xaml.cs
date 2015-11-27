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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var newUser = new User()
            {
                Name = "Alex",
                Birthday = DateTime.Now,
                Country = "Ukraine",
                Email = "alex@ex.ua",
                Login = "alex",
                Password = "qwerty",
                Role = "Client",
                Skype = "alex123",
                Surname = "Alexeev"
            };
            Client c = new Client() { Account = 100, User = newUser };
            User.Items.Add(newUser);
            Client.Items.Add(c);
            Team t = new Team() { Name = "Shark", Description = ".NET team" };
            Task t1 = new Task();
            t1.Owner = Task.Owners.Team;
        }
    }
}
