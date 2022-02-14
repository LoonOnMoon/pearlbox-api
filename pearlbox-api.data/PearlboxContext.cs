using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pearlbox_api.data.DatabaseObjects.Authentication;

namespace pearlbox_api.data
{
    public class PearlboxContext : IdentityDbContext<IdentityUser>
    {
        public PearlboxContext(DbContextOptions<PearlboxContext> options) : base(options) { }
        //public DbSet<User> Users { get; set; } = null!;

    }
}