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
        private static List<PositionVM> _positions = new List<PositionVM>();

        private Position _position;

        public PositionVM()
        {
        }

        public PositionVM(Position position)
        {
            _position = position;
        }

        public static List<PositionVM> Positions
        {
            get
            {
                _positions.Clear();
                foreach (var item in Position.Items)
                {
                    _positions.Add(new PositionVM(item));
                }

                return _positions;
            }
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

        public IEnumerable<Users_Team> UsersInTeams
        {
            get { return _position.UsersInTeams; }
        }

        public IEnumerable<Users_Project> UsersInProjects
        {
            get { return _position.UsersInProjects; }
        }

        public override string ToString()
        {
            return Name;
        }

        public PositionVM Clone()
        {
            return new PositionVM(new Position() { Name = Name, Id = Position.Id });
        }
    }
}
