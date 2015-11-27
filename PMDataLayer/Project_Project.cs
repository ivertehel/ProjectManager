using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Project_Project : Base<Project_Project>
    {
        private Guid _parrentId;
        public Project ParrentProject
        {
            get
            {
                return Project.Items.Where(items => items.Id == _parrentId).FirstOrDefault();
            }
            set
            {
                _parrentId = value.Id;
            }
        }

        private Guid _childId;
        public Project ChildProject
        {
            get
            {
                return Project.Items.Where(items => items.Id == _childId).FirstOrDefault();
            }
            set
            {
                _childId = value.Id;
            }
        }
    }
}
