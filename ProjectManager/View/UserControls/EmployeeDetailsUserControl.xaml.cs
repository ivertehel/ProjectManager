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
using PMDataLayer;
using PMView.View.WrapperVM;

namespace PMView.View
{
    /// <summary>
    /// Interaction logic for EmployeeDetailsUserControl.xaml
    /// </summary>
    public partial class EmployeeDetailsUserControl : UserControl, ILoadDataSender
    {
        private EmployeeDetailsUserControlVM _employeeDetailsUserControlVM;

        private UserVM _currentUser;

        private List<CheckBox> _skills = new List<CheckBox>();

        private UserDetailsVM _userDetailsVM;
        private List<CheckBox> _savedSkills;
        private ILoadDataSender _lastScreen;

        public EmployeeDetailsUserControl(IEmployee employee, ILoadDataSender lastScreen, UserDetailsVM userDetailsVM)
        {
            InitializeComponent();
            _lastScreen = lastScreen;
            _userDetailsVM = userDetailsVM;
            _currentUser = employee as UserVM;
            _employeeDetailsUserControlVM = new EmployeeDetailsUserControlVM(employee, lastScreen);
            DataContext = _employeeDetailsUserControlVM;
            LoadSkills();

        }

        public void LoadSkills()
        {
            foreach (var item in SkillVM.Skills)
            {
                var cb = new CheckBox();
                cb.Content = item;
                _skills.Add(cb);
                cb.Click += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
                cb.IsChecked = _employeeDetailsUserControlVM.GetSkills(_currentUser).Where(skill => skill == item.Name).Count() != 0 ? true : false;
            }

            SkillsListBox.Items.Clear();
            foreach (var item in _skills)
            {
                SkillsListBox.Items.Add(item);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _userDetailsVM.OneOrMoreFieldsWereUpdated();
        }

        public void SaveChanges()
        {
            List<string> newSkills = new List<string>();
            foreach (var item in _skills)
            {
                if (item.IsChecked == true)
                    newSkills.Add(item.Content.ToString());
            }

            _employeeDetailsUserControlVM.SaveChanges(newSkills);
        }

        private void fillCheckboxList()
        {

            _skills.Clear();
            foreach (var item in SkillVM.Skills)
            {
                var cb = new CheckBox();
                cb.Content = item.Name;
                _skills.Add(cb);
                cb.IsChecked = false;
            }

            SkillsListBox.Items.Clear();
            foreach (var item in _skills)
            {
                SkillsListBox.Items.Add(item);
            }

            foreach (var item in _skills)
            {
                if (_savedSkills == null)
                    item.IsChecked = false;
                else
                {
                    item.Click += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
                    var isExist = _savedSkills.FirstOrDefault(skill => skill.Content.ToString() == item.Content.ToString());
                    item.IsChecked = isExist == null ? false : isExist.IsChecked;
                }
            }
            _savedSkills = null;

        }

        private void AddSkill_Click(object sender, RoutedEventArgs e)
        {
            _savedSkills = new List<CheckBox>();
            foreach (var item in _skills)
            {
                _savedSkills.Add(item);
            }

            (new SkillWindow(this)).Show();
        }

        public void LoadData(object sender)
        {
            fillCheckboxList();
            _lastScreen.LoadData(this);
        }
    }
}
