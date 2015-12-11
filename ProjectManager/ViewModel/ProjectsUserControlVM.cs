using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PMDataLayer;
using Core;
using PMView.View.WrapperVM;

namespace PMView.View
{
    public class ProjectsUserControlVM : INotifyPropertyChanged
    {
        private ObservableCollection<OrderVM> _ordersCollection = new ObservableCollection<OrderVM>();

        private ObservableCollection<Project> _projectsCollection = new ObservableCollection<Project>();

        private ObservableCollection<User> _employeesCollection = new ObservableCollection<User>();

        private ObservableCollection<TeamVM> _teamsCollection = new ObservableCollection<TeamVM>();

        private ObservableCollection<Task> _taskCollection = new ObservableCollection<Task>();

        private OrderVM _selectedOrder;

        public ProjectsUserControlVM()
        {
            Logger.Info("First screen","Main screen has been open");
            if (User.Items.Count == 0)
                GenerateData();

            foreach (var item in Order.Items)
            {
                OrdersCollection.Add(new OrderVM(item));
            }

            SelectedOrder = OrdersCollection[1];
        }

        public ProjectsUserControlVM(Order order) : this()
        {
            SelectedOrder.Order = order;
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public OrderVM SelectedOrder
        {
            get
            {
                return _selectedOrder;
            }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged("Skills");
                OnPropertyChanged("CustomerName");
                OnPropertyChanged("ProjectStatus");
                OnPropertyChanged("ReleaseDate");
                OnPropertyChanged("StartDate");
                OnPropertyChanged("Price");
                OnPropertyChanged("Description");
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Skills of selected order
        /// </summary>
        public string Skills
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                string skills = string.Empty;
                List<Skill> s = new List<Skill>();
                foreach (var item in SelectedOrder.Order.Projects)
                {
                    IEnumerable<Skill> inProj = from items in item.Skills where !s.Exists(x => x.Id == items.Id) select items;
                    s.AddRange(inProj);
                }

                for (int i = 0; i < s.Count; i++)
                {
                    skills += s[i].Name + (i < s.Count - 1 ? ", " : " ");
                }

                return skills;
            }
        }

        /// <summary>
        /// Teams of selected order
        /// </summary>
        public List<TeamVM> Teams
        {
            get
            {
                if (SelectedOrder == null)
                    return new List<TeamVM>();

                List<Team> t = new List<Team>();
                foreach (var item in SelectedOrder.Order.Projects)
                {
                    IEnumerable<Team> inProj = from items in item.Teams where !t.Exists(x => x.Id == items.Id) select items;
                    t.AddRange(inProj);
                }

                List<TeamVM> teams = new List<TeamVM>();

                foreach (var item in t)
                {
                    teams.Add(new TeamVM(item));
                }

                return teams;
            }
        }

        /// <summary>
        /// Employees of selected order
        /// </summary>
        public List<User> Employees
        {
            get
            {
                if (SelectedOrder == null)
                    return null;
                List<User> e = new List<User>();
                foreach (var item in SelectedOrder.Order.Projects)
                {
                    IEnumerable<User> inProj = from items in item.Users where !e.Exists(x => x.Id == items.Id) select items.User;
                    e.AddRange(inProj);
                }

                return e;
            }
        }

        public List<Task> Tasks
        {
            get
            {
                if (SelectedOrder == null)
                    return new List<Task>();

                List<Task> t = new List<Task>();
                foreach (var team in Teams)
                {
                    var tasks = from items in Task.TeamsTasks where items.OwnerId == team.Team.Id select items;
                    t.AddRange(tasks);
                }

                foreach (var employee in Employees)
                {
                    var tasks = from items in Task.UsersTasks where items.OwnerId == employee.Id select items;
                    t.AddRange(tasks);
                }

                return t;
            }
        }

