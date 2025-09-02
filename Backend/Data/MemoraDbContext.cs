using Backend.Models.Entities;
using Backend.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Backend.Data;

public class MemoraDbContext(DbContextOptions<MemoraDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<VocabularyList> VocabularyLists { get; set; }
  public DbSet<Vocabulary> Vocabularies { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>()
        .HasMany(u => u.VocabularyLists)
        .WithOne(vl => vl.User)
        .HasForeignKey(vl => vl.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<VocabularyList>()
        .HasMany(vl => vl.Vocabularies)
        .WithOne(v => v.VocabularyList)
        .HasForeignKey(v => v.VocabularyListId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

    modelBuilder.Entity<User>()
        .HasIndex(u => u.Username)
        .IsUnique();
  }

  public class MemoraDbContextFactory : IDesignTimeDbContextFactory<MemoraDbContext>
  {
    public MemoraDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<MemoraDbContext>();
      optionsBuilder.UseSqlite(DatabaseConfig.ConnectionString);

      return new MemoraDbContext(optionsBuilder.Options);
    }
  }
}

