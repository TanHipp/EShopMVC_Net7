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
        public DbSet<AppOrder> AppOrders { get; set; }


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
            /*
                  Fluent API
             */

            // Bảng AppCategogyes
            modelBuilder.Entity<AppCategory>()
                        .Property(c => c.Name)
                        .HasMaxLength(200);
            modelBuilder.Entity<AppCategory>()
                        .Property(c => c.Slug)
                        .HasMaxLength(200);

            // Bảng Product
            modelBuilder.Entity<AppProduct>()
                        .Property(m => m.Name)
                        .HasMaxLength(200);
            modelBuilder.Entity<AppProduct>()
                        .Property(m => m.Slug)
                        .HasMaxLength(200);
            modelBuilder.Entity<AppProduct>()
                        .Property(m => m.CoverImg)
                        .HasMaxLength(300);
            modelBuilder.Entity<AppProduct>()
                        .Property(m => m.Summary)
                        .HasMaxLength(1000);
            //Cấu hình khóa ngoại
            modelBuilder.Entity<AppProduct>()
                        .HasOne(m => m.Category) // Bảng Category
                        .WithMany(m => m.Products) // Products
                        .HasForeignKey(m => m.CategoryId)  // Khóa ngoại
                        .OnDelete(DeleteBehavior.NoAction); // => Xóa danh mục thì không xóa sản phẩm thuộc danh mục đó

            // Bảng ProductImg
            modelBuilder.Entity<AppProductImage>()
                        .Property(m => m.Path)
                        .HasMaxLength(300);
            //Cấu hình khóa ngoại
            modelBuilder.Entity<AppProductImage>()
                        .HasOne(m => m.Product) // Bảng ProductImg
                        .WithMany(m => m.ProductImages) // ProductImg
                        .HasForeignKey(m => m.ProductId); //Khóa ngoại
                        
            //Bảng AppOders
            modelBuilder.Entity<AppOrder>()
                        .Property(m => m.CustomerAddress)
                        .HasMaxLength (500);
            modelBuilder.Entity<AppOrder>()
                        .Property(m => m.CustomerPhone)
                        .HasMaxLength(20);
            modelBuilder.Entity<AppOrder>()
                        .Property(m => m.CustomerName)
                        .HasMaxLength(100);
            modelBuilder.Entity<AppOrder>()
                        .Property(m => m.CustomerEmail)
                        .HasMaxLength(100);
            //Cấu hình khóa ngoại bảng AppOrders
            modelBuilder.Entity <AppOrder>()
                        .HasMany(m => m.Details)
                        .WithOne(m => m.AppOrder)
                        .HasForeignKey(m => m.OrderId);




        }
    }
}
