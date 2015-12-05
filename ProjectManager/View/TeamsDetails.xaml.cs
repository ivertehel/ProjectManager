using PMDataLayer;
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
using System.Windows.Shapes;
using PMView.View;

namespace PMView
{
    /// <summary>
    /// Interaction logic for TeamsDetails.xaml
    /// </summary>
    public partial class TeamsDetails : Window
    {
        TeamDetailsVM _teamDetailsVM;
        public TeamsDetails(Team team)
        {
            InitializeComponent();
            _teamDetailsVM = new TeamDetailsVM(team);
            DataContext = _teamDetailsVM;
            EmployeesListBox.ItemsSource = _teamDetailsVM.EmployeesCollection;
        }
    }
}
