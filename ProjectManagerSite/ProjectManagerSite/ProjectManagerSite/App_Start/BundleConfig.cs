using PMDataLayer;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ProjectManagerSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            GenerateData();
            
        }

        public static void GenerateData()
        {
            User.Update();
            Client.Update();
            User e1 = User.Items.Where(item => item.Login == "vsailor").FirstOrDefault();
            User e2 = User.Items.Where(item => item.Login == "datrax").FirstOrDefault();
            User e3 = User.Items.Where(item => item.Login == "khrystyna1204").FirstOrDefault();
            User e4 = User.Items.Where(item => item.Login == "hacapet89").FirstOrDefault();
            User u1 = User.Items.Where(item => item.RoleType == User.Roles.Client).FirstOrDefault();
            Client c1 = Client.Items.Where(item => item.User.Id == u1.Id).FirstOrDefault();
            Order.Update();
            Order o2 = Order.Items[0];
            Project.Update();
            Project p1 = Project.Items[0];
            Project_Project.Update();

            Team t1 = new Team()
            {
                Name = "Unity3D-1",
                Description = "Unity3D developers"
            };

            Team t2 = new Team()
            {
                Name = "Java-1",
                Description = "Java developers"
            };
            Team.Items.Add(t1);
            Team.Items.Add(t2);

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
