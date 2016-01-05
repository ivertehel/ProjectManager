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

namespace PMView
{
    /// <summary>
    /// Interaction logic for ProjectModuleEdit.xaml
    /// </summary>
    public partial class ProjectModuleEdit : Window
    {
        private ILoadData _lastScreen;

        private ProjectModuleEditVM _projectModuleEditVM;
        private List<CheckBox> _skills = new List<CheckBox>();

        public ProjectModuleEdit(ProjectsUserControlVM projectsUserControlVM)
        {
            InitializeComponent();
            _lastScreen = projectsUserControlVM;
            _projectModuleEditVM = new ProjectModuleEditVM(_lastScreen, projectsUserControlVM);
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
            ////throw new NotImplementedException();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ////throw new NotImplementedException();
        }

        private void AddEmployeeToTheProject_Click(object sender, RoutedEventArgs e)
        {
            (new AddEmployeeToTheProject(_projectModuleEditVM, _projectModuleEditVM)).Show();
        }
    }
}
