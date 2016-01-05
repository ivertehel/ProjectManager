using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PMDataLayer;

namespace PMView.View.WrapperVM
{
    public class PositionVM : BaseVM
    {
        private Position _position;

        public PositionVM(Position position)
        {
            _position = position;
        }

        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public string Name
        {
            get { return _position.Name; }
            set { _position.Name = value; }
        }

        public string Description
        {
            get { return _position.Description; }
            set { _position.Description = value; }
        }

        public IEnumerable<User_Team> UsersInTeams
        {
            get { return _position.UsersInTeams; }
        }

        public IEnumerable<User_Project> UsersInProjects
        {
            get { return _position.UsersInProjects; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
