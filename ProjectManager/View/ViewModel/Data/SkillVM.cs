using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMDataLayer;

namespace PMView.View.WrapperVM
{
    public class SkillVM : BaseVM
    {
        private Skill _skill;

        private static List<SkillVM> _skills = new List<SkillVM>();

        public SkillVM()
        {

        }

        public SkillVM(Skill skill)
        {
            _skill = skill;
        }

        public Skill Skill
        {
            get { return _skill; }
            set { _skill = value; }
        }
       
        public static List<SkillVM> Skills
        {
            get
            {
                _skills.Clear();
                foreach (var item in Skill.Items)
                {
                    _skills.Add(new SkillVM(item));
                }

                return _skills; 
            }
        }

        public string Name
        {
            get { return _skill.Name; }
            set { _skill.Name = value; }
        }

        public IEnumerable<User> Users
        {
            get { return _skill.Users; }
        }

        public IEnumerable<Project> Projects
        {
            get { return _skill.Projects; }
        }

        public override string ToString()
        {
            return Name;
        }

        public SkillVM Clone()
        {
            return new SkillVM(new Skill() { Name = Name, Id = Skill.Id });
        }
    }
}
