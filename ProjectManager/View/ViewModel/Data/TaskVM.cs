using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PMDataLayer;

namespace PMView.View.WrapperVM
{
    public class TaskVM : BaseVM
    {
        private Task _task;

        public TaskVM(Task task)
        {
            _task = task;
        }

        public Task Task
        {
            get { return _task; }
            set { _task = value; }
        }

        public string Name
        {
            get { return _task.Name; }
            set { _task.Name = value; }
        }

        public string Description
        {
            get { return _task.Description; }
            set { _task.Description = value; }
        }

        public string Status
        {
            get { return _task.Status; }
            set { _task.Status = value; }
        }

        public int Priority
        {
            get { return _task.Priority; }
            set { _task.Priority = value; }
        }

        public int Hours
        {
            get { return _task.Hours; }
            set { _task.Hours = value; }
        }

        public Project Project
        {
            get { return _task.Project; }
            set { _task.Project = value; }
        }

        public Task.Owners Owner
        {
            get { return _task.Owner; }
            set { _task.Owner = value; }
        }
    }
}
