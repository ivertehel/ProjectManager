namespace ProjectManagerSite.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users_Skills
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid SkillId { get; set; }
    }
}
