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
        public SkillsVM(IPrincipal user) : base(user)
        {

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
