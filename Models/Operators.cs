namespace TourOperatorManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Operators
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Login { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(250)]
        public string Comment { get; set; }

        public byte? IsDenied { get; set; }
    }
}
