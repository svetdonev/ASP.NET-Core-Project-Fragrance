using FragranceProject.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FragranceProject.Data
{
    public class FragranceDbContext : IdentityDbContext<User>
    {
        public FragranceDbContext(DbContextOptions<FragranceDbContext> options)
            : base(options)
        {
        }
        public DbSet<Fragrance> Fragrances { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Review> Reviews { get; init; }
        public DbSet<UserReview> UserReviews { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Fragrance>()
                .HasOne(f => f.Category)
                .WithMany(f => f.Fragrances)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Review>()
                .HasOne(m => m.Author)
                .WithMany(m => m.Reviews)
                .HasForeignKey(m => m.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Review>()
                .HasOne(m => m.Fragrance)
                .WithMany(m => m.Reviews)
                .HasForeignKey(m => m.FragranceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserReview>()
                .HasKey(uc => new { uc.UserId, uc.ReviewId });

            builder
                .Entity<UserReview>()
                .HasOne(uc => uc.User)
                .WithMany(uc => uc.UserReviews)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<UserReview>()
                .HasOne(uc => uc.Review)
                .WithMany(uc => uc.UserReviews)
                .HasForeignKey(uc => uc.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}