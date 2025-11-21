namespace DailyPlanTracker.Models;

/// <summary>
/// Tracks the actual progress for a specific day
/// </summary>
public class DailyProgress
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
    /// Date when progress was recorded
    /// </summary>
    public DateTime CompletedDate { get; set; }

    /// <summary>
    /// Whether morning activities were completed
    /// </summary>
    public bool MorningCompleted { get; set; }

    /// <summary>
    /// Whether evening activities were completed
    /// </summary>
    public bool EveningCompleted { get; set; }

    /// <summary>
    /// Whether night activities were completed
    /// </summary>
    public bool NightCompleted { get; set; }

    /// <summary>
    /// Number of DSA problems solved
    /// </summary>
    public int DsaProblemsCompleted { get; set; }

    /// <summary>
    /// Total hours spent on the day's activities
    /// </summary>
    public double HoursSpent { get; set; }

    /// <summary>
    /// Energy level (1-5 scale)
    /// </summary>
    public int EnergyLevel { get; set; }

    /// <summary>
    /// Confidence level (1-5 scale)
    /// </summary>
    public int ConfidenceLevel { get; set; }

    /// <summary>
    /// Additional notes for the day
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Overall completion status
    /// </summary>
    public bool IsFullyCompleted => MorningCompleted && EveningCompleted && NightCompleted;

    /// <summary>
    /// Partial completion percentage
    /// </summary>
    public int CompletionPercentage
    {
        get
        {
            int completed = 0;
            if (MorningCompleted) completed++;
            if (EveningCompleted) completed++;
            if (NightCompleted) completed++;
            return (int)((completed / 3.0) * 100);
        }
    }
}
