using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Project_Skill : Base<Project_Skill>
    {
        private Guid _projectId;

        private Guid _skillId;

        public Project Project
        {
            get { return Project.Items.Where(items => items.Id == _projectId).FirstOrDefault(); }
            set { _projectId = value.Id; }
        }

        public Skill Skill
        {
            get { return Skill.Items.Where(items => items.Id == _skillId).FirstOrDefault(); }
            set { _skillId = value.Id; }
        }
    }
}
