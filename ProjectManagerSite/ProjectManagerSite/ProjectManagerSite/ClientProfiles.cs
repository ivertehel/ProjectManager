namespace ProjectManagerSite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientProfiles
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
