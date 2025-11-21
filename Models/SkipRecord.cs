namespace DailyPlanTracker.Models;

/// <summary>
/// Records when a day is skipped and the reason
/// </summary>
public class SkipRecord
{
    public int Id { get; set; }

    /// <summary>
    /// Foreign key to DailyPlan
    /// </summary>
    public int DailyPlanId { get; set; }

    /// <summary>
    /// Reference to the plan
    /// </summary>
    public DailyPlan DailyPlan { get; set; } = null!;

    /// <summary>
    /// Date when the day was skipped
    /// </summary>
    public DateTime SkippedDate { get; set; }

    /// <summary>
    /// Reason for skipping
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Whether remaining days were auto-rescheduled
    /// </summary>
    public bool AutoRescheduled { get; set; }

    /// <summary>
    /// New date if this day was rescheduled
    /// </summary>
    public DateTime? RescheduledTo { get; set; }
}
