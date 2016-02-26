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

        private string _orderId;

        private string _parrentProjectId;

        public ProjectVM(IPrincipal user, string orderId, string projectId) : base(user)
        {
            _projectId = projectId;
            _orderId = orderId;
        }

        public Orders CurrentOrder
        {
            get { return Model.Orders.FirstOrDefault(item => item.Id.ToString() == _orderId); }
        }

        public Projects CurrentProject
        {
            get
            {
                return Model.Projects.FirstOrDefault(item => item.Id.ToString() == _projectId && item.Order_Id.ToString() == _orderId);
            }
        }

        public Projects ParrentProject
        {
            get
            {
                var pp = Model.Project_Projects.ToList().FirstOrDefault(item => item.ChildProject_Id.ToString() == _projectId);
                if (pp.ParrentProject_Id != null)
                {
                    return Model.Projects.FirstOrDefault(item => item.Id == pp.ParrentProject_Id);
                }
                else return null;
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
