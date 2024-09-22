using Microsoft.EntityFrameworkCore;

namespace StoragesApi.Db
{
    //"Host=localhost;Username=postgres;Password=example;Database=MyMarketStorages"
    public partial class AppDbContext:DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        private string? _connectionString;
        public AppDbContext()
        {
            
        }
        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder .UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("storage_pkey");
                entity.HasIndex(e => e.Id).IsUnique();

                entity.ToTable("storages");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
