using FragranceProject.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FragranceProject.Data
{
    public class FragranceDbContext : IdentityDbContext
    {
        public FragranceDbContext(DbContextOptions<FragranceDbContext> options)
            : base(options)
        {
        }
        public DbSet<Fragrance> Fragrances { get; init; }
        public DbSet<Category> Categories { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Fragrance>()
                .HasOne(f => f.Category)
                .WithMany(f => f.Fragrances)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}