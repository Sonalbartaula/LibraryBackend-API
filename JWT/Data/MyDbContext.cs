using JWT.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {}
        public DbSet<User> Users { get; set; } 
    }
}
