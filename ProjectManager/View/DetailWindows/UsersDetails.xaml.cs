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
using PMView.View.WrapperVM;
using Core;
using Microsoft.Win32;

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

        public UsersDetails(IUser user, ILoadDataSender lastScreen)
        {
            InitializeComponent();
            var u = user as UserVM;
            if (u.Login == null)
            {
                u.Birthday = DateTime.Now;
                u.Password = Guid.NewGuid().ToString();
                u.Role = PMDataLayer.User.Roles.Client;
            }

            _user = user;
            _userDetailsVM = new UserDetailsVM(user as UserVM, lastScreen);
            DataContext = _userDetailsVM;
        }

        public UsersDetails(IEmployee employee, ILoadDataSender lastScreen) : this(employee as IUser, lastScreen)
        {
            InitializeComponent();
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
            _userDetailsVM.ButtonSaveClick();
            if (_employeeDetailsVM != null)
                _employeeDetailsUserControl.SaveChanges();
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

        private void LoadProfileImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == false) return;
            string type = UserVM.TypeOfImage(openFileDialog1.FileName);
            if (type == "JPG" || type == "PNG")
            {
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.UriSource = new Uri(openFileDialog1.FileName);
                b.EndInit();
                _userDetailsVM.BitmapImage = b;
            }
            else
            {
                _userDetailsVM.BitmapImage = new BitmapImage(new Uri(Environment.CurrentDirectory + @"//Assets//MaleAvatar.jpg"));
            }

            _userDetailsVM.ButtonsActive = true;
        }
    }
}
