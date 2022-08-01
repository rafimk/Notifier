using Membership.Notifier.DAL.Models;
using Membership.Shared.Deduplication;
using Microsoft.EntityFrameworkCore;

namespace Membership.Notifier.DAL;

public class NotifyDbContext : DbContext
{
    public DbSet<UserCreatedModel> UserCreated { get; set; }
    public DbSet<DeduplicationModel> Deduplications { get; set; }
    
    public NotifyDbContext(DbContextOptions<NotifyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}