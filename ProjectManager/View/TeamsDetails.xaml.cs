﻿using System;
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
using PMDataLayer;
using PMView.View;
using PMView.View.WrapperVM;

namespace PMView
{
    /// <summary>
    /// Interaction logic for TeamsDetails.xaml
    /// </summary>
    public partial class TeamsDetails : Window
    {
        private TeamDetailsVM _teamDetailsVM;

        public TeamsDetails(TeamVM team, ProjectsUserControlVM projectsUserControlVM)
        {
            InitializeComponent();
            _teamDetailsVM = new TeamDetailsVM(team.Team, projectsUserControlVM);
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
            if (PositionToAddListBox.SelectedItem == null)
                return;

            try
            {
                _teamDetailsVM.AddPosition(PositionToAddListBox.SelectedItem as Position);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PositionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionListBox.SelectedItem == null)
                return;

            try
            { 
                _teamDetailsVM.RemovePosition(PositionListBox.SelectedItem as Position);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var order = OrdersDataGrid.SelectedItem as Order;
            EmptyWindow ew = new EmptyWindow(order.Name);
            ew.Body.Children.Add(new ProjectsUserControl(order));
            ew.Show();
        }
    }
}