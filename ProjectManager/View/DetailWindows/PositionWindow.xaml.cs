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

        public PositionWindow(ILoadData lastScreen)
        {
            InitializeComponent();
            _positionWindowVM = new PositionWindowVM();
            DataContext = _positionWindowVM;
        }

        private void PositionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PositionNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelAllButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
