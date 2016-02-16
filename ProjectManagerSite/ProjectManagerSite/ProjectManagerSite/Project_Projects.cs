namespace ProjectManagerSite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project_Projects
    {
        public Guid Id { get; set; }

        public Guid? ParrentProject_Id { get; set; }

        public Guid ChildProject_Id { get; set; }
    }
}
