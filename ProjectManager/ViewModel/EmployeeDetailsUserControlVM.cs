using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using PMDataLayer;
using PMView.View.WrapperVM;
using Core;

namespace PMView.View
{
    public class EmployeeDetailsUserControlVM : ILoadDataSender
    {
        private UserVM _currentEmployee;

        private ILoadDataSender _lastScreen;

        public EmployeeDetailsUserControlVM(IEmployee employee, ILoadDataSender lastScreen)
        {
            _currentEmployee = employee as UserVM;
            _lastScreen = lastScreen;
        }

        public void LoadData(object sender)
        {
            _lastScreen.LoadData(this);
        }

        public List<string> GetSkills(UserVM user)
        {
            var skills = Users_Skill.Items.Where(user_skill => user.Equals(user_skill.User) == true).ToList();
            var result = new List<string>();
            foreach (var item in skills)
            {
                result.Add(item.Skill.Name);
            }

            return result;
        }

        public void SaveChanges(List<string> newSkills)
        {
            Users_Skill.Items.RemoveAll(user_skill => user_skill.User.Id == _currentEmployee.User.Id);
            foreach (var item in newSkills)
            {
                var existSkill = Skill.Items.FirstOrDefault(skill => skill.Name == item);
                if (existSkill == null)
                {
                    existSkill = new Skill() { Name = item };
                }

                Users_Skill.Items.Add(new Users_Skill() { Skill = existSkill, User = _currentEmployee.User });
            }

            LoadData(this);
        }
    }
}
