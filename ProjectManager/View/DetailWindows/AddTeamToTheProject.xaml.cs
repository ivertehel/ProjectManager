using PMDataLayer;
using PMView.View;
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
    public partial class AddTeamToTheProject : Window
    {
        private ILoadDataSender _lastScreen;
        private ProjectModuleEditVM _projectModuleEditVM;
        private AddTeamToTheProjectVM _addTeamToTheProject;
        private List<CheckBox> _skills = new List<CheckBox>();


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
            throw new NotImplementedException();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddSkill_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
