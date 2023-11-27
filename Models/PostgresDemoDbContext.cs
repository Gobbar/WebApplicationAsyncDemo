using Microsoft.EntityFrameworkCore;

namespace WebApplicationAsyncDemo.Models
{
    public class PostgresDemoDbContext : DbContext
    {
        public PostgresDemoDbContext(DbContextOptions<PostgresDemoDbContext> options) : base(options) { }

        public virtual DbSet<PostgresDemo> PostgresDemos { get; set; }
    }
}
