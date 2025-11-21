namespace DailyPlanTracker.Models;

/// <summary>
/// Stores weekly summary information
/// </summary>
public class WeeklySummary
{
    public int Id { get; set; }

    /// <summary>
    /// Week number (1-52+)
    /// </summary>
    public int WeekNumber { get; set; }

    /// <summary>
    /// Start date of the week
    /// </summary>
    public DateTime WeekStartDate { get; set; }

    /// <summary>
    /// End date of the week
    /// </summary>
    public DateTime WeekEndDate { get; set; }

    /// <summary>
    /// Number of days completed this week
    /// </summary>
    public int DaysCompleted { get; set; }

    /// <summary>
    /// Total DSA problems solved this week
    /// </summary>
    public int TotalDsaProblems { get; set; }

    /// <summary>
    /// Total hours spent this week
    /// </summary>
    public double TotalHours { get; set; }

    /// <summary>
    /// Average energy level for the week
    /// </summary>
    public double AverageEnergyLevel { get; set; }

    /// <summary>
    /// Average confidence level for the week
    /// </summary>
    public double AverageConfidenceLevel { get; set; }

    /// <summary>
    /// Key learnings from the week
    /// </summary>
    public string KeyLearnings { get; set; } = string.Empty;

    /// <summary>
    /// Areas to improve
    /// </summary>
    public string AreasToImprove { get; set; } = string.Empty;

    /// <summary>
    /// Additional notes for the week
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Date when the summary was created
    /// </summary>
    public DateTime CreatedDate { get; set; }
}
