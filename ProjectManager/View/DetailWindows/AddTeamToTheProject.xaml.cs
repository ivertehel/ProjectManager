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
    /// Interaction logic for AddTeamToTheProject.xaml
    /// </summary>
    public partial class AddTeamToTheProject : Window, ILoadDataSender
    {
        private ILoadDataSender _lastScreen;
        private ProjectModuleEditVM _projectModuleEditVM;
        private AddTeamToTheProjectVM _addTeamToTheProject;
        private List<CheckBox> _skills = new List<CheckBox>();
        private TeamVM _selectedTeamToAdd;

        public AddTeamToTheProject(ILoadDataSender lastScreen, ProjectModuleEditVM projectModuleEditVM)
        {
            InitializeComponent();
            _lastScreen = lastScreen;
            _projectModuleEditVM = projectModuleEditVM;
            _addTeamToTheProject = new AddTeamToTheProjectVM(lastScreen, projectModuleEditVM, this);
            DataContext = _addTeamToTheProject;
            fillCheckboxList();
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _addTeamToTheProject.SelectedSkills.Clear();
            _addTeamToTheProject.SelectedSkills.AddRange((from items in _skills where items.IsChecked == true select items.Content.ToString()).ToList());
            _addTeamToTheProject.OnPropertyChanged("TeamsCollection");
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _addTeamToTheProject.AddButtonClick(_selectedTeamToAdd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var select = (TeamVM)TeamsCollectionDataGrid.SelectedItem;
            if (select == null)
                select = (TeamVM)TeamsToAddListBox.SelectedItem;
            if (select == null)
                select = _selectedTeamToAdd;
            if (select != null)
                _addTeamToTheProject.RemoveButtonClick(select);
        }

        private void AddSkill_Click(object sender, RoutedEventArgs e)
        {
            (new SkillWindow(this)).Show();
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            _addTeamToTheProject.Name = Name.Text;
            _addTeamToTheProject.OnPropertyChanged("TeamsCollection");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTeamToAdd != null)
                (new TeamsDetails(_selectedTeamToAdd, this)).Show();
        }

        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {

        }


        public void LoadData(object sender)
        {
            fillCheckboxList();
            _lastScreen.LoadData(sender);
        }

        private void TeamsToAddListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamsToAddListBox.SelectedItem != null)
            {
                _selectedTeamToAdd = (TeamVM)TeamsToAddListBox.SelectedItem;

                _addTeamToTheProject.ActivateButtons(_selectedTeamToAdd);
            }

            TeamsToAddListBox.SelectedItem = null;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamsCollectionDataGrid.SelectedItem != null)
            {
                _selectedTeamToAdd = (TeamVM)TeamsCollectionDataGrid.SelectedItem;

                _addTeamToTheProject.ActivateButtons(_selectedTeamToAdd);
            }
        }
    }
}
