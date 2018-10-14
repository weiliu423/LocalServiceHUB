namespace MVCHUB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string userName { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        [StringLength(200)]
        public string resourceKey { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expirationDate { get; set; }

        [StringLength(50)]
        public string fullName { get; set; }

        [StringLength(50)]
        public string firstName { get; set; }

        [StringLength(50)]
        public string lastName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }
    }
}
