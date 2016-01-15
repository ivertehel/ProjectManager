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
using PMDataLayer;
using PMView.View;
using PMView.View.WrapperVM;

namespace PMView
{
    /// <summary>
    /// Interaction logic for AddEmployeeToTheProject.xaml
    /// </summary>
    public partial class AttachEmployee : Window, ILoadDataSender
    {
        private ILoadDataSender _lastScreen;

        private List<CheckBox> _skills = new List<CheckBox>();

        private TeamDetailsVM _teamDetailsVM;

        private UserVM _selectedEmployeeToAdd;
        private List<CheckBox> _positions;
        private IAddEmployee _addEmployeeVM;

        public AttachEmployee(ILoadDataSender lastScreen, ProjectModuleEditVM projectModuleEditVM)
        {
            InitializeComponent();
            _lastScreen = lastScreen;
            _addEmployeeVM = new AddEmployeeToTheProjectVM(this, projectModuleEditVM, this);
            DataContext = _addEmployeeVM;
            fillCheckboxList();
        }

        public AttachEmployee(ILoadDataSender lastScreen, TeamDetailsVM teamDetailsVM)
        {
            InitializeComponent();
            _lastScreen = lastScreen;
            _addEmployeeVM = new AddEmployeeToTheTeamVM(this, teamDetailsVM, this);
            DataContext = _addEmployeeVM;
            fillCheckboxList();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _addEmployeeVM.SelectedSkills.Clear();
            _addEmployeeVM.SelectedSkills.AddRange((from items in _skills where items.IsChecked == true select items.Content.ToString()).ToList());
            _addEmployeeVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeVM.Name = Name.Text;
            _addEmployeeVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeVM.Surname = Surname.Text;
            _addEmployeeVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeVM.Login = Login.Text;
            _addEmployeeVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Skype_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeVM.Skype = Skype.Text;
            _addEmployeeVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeVM.Email = Email.Text;
            _addEmployeeVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _addEmployeeVM.Country = Countries.SelectedValue.ToString();
            _addEmployeeVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Statuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _addEmployeeVM.Status = (User.Statuses)Statuses.SelectedValue;
            _addEmployeeVM.OnPropertyChanged("EmployeesCollection");
        }

        private void States_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _addEmployeeVM.State = (User.States)States.SelectedValue;
            _addEmployeeVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            States.SelectedIndex = 0;
            Statuses.SelectedIndex = 0;
            Countries.SelectedItem = "NotChosen";
            _addEmployeeVM.LoadData(this);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _addEmployeeVM.AddButtonClick(_selectedEmployeeToAdd);

                if (_addEmployeeVM is AddEmployeeToTheTeamVM)
                    HelpText.Visibility = Visibility.Hidden;
                else
                HelpText.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var select = (UserVM)EmployeesCollectionDataGrid.SelectedItem;
            if (select == null)
                select = (UserVM)EmployeesToAddListBox.SelectedItem;
            if (select == null)
                select = _selectedEmployeeToAdd;
            if (select != null)
                _addEmployeeVM.RemoveButtonClick(select);
        }

        private void EmployeesCollectionDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_addEmployeeVM is AddEmployeeToTheTeamVM)
                HelpText.Visibility = Visibility.Hidden;

            if (EmployeesCollectionDataGrid.SelectedItem != null)
            {
                _selectedEmployeeToAdd = (UserVM)EmployeesCollectionDataGrid.SelectedItem;
                _addEmployeeVM.ActivateButtons(_selectedEmployeeToAdd);
            }

            EmployeesToAddListBox.SelectedItem = null;
            EmployeesCollectionDataGrid.SelectedItem = null;
        }

        private void EmployeesToAddListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (EmployeesToAddListBox.SelectedItem != null)
            {
                _selectedEmployeeToAdd = (UserVM)EmployeesToAddListBox.SelectedItem;
                _addEmployeeVM.ActivateButtons(_selectedEmployeeToAdd);


                _addEmployeeVM.SelectedEmployeeToDelete = EmployeesToAddListBox.SelectedItem as UserVM;
                if (_addEmployeeVM is AddEmployeeToTheTeamVM)
                    return;

                PositionsGrid.Visibility = Visibility.Visible;
                _positions = new List<CheckBox>();
                var employeesPositions = _addEmployeeVM.EmployeesPositions;

                foreach (var item in Position.Items)
                {
                    var cb = new CheckBox();
                    cb.Content = item.Name;
                    _positions.Add(cb);
                    cb.IsChecked = _addEmployeeVM.EmployeesPositions.FirstOrDefault(pos => (pos as User_ProjectVM).User.Id == _selectedEmployeeToAdd.User.Id && (pos as User_ProjectVM).Position.Id == item.Id) == null ? false : true;
                    cb.Click += new System.Windows.RoutedEventHandler(this.PositionCheckBox_Checked);
                }

                PositionListBox.Items.Clear();
                foreach (var item in _positions)
                {
                    PositionListBox.Items.Add(item);
                }
            }
            else
            {
                PositionsGrid.Visibility = Visibility.Hidden;
            }

            EmployeesCollectionDataGrid.SelectedItem = null;
        }

        private void PositionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _addEmployeeVM.SavePositionButton = true;
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEmployeeToAdd != null)
                (new UsersDetails(_selectedEmployeeToAdd as IEmployee, _addEmployeeVM)).Show();
        }

        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _addEmployeeVM.SaveButtonClick();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SavePositionsButton_Click(object sender, RoutedEventArgs e)
        {
            var positions = (from items in _positions where items.IsChecked == true select new PositionVM(Position.Items.FirstOrDefault(pos => pos.Name == items.Content.ToString()))).ToList();

            _addEmployeeVM.SavePositionsClick(positions);
            _addEmployeeVM.SavePositionButton = false;
            _addEmployeeVM.SaveButton = true;
        }

        private void AddSkill_Click(object sender, RoutedEventArgs e)
        {
            (new SkillWindow(this)).Show();
        }

        private void fillCheckboxList()
        {
            _skills.Clear();
            SkillsListBox.Items.Clear();
            foreach (var item in Skill.Items)
            {
                var cb = new CheckBox();
                cb.Content = item.Name;
                _skills.Add(cb);
                cb.IsChecked = false;
                cb.Click += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
                SkillsListBox.Items.Add(cb);
            }
        }


        private void AddPositions_Click(object sender, RoutedEventArgs e)
        {
            (new PositionWindow(_addEmployeeVM)).Show();
        }

        public void LoadData(object sender)
        {
            fillCheckboxList();
            _positions = new List<CheckBox>();
            if (_addEmployeeVM == null)
                return;
            var employeesPositions = _addEmployeeVM.EmployeesPositions;

            foreach (var item in Position.Items)
            {
                var cb = new CheckBox();
                cb.Content = item.Name;
                _positions.Add(cb);
                cb.IsChecked = employeesPositions.FirstOrDefault(pos => item.Id == (pos as User_ProjectVM).Position.Id) == null ? false : true;
                cb.Click += new System.Windows.RoutedEventHandler(this.PositionCheckBox_Checked);
            }

            PositionListBox.Items.Clear();
            foreach (var item in _positions)
            {
                PositionListBox.Items.Add(item);
            }
            
            _lastScreen.LoadData(sender);
        }
    }
}
