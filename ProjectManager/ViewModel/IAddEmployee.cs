using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMDataLayer;
using PMView.View.WrapperVM;
using System.Collections.ObjectModel;

namespace PMView.View
{
    public interface IAddEmployee : ILoadDataSender
    {
        string Country { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Name { get; set; }
        List<string> SelectedSkills { get; set; }
        string Skype { get; set; }
        User.States State { get; set; }
        User.Statuses Status { get; set; }
        string Surname { get; set; }
        UserVM SelectedEmployeeToDelete { get; set; }
        ObservableCollection<string> EmployeesPositions { get; }
        bool SavePositionButton { get; set; }
        bool SaveButton { get; set; }

        void OnPropertyChanged(string v);
        void RemoveButtonClick(UserVM select);
        void ActivateButtons(UserVM _selectedEmployeeToAdd);
        void SaveButtonClick();
        void SavePositionsClick(List<string> list);
        void AddButtonClick(UserVM _selectedEmployeeToAdd);
    }
}
