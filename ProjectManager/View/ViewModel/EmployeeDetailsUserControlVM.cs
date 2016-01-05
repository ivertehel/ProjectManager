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
    public class EmployeeDetailsUserControlVM : ILoadData
    {
        private UserVM _currentEmployee;

        private ILoadData _lastScreen;

        public EmployeeDetailsUserControlVM(IEmployee employee, ILoadData lastScreen)
        {
            _currentEmployee = employee as UserVM;
            _lastScreen = lastScreen;
        }

        public void LoadData()
        {
            _lastScreen.LoadData();
        }
    }
}
