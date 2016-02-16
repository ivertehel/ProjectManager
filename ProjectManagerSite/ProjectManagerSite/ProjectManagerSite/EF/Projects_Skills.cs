namespace ProjectManagerSite.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Projects_Skills
    {
        public Guid Id { get; set; }

        public Guid SkillId { get; set; }

        public Guid ProjectId { get; set; }
    }
}
