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
using PMDataLayer;

namespace PMView
{
    /// <summary>
    /// Interaction logic for ProjectModuleEdit.xaml
    /// </summary>
    public partial class ProjectModuleEdit : Window, ILoadData
    {
        private ILoadData _lastScreen;

        private ProjectModuleEditVM _projectModuleEditVM;
        private List<CheckBox> _skills = new List<CheckBox>();
        private ProjectVM _projectVM;

        public ProjectModuleEdit(ProjectsUserControlVM projectsUserControlVM)
        {
            InitializeComponent();
            _lastScreen = projectsUserControlVM;
            _projectModuleEditVM = new ProjectModuleEditVM(this, projectsUserControlVM);
            DataContext = _projectModuleEditVM;
            fillCheckboxList();
        }

        public ProjectModuleEdit(ProjectsUserControlVM projectsUserControlVM, ProjectVM projectVM)
        {
            InitializeComponent();
            _lastScreen = projectsUserControlVM;
            _projectModuleEditVM = new ProjectModuleEditVM(this, projectsUserControlVM, projectVM);
            DataContext = _projectModuleEditVM;
            fillCheckboxList();
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
        }

        private void SomeProperty_Changed(object sender, TextChangedEventArgs e)
        {
            _projectModuleEditVM.SaveButton = true;
        }

        private void LeadersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _projectModuleEditVM.SelectedLeader = LeadersCollection.SelectedItem as UserVM;
        }

        private void AddEmployeeToTheProject_Click(object sender, RoutedEventArgs e)
        {
            (new AddEmployeeToTheProject(_projectModuleEditVM, _projectModuleEditVM)).Show();
        }

        private void AddSkill_Click(object sender, RoutedEventArgs e)
        {
            (new SkillWindow(_projectModuleEditVM)).Show();
        }

        public void LoadData()
        {
            fillCheckboxList();
            _lastScreen.LoadData();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _projectModuleEditVM.AddProject((from items in _skills where items.IsChecked == true select items.Content.ToString()).ToArray());
                MessageBox.Show("Module was saved", "Module information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StateCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
