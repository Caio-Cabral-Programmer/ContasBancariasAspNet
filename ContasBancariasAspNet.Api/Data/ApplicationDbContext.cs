using ContasBancariasAspNet.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ContasBancariasAspNet.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Account configurations
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasIndex(e => e.Number).IsUnique();
        });

        // Card configurations
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasIndex(e => e.Number).IsUnique();
        });

        // User configurations
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(u => u.Account)
                  .WithOne(a => a.User)
                  .HasForeignKey<User>(u => u.AccountId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(u => u.Card)
                  .WithOne(c => c.User)
                  .HasForeignKey<User>(u => u.CardId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}