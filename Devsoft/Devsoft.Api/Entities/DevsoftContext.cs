using Microsoft.EntityFrameworkCore;

namespace Devsoft.Api.Entities
{
    public class DevsoftContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<FoodGroup> FoodGroups { get; set; }
        public DbSet<Publication> Publications { get; set; }

        public DevsoftContext(DbContextOptions<DevsoftContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publication>().HasKey(p => p.Id);
            modelBuilder.Entity<Publication>()
                .HasOne<User>(p => p.User)
                .WithMany(u => u.Publications)
                .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Publication>()
                .HasOne<Food>(p => p.Food)
                .WithMany(u => u.Publications)
                .HasForeignKey(p => p.FoodId);
        }
    }
}