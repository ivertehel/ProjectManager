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
        public ClientsUserControl()
        {
            InitializeComponent();
            ////ClientsDataGrid.ItemsSource = from items in _projectsUserControlVM.OrdersCollection select new { Name = items.Name, Description = items.Description, StartDate = items.StartDate.ToShortDateString(), ReleaseDate = items.ReleaseDate.ToShortDateString(), Price = items.Price, Status = items.Status };
        }
    }
}
