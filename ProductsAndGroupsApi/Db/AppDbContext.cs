using Microsoft.EntityFrameworkCore;


//"Host=localhost;Username=postgres;Password=example;Database=MyMarketProductsAndGroups"
namespace ProductsAndGroupsApi.Db
{
    public partial class AppDbContext:DbContext
    {
        public DbSet<Product> Products {  get; set; }
        public DbSet<ProductGroup> ProductGroups {  get; set; }
        private string? _connectionString;
        public AppDbContext()
        {
            
        }

        public AppDbContext(string? connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("product_pkey");
                entity.HasIndex(e => e.Id).IsUnique();
                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e=>e.Name).HasColumnName("name");
                entity.Property(e=>e.Description).HasColumnName("description");

                entity.HasOne(e => e.ProductGroup).WithMany(p => p.Products).HasForeignKey(p => p.ProductGroupId);
            });

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("productGroup_pkey");
                entity.HasIndex(e => e.Id).IsUnique();
                entity.ToTable("productGroups");

                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e=>e.Description).HasColumnName(@"description");
            });
        }

    }
}
