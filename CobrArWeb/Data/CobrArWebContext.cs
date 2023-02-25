using Microsoft.EntityFrameworkCore;

namespace CobrArWeb.Data
{
    public class CobrArWebContext : DbContext
    {
        public CobrArWebContext(DbContextOptions<CobrArWebContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
