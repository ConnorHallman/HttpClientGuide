using HttpClientGuide.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HttpClientGuide.Server.Storage
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
