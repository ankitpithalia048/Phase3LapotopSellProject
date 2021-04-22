using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataLayer
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
      
        public DbSet<LaptopModel> LaptopModel { get; set; }
        public DbSet<OrderDetails> DetailsModel { get; set; }
    }
}
