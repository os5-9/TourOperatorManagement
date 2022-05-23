using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TourOperatorManagement.Models
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=AgencyModel")
        {
        }

        public virtual DbSet<Operators> Operators { get; set; }
        public virtual DbSet<Tours> Tours { get; set; }
        public virtual DbSet<TourStates> TourStates { get; set; }
        public virtual DbSet<TourType> TourType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tours>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Tours>()
                .HasMany(e => e.Track)
                .WithOptional(e => e.Tours)
                .HasForeignKey(e => e.TourID);

            modelBuilder.Entity<TourStates>()
                .HasMany(e => e.Tours)
                .WithOptional(e => e.TourStates)
                .HasForeignKey(e => e.State);

            modelBuilder.Entity<TourType>()
                .HasMany(e => e.Tours)
                .WithOptional(e => e.TourType)
                .HasForeignKey(e => e.Type);
        }
    }
}
