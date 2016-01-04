using PMDataLayer;
using PMView.View;
using PMView.View.WrapperVM;
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

namespace PMView
{
    /// <summary>
    /// Interaction logic for AddEmployeeToTheProject.xaml
    /// </summary>
    public partial class AddEmployeeToTheProject : Window
    {
        private AddEmployeeToTheProjectVM _addEmployeeToTheProjectVM;

        private ProjectModuleEditVM _projectModuleEditVM;

        private List<CheckBox> _skills = new List<CheckBox>();

        private UserVM _selectedEmployeeToAdd;

        public AddEmployeeToTheProject(ProjectModuleEditVM projectModuleEditVM)
        {
            InitializeComponent();
            _projectModuleEditVM = projectModuleEditVM;
            _addEmployeeToTheProjectVM = new AddEmployeeToTheProjectVM();
            DataContext = _addEmployeeToTheProjectVM;
            foreach (var item in Skill.Items)
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
                _addEmployeeToTheProjectVM.AddButtonClick((UserVM)EmployeesCollectionDataGrid.SelectedItem);
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
            if (EmployeesToAddListBox.SelectedItem != null)
                _addEmployeeToTheProjectVM.SelectedEmployeeToDelete = null;
            _selectedEmployeeToAdd = (UserVM)EmployeesCollectionDataGrid.SelectedItem;
        }

        private void EmployeesToAddListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesToAddListBox.SelectedItem != null)
                _selectedEmployeeToAdd = (UserVM)EmployeesToAddListBox.SelectedItem;
            EmployeesToAddListBox.SelectedItem = null;
            EmployeesCollectionDataGrid.SelectedItem = null;
        }
    }
}
