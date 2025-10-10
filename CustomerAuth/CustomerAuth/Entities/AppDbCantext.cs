using Microsoft.EntityFrameworkCore;

namespace CustomerAuth.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Useraccount> User_Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Useraccount>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Useraccount>()
                .HasIndex(u => u.username)
                .IsUnique();
        }
    }
}
