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
    public partial class AddEmployeeToTheProject : Window
    {
        private AddEmployeeToTheProjectVM _addEmployeeToTheProjectVM;

        private ILoadData _lastScreen;

        private List<CheckBox> _skills = new List<CheckBox>();

        private ProjectModuleEditVM _projectModuleEditVM;

        private UserVM _selectedEmployeeToAdd;
        private List<CheckBox> _positions;

        public AddEmployeeToTheProject(ILoadData lastScreen, ProjectModuleEditVM projectModuleEditVM)
        {
            InitializeComponent();
            _lastScreen = lastScreen;
            _projectModuleEditVM = projectModuleEditVM;
            _addEmployeeToTheProjectVM = new AddEmployeeToTheProjectVM(lastScreen, projectModuleEditVM);
            DataContext = _addEmployeeToTheProjectVM;
            foreach (var item in SkillVM.Skills)
            {
                var cb = new CheckBox();
                cb.Content = item.Name;
                _skills.Add(cb);
                cb.IsChecked = false;
                cb.Click += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            }

            SkillsListBox.Items.Clear();
            foreach (var item in _skills)
            {
                SkillsListBox.Items.Add(item);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _addEmployeeToTheProjectVM.SelectedSkills.Clear();
            _addEmployeeToTheProjectVM.SelectedSkills.AddRange((from items in _skills where items.IsChecked == true select items.Content.ToString()).ToList());
            _addEmployeeToTheProjectVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeToTheProjectVM.Name = Name.Text;
            _addEmployeeToTheProjectVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeToTheProjectVM.Surname = Surname.Text;
            _addEmployeeToTheProjectVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeToTheProjectVM.Login = Login.Text;
            _addEmployeeToTheProjectVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Skype_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeToTheProjectVM.Skype = Skype.Text;
            _addEmployeeToTheProjectVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addEmployeeToTheProjectVM.Email = Email.Text;
            _addEmployeeToTheProjectVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _addEmployeeToTheProjectVM.Country = Countries.SelectedValue.ToString();
            _addEmployeeToTheProjectVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Statuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _addEmployeeToTheProjectVM.Status = (User.Statuses)Statuses.SelectedValue;
            _addEmployeeToTheProjectVM.OnPropertyChanged("EmployeesCollection");
        }

        private void States_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _addEmployeeToTheProjectVM.State = (User.States)States.SelectedValue;
            _addEmployeeToTheProjectVM.OnPropertyChanged("EmployeesCollection");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            States.SelectedIndex = 0;
            Statuses.SelectedIndex = 0;
            Countries.SelectedItem = "NotChosen";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _addEmployeeToTheProjectVM.AddButtonClick(_selectedEmployeeToAdd);
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
                _addEmployeeToTheProjectVM.RemoveButtonClick(select);
        }

        private void EmployeesCollectionDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesCollectionDataGrid.SelectedItem != null)
            {
                _selectedEmployeeToAdd = (UserVM)EmployeesCollectionDataGrid.SelectedItem;
                _addEmployeeToTheProjectVM.ActivateButtons(_selectedEmployeeToAdd);
            }

            EmployeesToAddListBox.SelectedItem = null;
            EmployeesCollectionDataGrid.SelectedItem = null;
        }

        private void EmployeesToAddListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesToAddListBox.SelectedItem != null)
            {
                _selectedEmployeeToAdd = (UserVM)EmployeesToAddListBox.SelectedItem;
                _addEmployeeToTheProjectVM.ActivateButtons(_selectedEmployeeToAdd);
                PositionsGrid.Visibility = Visibility.Visible;
                _addEmployeeToTheProjectVM.SelectedEmployeeToDelete = (EmployeesToAddListBox.SelectedItem as UserVM);
                _positions = new List<CheckBox>();
                var employeesPositions = _addEmployeeToTheProjectVM.EmployeesPositions;

                foreach (var item in Position.Items)
                {
                    var cb = new CheckBox();
                    cb.Content = item.Name;
                    _positions.Add(cb);
                    cb.IsChecked = employeesPositions.Contains(item.Name) ? true : false;
                    cb.Click += new System.Windows.RoutedEventHandler(this.PositionCheckBox_Checked);
                }

                PositionListBox.Items.Clear();
                foreach (var item in _positions)
                {
                    PositionListBox.Items.Add(item);
                }
            }

            EmployeesCollectionDataGrid.SelectedItem = null;
            //EmployeesToAddListBox.SelectedItem = null;
        }

        private void PositionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEmployeeToAdd != null)
                (new UsersDetails(_selectedEmployeeToAdd as IEmployee, _addEmployeeToTheProjectVM)).Show();
        }

        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {
            _addEmployeeToTheProjectVM.SaveButtonClick();
        }

        private void RetrievePositionsButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SavePositionsButton_Click(object sender, RoutedEventArgs e)
        {
            _addEmployeeToTheProjectVM.SavePositionsClick((from items in _positions where items.IsChecked == true select items.Content.ToString()).ToList());
        }
    }
}