        /// <summary>
        /// Customer name of selected order
        /// </summary>
        public string CustomerName
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;
                return "Customer: " + SelectedOrder.Client.User.Name + " " + SelectedOrder.Client.User.Surname;
            }
        }

        /// <summary>
        /// Status of selected order
        /// </summary>
        public Order.Statuses ProjectStatus
        {
            get
            {
                if (SelectedOrder == null)
                    return Order.Statuses.Open;

                return SelectedOrder.Status;
            }
        }

        /// <summary>
        /// Release date of selected order
        /// </summary>
        public string ReleaseDate
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return "Release date: " + SelectedOrder.ReleaseDate.ToShortDateString();
            }
        }

        /// <summary>
        /// Start date of selected order
        /// </summary>
        public string StartDate
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return "Start date: " + SelectedOrder.StartDate.ToShortDateString();
            }
        }

        /// <summary>
        /// Price of selected order
        /// </summary>
        public string Price
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return "Price: " + SelectedOrder.Price + " USD";
            }
        }

        /// <summary>
        /// Description of selected order
        /// </summary>
        public string Description
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return SelectedOrder.Description;
            }
        }

        /// <summary>
        /// Description of selected order
        /// </summary>
        public string Name
        {
            get
            {
                if (SelectedOrder == null)
                    return string.Empty;

                return SelectedOrder.Name;
            }
        }

        public IEnumerable<Project> Projects
        {
            get { return from items in Project_Project.Items where items.ParrentProject == null && items.ChildProject.Order == SelectedOrder.Order select items.ChildProject; }
        }

        public ObservableCollection<OrderVM> OrdersCollection
        {
            get { return _ordersCollection; }
        }

        public ObservableCollection<Project> ProjectsCollection
        {
            get { return _projectsCollection; }
        }

        public ObservableCollection<User> EmployeesCollection
        {
            get { return _employeesCollection; }
        }

        public ObservableCollection<TeamVM> TeamsCollection
        {
            get { return _teamsCollection; }
        }

        public ObservableCollection<Task> TasksCollection
        {
            get { return _taskCollection; }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void LoadData()
        {
            if (SelectedOrder == null)
                return;

            LoadDetails();

            ProjectsCollection.Clear();
            var projects = Projects;
            foreach (var item in projects)
            {
                ProjectsCollection.Add(item);
            }

            //EmployeesCollection.Clear();
            //var emp = Employees;
            //foreach (var item in emp)
            //{
            //    EmployeesCollection.Add(item);
            //}

            TeamsCollection.Clear();
            var tm = Teams;
            foreach (var item in tm)
            {
                TeamsCollection.Add(item);
            }

            //TasksCollection.Clear();
            //var tsk = Tasks;
            //foreach (var item in tsk)
            //{
            //    TasksCollection.Add(item);
            //}
        }

        public void LoadDetails()
        {
            OnPropertyChanged("Skills");
            OnPropertyChanged("Teams");
            OnPropertyChanged("Employees");
            OnPropertyChanged("CustomerName");
            OnPropertyChanged("ProjectStatus");
            OnPropertyChanged("ReleaseDate");
            OnPropertyChanged("StartDate");
            OnPropertyChanged("Price");
            OnPropertyChanged("Description");
            OnPropertyChanged("Name");
        }

        public void GenerateData()
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
                Name = "Khrystyna",
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
            User e4 = new User()
            {
                Name = "Panteleymon",
                Surname = "Golobudko",
                Birthday = new DateTime(1989, 03, 01),
                Country = "Ukraine",
                Description = "Junior Java developer",
                Email = "hacapet@haca.com",
                Image = "Assets/MaleAvatar.jpg",
                Login = "hacapet89",
                Password = "qwerty",
                Role = User.Roles.Employee,
                Skype = "hacapet89",
                State = User.States.Male,
                Status = User.Statuses.Ready
            };

            User.Items.Add(e1);
            User.Items.Add(e2);
            User.Items.Add(e3);
            User.Items.Add(e4);

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

            Project_Project pp1 = new Project_Project()
            {
                ChildProject = p1
            };

            Project_Project.Items.Add(pp1);

            Team t1 = new Team()
            {
                Name = "Unity3D-1",
                Description = "Unity3D developers"
            };

            Team.Items.Add(t1);

            Task task1 = new Task()
            {
                Name = "Create a main menu",
                Description = "Main menu with the next buttons: Start game, Continue game, Levels, Settings, Quit",
                Hours = 5,
                Owner = Task.Owners.Team,
                OwnerId = t1.Id,
                Priority = 1,
                Project = p1,
                Status = "Active"
            };

            Task.Items.Add(task1);

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

            Position position4 = new Position()
            {
                Name = "Junior Java developer"
            };

            Position.Items.AddRange(new[] { teamLeadPosition, position1, position2, position4, position3 });

            User_Team ut1 = new User_Team()
            {
                User = e1,
                IsLeader = true,
                Position = teamLeadPosition,
                Team = t1
            };

            User_Team ut2 = new User_Team()
            {
                User = e1,
                IsLeader = true,
                Position = position1,
                Team = t1
            };

            User_Team ut3 = new User_Team()
            {
                User = e2,
                IsLeader = true,
                Position = position2,
                Team = t1
            };
            User_Team ut4 = new User_Team()
            {
                User = e3,
                IsLeader = true,
                Position = position3,
                Team = t1
            };

            User_Team.Items.AddRange(new[] { ut1, ut2, ut3, ut4 });

            User_Project up = new User_Project()
            {
                User = e3,
                Position = position3,
                Project = p1
            };
            User_Project up2 = new User_Project()
            {
                User = e4,
                Position = position4,
                Project = p1
            };

            User_Project.Items.Add(up);
            User_Project.Items.Add(up2);

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
                Skill = s1,
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
    }
}
