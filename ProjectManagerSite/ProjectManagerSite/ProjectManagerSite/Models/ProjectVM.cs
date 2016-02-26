using ProjectManagerSite.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerSite.Models
{
    public class ProjectVM : BaseVM
    {
        private string _projectId;

        public ProjectVM(IPrincipal user, string projectId) : base(user)
        {
            _projectId = projectId;
        }

        public Projects CurrentProject
        {
            get
            {
                return Model.Projects.FirstOrDefault(item => item.Id.ToString() == _projectId);
            }
        }

        public List<Projects> ChildProjects
        {
            get
            {
                var project_projects = from items in Model.Project_Projects where items.ParrentProject_Id != null && items.ParrentProject_Id.ToString() == _projectId select items;
                return (from items in Model.Projects where project_projects.FirstOrDefault(p => p.ChildProject_Id == items.Id) != null select items).ToList();
            }
        }
    }
}
