using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFashion.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<UserMaster> UserMaster { get; set; }
        public DbSet<ProductMaster> ProductMaster { get; set; }
        public DbSet<SubCategoryMaster> SubCategoryMaster { get; set; }
        public DbSet<WishlistDetails> WishlistDetails { get; set; }
        public DbSet<ImageGallery> ImageGallery { get; set; }
        public DbSet<CategoryMaster> CategoryMaster { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
    }
}
