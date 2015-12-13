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

namespace PMView
{
    /// <summary>
    /// Interaction logic for UsersDetails.xaml
    /// </summary>
    public partial class UsersDetails : Window
    {
        private IUser _user;

        private UserDetailsVM _userDetailsVM;

        public UsersDetails(IUser user)
        {
            InitializeComponent();
            _user = user;
            _userDetailsVM = new UserDetailsVM(user as UserVM);
            DataContext = _userDetailsVM;
        }
    }
}
