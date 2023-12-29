using FragranceProject.Data;
using FragranceProject.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FragranceProject.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<FragranceDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(FragranceDbContext data)
        {
            if(data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "Men's Perfumes"},
                new Category { Name = "Women's Perfumes"},
                new Category { Name = "Unisex Perfumes"},
            });

            data.SaveChanges();
        }
    }
}
