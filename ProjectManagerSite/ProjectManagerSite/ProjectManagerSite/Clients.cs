namespace ProjectManagerSite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        public Guid Id { get; set; }

        public decimal Account { get; set; }

        public Guid User_Id { get; set; }
    }
}
