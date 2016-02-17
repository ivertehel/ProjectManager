using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagerSite.EF;
using System.Security.Principal;

namespace ProjectManagerSite.Models
{
    public class SkillsVM
    {
        private Entities _model;
        private Users _user;

        public SkillsVM(Entities model, Users user)
        {
            _model = model;
            _user = user;
        }

        public List<string> AllSkills
        {
            get
            {
                
                return (from items in _model.Skills select items.Name).ToList();
            }
        }

        //public List<string> UserSkills
        //{
        //    get
        //    {
        //        var allSkills = AllSkills;
               
        //    }
        //}

        
    }
}
