using FinanceTracker.DAL.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DAL.EfCore.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(p => p.RegistratedDate)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Account>()
            .Property(p => p.CreatedDate)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Transaction>()
           .Property(p => p.Timestamp)
           .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Account>()
            .HasOne(e => e.User)
            .WithMany(e => e.Accounts)
            .HasForeignKey(e => e.Id);

        modelBuilder.Entity<Transaction>()
            .HasOne(e => e.Account)
            .WithMany(e => e.Transactions)
            .HasForeignKey(e => e.Id);
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
