using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class Project : Base<Project>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string StartDate { get; set; }
        public string ReleaseDate { get; set; }

        private Guid _projectStatusId;
        public ProjectStatus Status
        {
            get
            {
                return (from items in ProjectStatus.Items where items.Id == _projectStatusId select items).FirstOrDefault();
            }
            set
            {
                _projectStatusId = value.Id;
            }
        }

        private Guid _clientId;
        public Client Client
        {
            get
            {
                return (Client)(from items in Client.Items where items.Id == _clientId select items).FirstOrDefault();
            }
            set
            {
                _clientId = value.Id;
            }
        }
        public IEnumerable<Comment> Comments
        {
            get
            {
                return (from items in Comment.Items where items.Project.Id == Id select items);
            }
        }
    }
}
