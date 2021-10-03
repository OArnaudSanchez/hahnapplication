using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.July2021.Data.Data
{
    public class HahnProcessContext : DbContext
    {
        public HahnProcessContext(DbContextOptions<HahnProcessContext> options) : base(options) {}

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<UserAsset> UserAssets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.HasKey(x => x.Id);
                entity.Ignore(x => x.AssetName);
            });

            modelBuilder.Entity<Asset>(entity => {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<UserAsset>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.IdAsset)
                    .IsUnicode(false);
            });
        }
    }
}
