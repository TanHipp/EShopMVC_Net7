using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EShopMVC_Net7.Models;

namespace EShopMVC_Net7.Models
{
    public class EShopDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppProduct> AppProducts { get; set; }
        public DbSet<AppOrderDetail> AppOrderDetails { get; set; }
        public DbSet<AppCategory> AppCategorys { get; set; }
        public DbSet<AppProductImage> AppProductImages { get; set; }
        public DbSet<AppOrderProduct> AppOrders { get; set; }


        public EShopDbContext(DbContextOptions options) : base(options)
        {

        }



        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {

          var connection = "Server=TANHIPP\\SQLEXPRESS;Database=RazorPage;Trusted_Connection=True;Encrypt=False";
          optionsBuilder.UseSqlServer(connection);
       }*/


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppCategory>()
                        .Property(c => c.Name)
                        .HasMaxLength(200);
            modelBuilder.Entity<AppCategory>()
                        .Property(c => c.Slug)
                        .HasMaxLength(200);
        }
    }
}
