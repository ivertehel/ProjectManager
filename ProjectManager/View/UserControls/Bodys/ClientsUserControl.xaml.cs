using PMDataLayer;
using PMView.View.WrapperVM;
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
            User.Update();
            Client.Update();
            InitializeComponent();
            _clientsUserControlVM = new ClientsUserControlVM();
            DataContext = _clientsUserControlVM;
        }

        private void ClientsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            (new UsersDetails(new UserVM(new PMDataLayer.User()) as IUser, _clientsUserControlVM)).Show();
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RemoveClient_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
