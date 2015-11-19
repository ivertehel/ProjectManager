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
using ProjectManagerDataLayer;

namespace ProjectManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client c;
        public MainWindow()
        {
            InitializeComponent();
            var user = new User() { Name = "Vasua", Login = "123" };
            User.Items.Add(user);
            var client = new Client() { User = user };
            Client.Items.Add(client);
        }
    }
}
