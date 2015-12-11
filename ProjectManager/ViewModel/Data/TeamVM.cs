using PMDataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMView.View.WrapperVM
{
    public class TeamVM : BaseVM
    {
        private Team _team;

        private string _name;

        private string _description;

        public TeamVM(Team team)
        {
            _team = team;
        } 

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
    }
}
