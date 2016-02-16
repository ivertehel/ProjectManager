namespace ProjectManagerSite.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Skype { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        public byte[] Image { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string State { get; set; }
    }
}
