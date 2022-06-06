namespace TourOperatorManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        public Clients()
        {
            Track = new HashSet<Track>();
        }
        public int ID { get; set; }

        public string FullName { get; set; }

        [StringLength(4)]
        public string PassportS { get; set; }

        [StringLength(6)]
        public string PassportN { get; set; }

        public string Whom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TakedDay { get; set; }

        [StringLength(7)]
        public string CodeUnit { get; set; }

        [StringLength(13)]
        public string BirthCertificateNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BDay { get; set; }

        public string Address { get; set; }

        [StringLength(7)]
        public string Gender { get; set; }

        [StringLength(17)]
        public string Msisdn { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public byte? IsExists { get; set; }

        public virtual ICollection<Track> Track { get; set; }
    }
}
