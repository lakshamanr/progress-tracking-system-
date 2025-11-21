namespace DailyPlanTracker.Models;

/// <summary>
/// Represents the planned activities for a single day
/// </summary>
public class DailyPlan
{
    public int Id { get; set; }

    /// <summary>
    /// Day number (1-368)
    /// </summary>
    public int DayNumber { get; set; }

    /// <summary>
    /// Original planned date
    /// </summary>
    public DateTime OriginalDate { get; set; }

    /// <summary>
    /// Current scheduled date (can be rescheduled)
    /// </summary>
    public DateTime CurrentDate { get; set; }

    /// <summary>
    /// Day title/description
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Morning activities (DSA practice)
    /// </summary>
    public string MorningActivity { get; set; } = string.Empty;

    /// <summary>
    /// Evening activities (Technology learning)
    /// </summary>
    public string EveningActivity { get; set; } = string.Empty;

    /// <summary>
    /// Night activities (Reading/Review)
    /// </summary>
    public string NightActivity { get; set; } = string.Empty;

    /// <summary>
    /// Focus area for the day (e.g., "Backend/DSA", "System Design")
    /// </summary>
    public string FocusArea { get; set; } = string.Empty;

    /// <summary>
    /// Interview prep topic
    /// </summary>
    public string InterviewPrepTopic { get; set; } = string.Empty;

    /// <summary>
    /// Week number within the plan
    /// </summary>
    public int WeekNumber { get; set; }

    /// <summary>
    /// Month number (1-12)
    /// </summary>
    public int MonthNumber { get; set; }

    /// <summary>
    /// Month name and theme
    /// </summary>
    public string MonthTheme { get; set; } = string.Empty;

    /// <summary>
    /// Associated progress record
    /// </summary>
    public DailyProgress? Progress { get; set; }
}
