using Microsoft.EntityFrameworkCore;
using Moja_Aplikacija.Entity;

namespace Moja_Aplikacija.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Book { get; set; }
        public  DbSet<Genre> Genre { get; set; }
        public  DbSet<Writer> Writer { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>();
            modelBuilder.Entity<Genre>();
            modelBuilder.Entity<Writer>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<UserRole>();
            modelBuilder.Entity<Role>();

            modelBuilder.Entity<Genre>()
                .HasMany(p => p.Books)
                .WithOne(p => p.Genre);

            modelBuilder.Entity<Writer>()
                .HasMany(p => p.Books)
                .WithOne(p => p.Writer);

            modelBuilder.Entity<UserRole>()
                .HasKey(p => new { p.UserId, p.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(p => p.User)
                .WithMany(p => p.UserRole)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(p => p.Role)
                .WithMany(p => p.UserRole)
                .HasForeignKey(p => p.RoleId);
                
                

        }
    }
    
}
