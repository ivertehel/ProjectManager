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
using PMView.View;
using PMView.View.WrapperVM;
using Core;

namespace PMView
{
    /// <summary>
    /// Interaction logic for UsersDetails.xaml
    /// </summary>
    public partial class UsersDetails : Window
    {
        private IUser _user;

        private UserDetailsVM _userDetailsVM;

        private EmployeeDetailsUserControlVM _employeeDetailsVM;

        private EmployeeDetailsUserControl _employeeDetailsUserControl;

        public UsersDetails(IUser user, ILoadData lastScreen)
        {
            InitializeComponent();
            _user = user;
            _userDetailsVM = new UserDetailsVM(user as UserVM, lastScreen);
            DataContext = _userDetailsVM;
        }

        public UsersDetails(IEmployee employee, ILoadData lastScreen) : this(employee as IUser, lastScreen)
        {
            InitializeComponent();
            Form.MinHeight = 650;
            Form.MinWidth = 800;
            Title.Text = "Employee properties";
            _employeeDetailsVM = new EmployeeDetailsUserControlVM(employee, lastScreen);
            _employeeDetailsUserControl = new EmployeeDetailsUserControl(employee, lastScreen, _userDetailsVM);
            EmployeesGrid.Children.Add(_employeeDetailsUserControl);
        }

        protected void RetrieveButton_Click(object sender, RoutedEventArgs e)
        {
            _userDetailsVM.ButtonRetrieveClick();
        }

        protected void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _userDetailsVM.ButtonSaveClick();
                if (_employeeDetailsVM != null)
                    _employeeDetailsUserControl.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void SomeProperty_Changed(object sender, TextChangedEventArgs e)
        {
            _userDetailsVM.OneOrMoreFieldsWereUpdated();
        }

        protected void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _userDetailsVM.ButtonsActive = false;
        }

        protected void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _userDetailsVM.OneOrMoreFieldsWereUpdated();
        }
    }
}
