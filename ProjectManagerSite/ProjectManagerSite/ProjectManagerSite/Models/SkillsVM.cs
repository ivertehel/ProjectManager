using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagerSite.EF;
using System.Security.Principal;

namespace ProjectManagerSite.Models
{
    public class SkillsVM : BaseVM
    {
        public class CheckBox
        {
            public string Name { get; set; }
            public bool Enabled { get; set; }
        }

        public List<CheckBox> CheckBoxes { get; set; } = new List<CheckBox>();

        public SkillsVM() : base(null)
        {

        }

        public SkillsVM(IPrincipal user) : base(user)
        {
            foreach (var item in AllSkills)
            {
                CheckBoxes.Add(new CheckBox() { Name = item, Enabled = UserSkills.FirstOrDefault(skill => skill == item) != null ? true : false });
            }
        }

        public List<string> AllSkills
        {
            get
            {
                
                return (from items in Model.Skills select items.Name).ToList();
            }
        }


        public List<string> UserSkills
        {
            get
            {
                var userSkills = Model.Users_Skills.ToList().FindAll(item => item.UserId == User.Id);
                List<string> skills = new List<string>();
                foreach (var item in userSkills)
                {
                    var s = Model.Skills.FirstOrDefault(skill => skill.Id == item.SkillId);
                    if (s != null)
                        skills.Add(s.Name);
                }

                return skills;
            }
        }


    }
}
