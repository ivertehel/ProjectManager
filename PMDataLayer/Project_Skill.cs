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
        public Project Project
        {
            get
            {
                return (from items in Project.Items where items.Id == _projectId select items).FirstOrDefault();
            }
            set
            {
                _projectId = value.Id;
            }
        }

        private Guid _skillId;
        public Skill Skill
        {
            get
            {
                return (from items in Skill.Items where items.Id == _skillId select items).FirstOrDefault();
            }
            set
            {
                _skillId = value.Id;
            }
        }
    }
}
