namespace ProjectManagerSite.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users_Teams
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public string Position { get; set; }

        public bool IsLeader { get; set; }
    }
}
