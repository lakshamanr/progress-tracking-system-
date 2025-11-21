using Microsoft.EntityFrameworkCore;
using DailyPlanTracker.Models;

namespace DailyPlanTracker.Data;

/// <summary>
/// Database context for the Daily Plan Tracker
/// </summary>
public class TrackerDbContext : DbContext
{
    public DbSet<DailyPlan> DailyPlans { get; set; } = null!;
    public DbSet<DailyProgress> DailyProgress { get; set; } = null!;
    public DbSet<SkipRecord> SkipRecords { get; set; } = null!;
    public DbSet<WeeklySummary> WeeklySummaries { get; set; } = null!;

    public TrackerDbContext()
    {
    }

    public TrackerDbContext(DbContextOptions<TrackerDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Use SQLite database in the current directory
            optionsBuilder.UseSqlite("Data Source=tracker.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure DailyPlan
        modelBuilder.Entity<DailyPlan>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.DayNumber).IsUnique();
            entity.HasIndex(e => e.CurrentDate);
            entity.Property(e => e.Title).IsRequired();

            // One-to-one relationship with DailyProgress
            entity.HasOne(e => e.Progress)
                .WithOne(p => p.DailyPlan)
                .HasForeignKey<DailyProgress>(p => p.DailyPlanId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure DailyProgress
        modelBuilder.Entity<DailyProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.DailyPlanId).IsUnique();
            entity.Property(e => e.CompletedDate).IsRequired();
        });

        // Configure SkipRecord
        modelBuilder.Entity<SkipRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.DailyPlanId);
            entity.Property(e => e.SkippedDate).IsRequired();
            entity.Property(e => e.Reason).IsRequired();
        });

        // Configure WeeklySummary
        modelBuilder.Entity<WeeklySummary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.WeekNumber);
            entity.Property(e => e.WeekStartDate).IsRequired();
            entity.Property(e => e.WeekEndDate).IsRequired();
        });
    }
}
