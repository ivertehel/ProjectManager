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

        public TeamsDetails(Team team, ProjectsUserControlVM projectsUserControlVM)
        {
            InitializeComponent();
            _teamDetailsVM = new TeamDetailsVM(team, projectsUserControlVM);
            DataContext = _teamDetailsVM;
        }

        private void EmployeesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesListBox.SelectedItem == null)
                return;

            PositionsGrid.Visibility = Visibility.Visible;
            _teamDetailsVM.SelectedEmployee = (EmployeesListBox.SelectedItem as User_Team).User;
            _teamDetailsVM.LoadPositions();
        }

        private void PositionToAddListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _teamDetailsVM.AddPosition(PositionToAddListBox.SelectedItem as Position);
        }
    }
}
