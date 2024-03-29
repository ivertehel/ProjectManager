﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    [Table]
    public class Users_Team : Entity<Users_Team>
    {
        private Guid _teamId;

        private Guid _userId;

        private Guid _positionId;

        [Column]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column]
        public Guid TeamId
        {
            get { return _teamId; }
            set { _teamId = value; }
        }

        [Column]
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        [Column]
        public Guid PositionId
        {
            get { return _positionId; }
            set { _positionId = value; }
        }

        [Column]
        public bool IsLeader { get; set; }

        public Team Team
        {
            get { return Team.Items.Where(items => items.Id == _teamId).FirstOrDefault(); }
            set { _teamId = value.Id; }
        }

        public User User
        {
            get { return User.Items.Where(items => items.Id == _userId).FirstOrDefault(); }
            set { _userId = value.Id; }
        }

        public Position Position
        {
            get { return Position.Items.Where(items => items.Id == _positionId).FirstOrDefault(); }
            set { _positionId = value.Id; }
        }

        public IEnumerable<Position> Positions
        {
            get { return from items in Users_Team.Items where items._userId == _userId select items.Position; }
        }

        public override string ToString()
        {
            return User.ToString();
        }

        public override bool Equals(object obj)
        {
            var item = obj as Users_Team;
            if (item == null)
                return false;
            if (item.Id == Id)
                return true;
            return false;
        }
    }
}
