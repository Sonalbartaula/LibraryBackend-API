using Jwt.Model;
using JWT.Entities;
using JWT.Model;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data
{

    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books {get; set; }

        public DbSet<Student> students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Store enums as strings to match existing values
            modelBuilder.Entity<Student>()
                .Property(s => s.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Student>()
                .Property(s => s.Type)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Transaction> Transactions { get; set; }


    }

}
