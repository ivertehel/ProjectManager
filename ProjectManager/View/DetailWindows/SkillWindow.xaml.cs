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
    /// Interaction logic for SkillWindows.xaml
    /// </summary>
    public partial class SkillWindow : Window
    {
        private SkillWindowVM _skillWindowVM;

        public SkillWindow(ILoadData lastScreen)
        {
            InitializeComponent();
            _skillWindowVM = new SkillWindowVM(lastScreen);
            DataContext = _skillWindowVM;
        }

        private void SkillsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SkillsListBox.SelectedItem != null)
            {
                _skillWindowVM.Name = SkillsListBox.SelectedItem.ToString();
            }
            SkillsListBox.SelectedItem = null;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            _skillWindowVM.Editing = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _skillWindowVM.AddButtonClick();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _skillWindowVM.RemoveButtonClick();
        }

        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {
            _skillWindowVM.SaveAllButtonClick();
        }

        private void SkillNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _skillWindowVM.Name = SkillNameTextBox.Text;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _skillWindowVM.SaveButtonClick();
        }

        private void CancelAllButton_Click(object sender, RoutedEventArgs e)
        {
            _skillWindowVM.CancelAllChangesButtonClick();
        }
    }
}
