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

            MenuBarUserControl mbu = new MenuBarUserControl();
            MenuBarStackPanel.Children.Add(mbu);
            ProjectsUserControl puc = new ProjectsUserControl();
            ProjectsStackPanel.Children.Add(puc);

            //if (ProjectsDataGrid.Items.Count == 0)
            //{
            //    initDataLayer();
            //    ButtonPanel = new List<Button>();
            //    Layers = new List<Grid>();
            //    Layers.AddRange(new[] { ProjectsGrid, ClientsGrid });
            //    ButtonPanel.AddRange(new[] { ProjectsButton, ClientsButton, EmployeesButton, TeamsButton, ReportsButton, MessagesButton });
            //    LoadViewLayer(ViewLayers.Projects);
            //    ProjectDetailsGrid.Visibility = Visibility.Hidden;
            //    //ProjectsGrid.Visibility = Visibility.Hidden;
            //    ClientsGrid.Visibility = Visibility.Hidden;

            //}

        }

       public List<Button> ButtonPanel;
        public List<Grid> Layers;
        public enum ViewLayers
        {
            Projects,
            Clients,
            Employees,
            Teams,
            Reports,
            Messages
        }

        public ViewLayers LoadedLayer { get; set; }

        //public void LoadViewLayer(ViewLayers layer)
        //{
        //    foreach (var item in ButtonPanel)
        //    {
        //        item.Background = (new SolidColorBrush(Colors.SkyBlue));
        //    }
        //    foreach (var item in Layers)
        //    {
        //        item.Visibility = Visibility.Hidden;
        //    }
        //    if (layer == ViewLayers.Projects)
        //    {
        //        ProjectsButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xAC, 0xF0, 0xFF));
        //        ProjectsDataGrid.ItemsSource = from items in Order.Items select new { ProjectName = items.Name, Description = items.Description, Price = items.Price, StartDate = items.StartDate.ToShortDateString(), ReleaseDate = items.ReleaseDate.ToShortDateString(), Status = items.Status };
        //        (from items in Layers where items == ProjectsGrid select items).FirstOrDefault().Visibility = Visibility.Visible;
        //    }
        //    else if (layer == ViewLayers.Clients)
        //    {
        //        ClientsButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xAC, 0xF0, 0xFF));
        //        (from items in Layers where items == ClientsGrid select items).FirstOrDefault().Visibility = Visibility.Visible;

        //    }
        //}

        public string GetDataGridCellValue(DataGrid dataGrid, int column)
        {
            var selectedCell = dataGrid.SelectedCells[column];
            var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
            if (cellContent is TextBlock)
            {
                return((cellContent as TextBlock).Text);
            }
            return "";
        }

        //private void fillOrderInfo()
        //{
        //    if (ProjectsDataGrid.SelectedItem == null)
        //        return;
        //    Order order = (from items in Order.Items where (items.Name == GetDataGridCellValue(ProjectsDataGrid, 0) && items.Price == decimal.Parse(GetDataGridCellValue(ProjectsDataGrid, 2)) && items.Description == GetDataGridCellValue(ProjectsDataGrid, 1)) select items).FirstOrDefault();

        //    if (order == null)
        //    {

        //        return;
        //    }
            
        //    // Set project name
        //    ProjectNameDetailsGrid.Content = order.Name;

        //    // Set project details
        //    var document = new FlowDocument();
        //    var paragraph = new Paragraph();
        //    paragraph.Inlines.Add(order.Description);
        //    document.Blocks.Add(paragraph);
        //    ProjectOtherInfoDetailsGrid.Document = document;
            
        //    // Set project price
        //    PriceDetailsGrid.Content = "Price: " + order.Price + " USD";

        //    // Set project start date
        //    StartDateDetailsGrid.Content = "Start date: " + order.StartDate.ToShortDateString();

        //    // Set project release date
        //    ReleaseDateDetailsGrid.Content = "Release date: " + order.ReleaseDate.ToShortDateString();

        //    // Set project status
        //    ProjectStatusDetailsGrid.Content = order.Status;

        //    // Set customer field
        //    CustomerNameDetailsGrid.Content = "Customer: " + order.Client.User.Name;

        //    // Set teams field
        //    document = new FlowDocument();
        //    paragraph = new Paragraph();
        //    string teams = "";
        //    List<Team> t = new List<Team>();
        //    foreach (var item in order.Projects)
        //    {
        //        IEnumerable<Team> inProj = from items in item.Teams where !t.Exists(x=>x.Id == items.Id) select items;
        //        t.AddRange(inProj);
        //    }
        //    foreach (var item in t)
        //    {
        //        teams += (item.Name + " ");
        //    }
        //    paragraph.Inlines.Add(teams);
        //    document.Blocks.Add(paragraph);
        //    TeamsTextBoxDetailsGrid.Document = document;

        //    // Set employees field
        //    document = new FlowDocument();
        //    paragraph = new Paragraph();
        //    string employees = "";
        //    List<User> e = new List<User>();
        //    foreach (var item in order.Projects)
        //    {
        //        IEnumerable<User> inProj = from items in item.Users where !e.Exists(x => x.Id == items.Id) select items.User;
        //        e.AddRange(inProj);
        //    }
        //    foreach (var item in e)
        //    {
        //        employees += (item.Name + " " + item.Surname + " ");
        //    }
        //    paragraph.Inlines.Add(employees);
        //    document.Blocks.Add(paragraph);
        //    EmployeesTextBoxDetailsGrid.Document = document;

        //    // Set skills
        //    document = new FlowDocument();
        //    paragraph = new Paragraph();
        //    string skills = "";
        //    List<Skill> s = new List<Skill>();
        //    foreach (var item in order.Projects)
        //    {
        //        IEnumerable<Skill> inProj = from items in item.Skills where !s.Exists(x => x.Id == items.Id) select items;
        //        s.AddRange(inProj);
        //    }
        //    for(int i=0; i<s.Count; i++)
        //    { 
        //        skills += (s[i].Name + ((i<s.Count-1) ? ", " : " "));
        //    }
        //    paragraph.Inlines.Add(skills);
        //    document.Blocks.Add(paragraph);
        //    SkillsTextBoxDetailsGrid.Document = document;
        //}

        private void ProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectsUserControl puc = new ProjectsUserControl();
            ProjectsStackPanel.Children.Add(puc);
            //LoadViewLayer(ViewLayers.Projects);
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
           // LoadViewLayer(ViewLayers.Clients);

        }
    }
}
