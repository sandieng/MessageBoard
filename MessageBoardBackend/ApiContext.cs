using Microsoft.EntityFrameworkCore;

namespace MessageBoardBackend
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Models.Message> Messages { get; set; }
        public DbSet<Models.User> Users { get; set; }
    }
}
