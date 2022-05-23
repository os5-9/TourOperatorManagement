namespace TourOperatorManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            Track = new HashSet<Track>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string FullName { get; set; }

        [StringLength(4)]
        public string PassportS { get; set; }

        [StringLength(6)]
        public string PassportN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BDay { get; set; }

        public string Address { get; set; }

        [StringLength(7)]
        public string Gender { get; set; }

        [StringLength(16)]
        public string Msisdn { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public byte? IsExists { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Track> Track { get; set; }
    }
}
