using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TourOperatorManagement.Models
{
    public partial class AgencyModel : DbContext
    {
        public AgencyModel()
            : base("name=AModel")
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Operators> Operators { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Tours> Tours { get; set; }
        public virtual DbSet<TourStates> TourStates { get; set; }
        public virtual DbSet<TourType> TourType { get; set; }
        public virtual DbSet<Track> Track { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>()
                .HasMany(e => e.Track)
                .WithOptional(e => e.Clients)
                .HasForeignKey(e => e.ClientID);

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
