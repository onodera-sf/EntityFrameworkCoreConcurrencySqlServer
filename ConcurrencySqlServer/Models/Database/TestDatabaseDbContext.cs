using Microsoft.EntityFrameworkCore;

namespace ConcurrencySqlServer.Models.Database;

public partial class TestDatabaseDbContext : DbContext
{
  public TestDatabaseDbContext()
  {
  }

  public TestDatabaseDbContext(DbContextOptions<TestDatabaseDbContext> options)
      : base(options)
  {
  }

  public virtual DbSet<Book> Book { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
  => optionsBuilder.UseSqlServer("Data Source=<サーバー名>;Database=<データベース名>;user id=<ユーザー名>;password=<パスワード>;TrustServerCertificate=true");

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Book>(entity =>
    {
      entity.Property(e => e.ID).ValueGeneratedNever();
      entity.Property(e => e.RowVersion)
              .IsRowVersion()
              .IsConcurrencyToken();
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
