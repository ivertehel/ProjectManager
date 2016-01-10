using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Project_Project : Entity<Project_Project>
    {
        private Guid _parrentId;

        private Guid _childId;

        public Project ParrentProject
        {
            get { return Project.Items.Where(items => items.Id == _parrentId).FirstOrDefault(); }
            set
            {
                if (value != null)
                    _parrentId = value.Id;
            }
        }

        public Project ChildProject
        {
            get { return Project.Items.Where(items => items.Id == _childId).FirstOrDefault(); }
            set { _childId = value.Id; }
        }
    }
}
