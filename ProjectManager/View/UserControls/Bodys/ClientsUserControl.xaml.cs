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
    /// Interaction logic for ClientsUserControl.xaml
    /// </summary>
    public partial class ClientsUserControl : UserControl
    {
        private ClientsUserControlVM _clientsUserControlVM;

        public ClientsUserControl()
        {
            InitializeComponent();
            _clientsUserControlVM = new ClientsUserControlVM();
            DataContext = _clientsUserControlVM;
        }

        private void ClientsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditProject_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RemoveProject_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
