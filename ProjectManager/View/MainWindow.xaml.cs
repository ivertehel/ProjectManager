using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PMDataLayer;
using System.Data;
using PMView.View;

namespace PMView
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MenuBarUserControl mainBarUserControl = new MenuBarUserControl();
            MenuBarStackPanel.Children.Add(mainBarUserControl);
            ProjectsUserControl projectsUserControl = new ProjectsUserControl();
            BodyStackPanel.Children.Add(projectsUserControl);
        }

        public string GetDataGridCellValue(DataGrid dataGrid, int column)
        {
            var selectedCell = dataGrid.SelectedCells[column];
            var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
            if (cellContent is TextBlock)
            {
                return ((cellContent as TextBlock).Text);
            }
            return "";
        }
    }
}
