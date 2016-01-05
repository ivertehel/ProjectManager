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
    public class EmployeeDetailsVM
    {
        private UserVM _currentEmployee;

        private ILoadData _lastScreen;

        public EmployeeDetailsVM(IEmployee employee, ILoadData lastScreen)
        {
            _currentEmployee = employee as UserVM;
            _lastScreen = lastScreen;
        }




    }
}
