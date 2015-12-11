using PMDataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMView.View.WrapperVM
{
    public class SkillVM : BaseVM
    {
        private Skill _skill;

        public SkillVM(Skill skill)
        {
            _skill = skill;
        }

        public Skill Skill
        {
            get { return _skill; }
            set { _skill = value; }
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
    }
}
