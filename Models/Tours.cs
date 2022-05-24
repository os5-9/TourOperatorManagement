namespace TourOperatorManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tours()
        {
            Track = new HashSet<Track>();
        }
        public int ID { get; set; }

        [StringLength(45)]
        public string City { get; set; }

        [StringLength(45)]
        public string Country { get; set; }

        public int? Price { get; set; }

        public int? State { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Departure { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Arrival { get; set; }

        public int? Type { get; set; }

        public int? Tickets { get; set; }

        public byte? IsExists { get; set; }

        public byte? IsApproved { get; set; }

        public virtual TourStates TourStates { get; set; }

        public virtual TourType TourType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Track> Track { get; set; }
    }
}
