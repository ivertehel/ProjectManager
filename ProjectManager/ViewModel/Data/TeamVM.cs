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
    public class TeamVM : BaseVM
    {
        private Team _team;

        public TeamVM(Team team)
        {
            _team = team;
        }

        public TeamVM()
        {

        }

        public Team Team
        {
            get { return _team; }
            set { _team = value; }
        }

        public string Name
        {
            get { return _team.Name; }
            set { _team.Name = value; }
        }

        public string Description
        {
            get { return _team.Description; }
            set { _team.Description = value; }
        }

        public IEnumerable<Project> Projects
        {
            get { return _team.Projects; }
        }

        public IEnumerable<Users_Team> Users
        {
            get { return from items in Users_Team.Items where items.Team.Id == _team.Id select items; }
        }

        public override string ToString()
        {
            return _team.ToString();
        }

        public IEnumerable<SkillVM> Skills
        {
            get
            {
                List<SkillVM> skills = new List<SkillVM>();
                foreach (var employee in User.Items)
                {
                    if (employee.RoleType == User.Roles.Employee)
                    {
                        foreach (var skill in employee.Skills)
                        {
                            if (skills.FirstOrDefault(item => item.Name == skill.Name) == null)
                            {
                                skills.Add(new SkillVM(skill));
                            }
                        }
                    }
                }

                return skills;
            }
        }
    }
}
