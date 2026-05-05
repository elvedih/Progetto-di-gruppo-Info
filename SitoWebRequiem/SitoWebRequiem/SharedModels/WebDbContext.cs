using Microsoft.EntityFrameworkCore;

namespace SitoWebRequiem.SharedModels
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
    }
}
