using Microsoft.EntityFrameworkCore;

//"Host=localhost;Username=postgres;Password=example;Database=MyMarketPlacements"
namespace PlacementApi.Db
{
    public partial class AppDbContext:DbContext
    {
        public DbSet<Placement> Placements { get; set; }
        private string? _connectionString;
        public AppDbContext(){}

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Placement>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("placement_pkey");

                entity.ToTable("placements");

                entity.Property(e => e.ProductId).HasColumnName("productId");
                entity.HasIndex(e => e.ProductId).IsUnique();
                entity.Property(e => e.StorageId).HasColumnName("storageId");
            });
            OnModelCreatinfPartial(modelBuilder);
        }
        partial void OnModelCreatinfPartial(ModelBuilder modelBuilder);
    }
}
