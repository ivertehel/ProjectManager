using ProjectManagerSite.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerSite.Models
{
    public class UserVM : BaseVM
    {
        private Users _authorizeUser;

        public UserVM(IPrincipal user) : base(user)
        {
            init(user);
        }

        private void init(IPrincipal user)
        {
            _authorizeUser = User;
            Name = User.Name;
            Surname = User.Surname;
            Skype = User.Skype;
            Email = User.Email;
            Description = User.Description;
            Avatar = User.Image;
        }

        public UserVM() : base(null)
        {

        }

        public UserVM(string login) : base(null)
        {
            User = GetUserByLogin(login);
            Name = User.Name;
            Surname = User.Surname;
            Skype = User.Skype;
            Email = User.Email;
            Description = User.Description;
            Avatar = User.Image;
        }


        public UserVM(IPrincipal user, UserVM editedUser, SkillsVM skill) : this(user)
        {
            _editedUser = editedUser;
            _editedSkills = skill;
        }

        private UserVM _editedUser;
        private SkillsVM _editedSkills;

        public void SaveChanges()
        {
            Name = User.Name = _editedUser.Name;
            Surname = User.Surname = _editedUser.Surname;
            Skype = User.Skype = _editedUser.Skype;
            Email = User.Email = _editedUser.Email;
            Description = User.Description = _editedUser.Description;
            if (_editedUser.Avatar != null)
                Avatar = User.Image = _editedUser.Avatar;
            Model.Users_Skills.RemoveRange(Model.Users_Skills.ToList().FindAll(item => item.UserId == User.Id));

            foreach (var item in _editedSkills.CheckBoxes)
            {
                if (item.Enabled)
                {
                    Model.Users_Skills.Add(new Users_Skills()
                    {
                        Id = Guid.NewGuid(),
                        SkillId = Model.Skills.FirstOrDefault(skill => skill.Name == item.Name).Id,
                        UserId = User.Id
                    });
                }
            }

            Model.SaveChanges();

        }

        public Users AuthorizeUser
        {
            get
            {
                return _authorizeUser;
            }
        }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name is not valid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Surname is not valid")]
        public string Surname { get; set; }

        public string Skype { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public byte[] Avatar { get; set; }

        public string Description { get; set; }

        public int DoneProjects
        {
            get
            {
                return getProjectsCountByStatus("Done");
            }
        }


        public int OrderedProjects
        {
            get
            {
                if (User == null)
                    return 0;
                return Model.Orders.ToList().FindAll(item => item.Clients.Users.Id == User.Id).Count;
            }
        }

        public int OrdersInProgress
        {
            get
            {
                if (User == null)
                    return 0;
                return Model.Orders.ToList().FindAll(item => item.Clients.Users.Id == User.Id && item.Status == "InProgress").Count;
            }
        }


        public int OrdersDone
        {
            get
            {
                if (User == null)
                    return 0;
                return Model.Orders.ToList().FindAll(item => item.Clients.Users.Id == User.Id && item.Status == "Done").Count;
            }
        }

        public int InProgress
        {
            get
            {
                return getProjectsCountByStatus("InProgress");
            }
        }

        private int getProjectsCountByStatus(string status)
        {
            int count = 0;
            if (User == null)
                return 0;

            foreach (var order in Model.Orders)
            {
                foreach (var project in order.Projects)
                {
                    if (Model.Users_Projects.FirstOrDefault(item => item.ProjectId == project.Id && item.UserId == User.Id) != null && order.Status == status)
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }

        public List<Orders> Orders
        {
            get
            {
                List<Orders> orders = new List<EF.Orders>();
                foreach (var order in Model.Orders)
                {
                    foreach (var project in order.Projects)
                    {
                        if (Model.Users_Projects.FirstOrDefault(item => item.ProjectId == project.Id && item.UserId == User.Id) != null)
                        {
                            orders.Add(order);
                            break;
                        }
                    }
                }

                return orders;
            }
        }


    }
}
