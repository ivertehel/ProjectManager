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
    /// Interaction logic for PositionWindow.xaml
    /// </summary>
    public partial class PositionWindow : Window
    {
        private PositionWindowVM _positionWindowVM;

        public PositionWindow(ILoadDataSender lastScreen)
        {
            InitializeComponent();
            _positionWindowVM = new PositionWindowVM(lastScreen);
            DataContext = _positionWindowVM;
        }

        private void PositionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionsListBox.SelectedItem != null)
            {
                _positionWindowVM.Name = PositionsListBox.SelectedItem.ToString();
            }

            PositionsListBox.SelectedItem = null;
        }

        private void PositionNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _positionWindowVM.Name = PositionNameTextBox.Text;
            if (!_positionWindowVM.Editing)
                PositionNameTextBoxBackground.Background = Brushes.White;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _positionWindowVM.AddButtonClick();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            _positionWindowVM.Editing = true;
            PositionNameTextBoxBackground.Background = Brushes.LightSkyBlue;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _positionWindowVM.RemoveButtonClick();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _positionWindowVM.SaveButtonClick();
        }

        private void CancelAllButton_Click(object sender, RoutedEventArgs e)
        {
            _positionWindowVM.CancelAllChangesButtonClick();
        }

        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("All your changes may change exist emloyees positions", "Are you sure?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                _positionWindowVM.SaveAllButtonClick();
        }
    }
}
