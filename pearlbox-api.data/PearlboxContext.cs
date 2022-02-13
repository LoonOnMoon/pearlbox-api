using Microsoft.EntityFrameworkCore;
using pearlbox_api.data.Models.Authentication;

namespace pearlbox_api.data
{
    public class PearlboxContext : DbContext
    {
        public PearlboxContext(DbContextOptions<PearlboxContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!;

    }
}