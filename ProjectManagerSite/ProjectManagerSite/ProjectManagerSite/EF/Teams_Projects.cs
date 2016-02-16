namespace ProjectManagerSite.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Teams_Projects
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        public Guid ProjectId { get; set; }
    }
}
