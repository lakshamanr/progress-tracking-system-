using Microsoft.EntityFrameworkCore;

namespace DailyPlanTracker.Data;

/// <summary>
/// Handles database initialization and migrations
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Initialize the database and apply migrations
    /// </summary>
    public static void Initialize()
    {
        using var context = new TrackerDbContext();

        // Create database if it doesn't exist
        context.Database.EnsureCreated();
    }

    /// <summary>
    /// Reset the database (delete and recreate)
    /// </summary>
    public static void Reset()
    {
        using var context = new TrackerDbContext();

        // Delete and recreate the database
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}
