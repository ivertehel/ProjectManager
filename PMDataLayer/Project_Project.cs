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
                return (from items in Project.Items where items.Id == _parrentId select items).FirstOrDefault();
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
                return (from items in Project.Items where items.Id == _childId select items).FirstOrDefault();
            }
            set
            {
                _childId = value.Id;
            }
        }
    }
}
