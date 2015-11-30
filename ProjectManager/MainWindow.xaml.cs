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

namespace ProjectManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (ProjectsDataGrid.Items.Count == 0)
            {
                initDataLayer();
                LoadViewLayer(ViewLayers.Projects);
                ProjectDetailsGrid.Visibility = Visibility.Hidden;
                //ProjectsGrid.Visibility = Visibility.Hidden;
                ClientsGrid.Visibility = Visibility.Hidden;
            }
        }

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

        public void LoadViewLayer(ViewLayers layer)
        {
            if (layer == ViewLayers.Projects)
            {
                ProjectsButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xAC, 0xF0, 0xFF));
                ProjectsDataGrid.ItemsSource = from items in Order.Items select new { ProjectName = items.Name, Description = items.Description, Price = items.Price, StartDate = items.StartDate.ToShortDateString(), ReleaseDate = items.ReleaseDate.ToShortDateString(), Status = items.Status };
            }
        }

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

        private void fillOrderInfo()
        {
            if (ProjectsDataGrid.SelectedItem == null)
                return;
            Order order = (from items in Order.Items where (items.Name == GetDataGridCellValue(ProjectsDataGrid, 0) && items.Price == decimal.Parse(GetDataGridCellValue(ProjectsDataGrid, 2)) && items.Description == GetDataGridCellValue(ProjectsDataGrid, 1)) select items).FirstOrDefault();

            if (order == null)
            {

                return;
            }
            
            // Set project name
            ProjectNameDetailsGrid.Content = order.Name;

            // Set project details
            var document = new FlowDocument();
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(order.Description);
            document.Blocks.Add(paragraph);
            ProjectOtherInfoDetailsGrid.Document = document;
            
            // Set project price
            PriceDetailsGrid.Content = "Price: " + order.Price + " USD";

            // Set project start date
            StartDateDetailsGrid.Content = "Start date: " + order.StartDate.ToShortDateString();

            // Set project release date
            ReleaseDateDetailsGrid.Content = "Release date: " + order.ReleaseDate.ToShortDateString();

            // Set project status
            ProjectStatusDetailsGrid.Content = order.Status;

            // Set customer field
            CustomerNameDetailsGrid.Content = "Customer: " + order.Client.User.Name;

            // Set teams field
            document = new FlowDocument();
            paragraph = new Paragraph();
            string teams = "";
            List<Team> t = new List<Team>();
            foreach (var item in order.Projects)
            {
                IEnumerable<Team> inProj = from items in item.Teams where !t.Exists(x=>x.Id == items.Id) select items;
                t.AddRange(inProj);
            }
            foreach (var item in t)
            {
                teams += (item.Name + " ");
            }
            paragraph.Inlines.Add(teams);
            document.Blocks.Add(paragraph);
            TeamsTextBoxDetailsGrid.Document = document;

            // Set employees field
            document = new FlowDocument();
            paragraph = new Paragraph();
            string employees = "";
            List<User> e = new List<User>();
            foreach (var item in order.Projects)
            {
                IEnumerable<User> inProj = from items in item.Users where !e.Exists(x => x.Id == items.Id) select items.User;
                e.AddRange(inProj);
            }
            foreach (var item in e)
            {
                employees += (item.Name + " " + item.Surname + " ");
            }
            paragraph.Inlines.Add(employees);
            document.Blocks.Add(paragraph);
            EmployeesTextBoxDetailsGrid.Document = document;

            // Set skills
            document = new FlowDocument();
            paragraph = new Paragraph();
            string skills = "";
            List<Skill> s = new List<Skill>();
            foreach (var item in order.Projects)
            {
                IEnumerable<Skill> inProj = from items in item.Skills where !s.Exists(x => x.Id == items.Id) select items;
                s.AddRange(inProj);
            }
            for(int i=0; i<s.Count; i++)
            { 
                skills += (s[i].Name + ((i<s.Count-1) ? ", " : " "));
            }
            paragraph.Inlines.Add(skills);
            document.Blocks.Add(paragraph);
            SkillsTextBoxDetailsGrid.Document = document;
        }
        private void initDataLayer()
        {
            User e1 = new User()
            {
                Name = "Ivan",
                Surname = "Vertegel",
                Birthday = new DateTime(1994, 10, 13),
                Country = "Ukraine",
                Description = ".NET developer",
                Email = "ivanvertegel@outlook.com",
                Image = "Assets/MaleAvatar.jpg",
                Login = "vsailor",
                Password = "qwerty",
                Role = User.Roles.Employee,
                Skype = "sirius9764",
                State = User.States.Male,
                Status = User.Statuses.Ready
            };

            User e2 = new User()
            {
                Name = "Denis",
                Surname = "Pikushiy",
                Birthday = new DateTime(1995, 08, 17),
                Country = "Ukraine",
                Description = ".NET developer",
                Email = "datrax@ex.ua",
                Image = "Assets/MaleAvatar.jpg",
                Login = "datrax",
                Password = "qwerty",
                Role = User.Roles.Employee,
                Skype = "dat_rax",
                State = User.States.Male,
                Status = User.Statuses.Ready
            };

            User e3 = new User()
            {
                Name = "Krystyna",
                Surname = "Romanyshyn",
                Birthday = new DateTime(1995, 04, 12),
                Country = "Ukraine",
                Description = "QA Engineer",
                Email = "khrystyna1204@gmail.com",
                Image = "Assets/MaleAvatar.jpg",
                Login = "khrystyna1204",
                Password = "qwerty",
                Role = User.Roles.Employee,
                Skype = "khrystyna1204",
                State = User.States.Female,
                Status = User.Statuses.Ready
            };

            User.Items.Add(e1);
            User.Items.Add(e2);
            User.Items.Add(e3);


            User u1 = new User()
            {
                Name = "Olga",
                Surname = "Karpushin",
                Country = "Israel",
                Description = "Low budget",
                Image = "Assets/MaleAvatar.jpg",
                Login = "sharksoft ",
                Password = "qwerty",
                Role = User.Roles.Client,
                Skype = "sharksoft",
                State = User.States.Female,
            };
            User.Items.Add(u1);

            Client c1 = new Client()
            {
                Account = 0m,
                User = u1
            };

            Client.Items.Add(c1);

            Order o1 = new Order()
            {
                Name = "Android Eggsckatcher game",
                Description = "I need Unity 3d Eggsckatcher game like this: https://play.google.com/store/apps/details?id=com.nomoc.wolfonfarm",
                Price = 50,
                ReleaseDate = new DateTime(2015, 12, 31),
                StartDate = new DateTime(2015, 11, 29),
                Status = Order.Statuses.InProgress,
                IsPrivate = false,
                Client = c1
            };
            Order.Items.Add(o1);

            Order o2 = new Order()
            {
                Name = "Android Matches Puzzle game",
                Description = "I need Matches Puzzel game for Unity 3d. Like https://play.google.com/store/apps/details?id=vn.bigfox.pencils",
                Price = 80,
                ReleaseDate = new DateTime(2015, 9, 18),
                StartDate = new DateTime(2015, 7, 15),
                Status = Order.Statuses.Done,
                IsPrivate = false,
                Client = c1
            };
            Order.Items.Add(o2);

            Project p1 = new Project()
            {
                Name = "Android game Matches Puzzle",
                Description = "Unity3D game for Android 4.0.2 and more. 100 puzlles from book. Ads must exist. Like https://play.google.com/store/apps/details?id=vn.bigfox.pencils",
                Leader = e1,
                Order = o2,
                StartDate = o2.StartDate,
                ReleaseDate = o2.ReleaseDate,
                Status = Project.Statuses.InProgress
            };
            Project.Items.Add(p1);

            Team t1 = new Team()
            {
                Name = "Unity3D-1",
                Description = "Unity3D developers"
            };

            Team.Items.Add(t1);

            Team_Project tp1 = new Team_Project()
            {
                Team = t1,
                Project = p1
            };

            Team_Project.Items.Add(tp1);

            Position teamLeadPosition = new Position()
            {
                Name = "Team Lead"
            };

            Position position1 = new Position()
            {
                Name = ".NET developer"
            };

            Position position2 = new Position()
            {
                Name = "Unity3D developer"
            };

            Position position3 = new Position()
            {
                Name = "QA engineer"
            };

            Position.Items.AddRange(new[] { teamLeadPosition, position1, position2 });

            User_Team ut1 = new User_Team()
            {
                User = e1,
                IsLeader = true,
                Postion = teamLeadPosition,
                Team = t1
            };

            User_Team ut2 = new User_Team()
            {
                User = e1,
                IsLeader = true,
                Postion = position1,
                Team = t1
            };

            User_Team ut3 = new User_Team()
            {
                User = e2,
                IsLeader = true,
                Postion = position2,
                Team = t1
            };

            User_Team.Items.AddRange(new[] { ut1, ut2, ut3 });

            User_Project up = new User_Project()
            {
                User = e3,
                Position = position3,
                Project = p1
            };
            User_Project.Items.Add(up);

            Skill s1 = new Skill()
            {
                Name = ".NET"
            };

            Skill s2 = new Skill()
            {
                Name = "QA"
            };

            Skill s3 = new Skill()
            {
                Name = "WPF"
            };

            Skill s4 = new Skill()
            {
                Name = "Unity3D"
            };

            Skill s5 = new Skill()
            {
                Name = "Android"
            };

            Skill.Items.AddRange(new[] { s1, s2, s3, s4, s5 });
            User_Skill us1 = new User_Skill()
            {
                User = e1,
                Skill = s1
            };

            User_Skill us2 = new User_Skill()
            {
                User = e1,
                Skill = s3
            };
            User_Skill us3 = new User_Skill()
            {
                User = e1,
                Skill = s4
            };
            User_Skill us4 = new User_Skill()
            {
                User = e1,
                Skill = s5
            };
            User_Skill us5 = new User_Skill()
            {
                User = e2,
                Skill = s1
            };
            User_Skill us6 = new User_Skill()
            {
                User = e2,
                Skill = s4
            };
            User_Skill us7 = new User_Skill()
            {
                User = e3,
                Skill = s2
            };

            User_Skill.Items.AddRange(new[] { us1, us2, us3, us4, us5, us6, us7 });

            Project_Skill ps1 = new Project_Skill()
            {
                Project = p1,
                Skill = s1
            };

            Project_Skill ps2 = new Project_Skill()
            {
                Project = p1,
                Skill = s2
            };

            Project_Skill ps3 = new Project_Skill()
            {
                Project = p1,
                Skill = s3
            };

            Project_Skill ps4 = new Project_Skill()
            {
                Project = p1,
                Skill = s4
            };

            Project_Skill ps5 = new Project_Skill()
            {
                Project = p1,
                Skill = s5
            };

            Project_Skill.Items.AddRange(new[] { ps1, ps2, ps3, ps4, ps5 });
        }

        private void ProjectsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ProjectDetailsGrid.Visibility = Visibility.Visible;
            fillOrderInfo();
        }

        private void ClientsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}
