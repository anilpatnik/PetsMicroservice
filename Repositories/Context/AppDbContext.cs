using Microsoft.EntityFrameworkCore;
using PetsMicroservice.Models;

namespace PetsMicroservice.Repositories.Context
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pet>().ToTable("Pets");
            modelBuilder.Entity<Pet>().HasData(MockDatabase.Pets);
        }
    }
}
