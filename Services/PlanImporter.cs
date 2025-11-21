using System.Text.RegularExpressions;
using DailyPlanTracker.Data;
using DailyPlanTracker.Models;

namespace DailyPlanTracker.Services;

/// <summary>
/// Service to import and parse the daily plan from markdown file
/// </summary>
public class PlanImporter
{
    private readonly TrackerDbContext _context;

    // Monthly themes mapping
    private readonly Dictionary<int, string> _monthlyThemes = new()
    {
        { 12, "Foundation refresh + DSA basics" },
        { 1, "Advanced .NET + React basics" },
        { 2, "Azure deep dive + System design basics" },
        { 3, "Microservices patterns + Kubernetes" },
        { 4, "Frontend mastery + NoSQL databases" },
        { 5, "System design intensive" },
        { 6, "Mock interviews + Portfolio projects" },
        { 7, "Design patterns + Architecture" },
        { 8, "Advanced Azure + DevOps" },
        { 9, "Interview simulation + Resume projects" },
        { 10, "Full-stack project + Open source" },
        { 11, "Final prep + Confidence building" }
    };

    public PlanImporter(TrackerDbContext context)
    {
        _context = context;
    }

    public PlanImporter() : this(new TrackerDbContext())
    {
    }

    /// <summary>
    /// Import the daily plan from a markdown file
    /// </summary>
    public async Task<int> ImportFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Plan file not found: {filePath}");
        }

        var content = await File.ReadAllTextAsync(filePath);
        return await ParseAndImportAsync(content);
    }

    /// <summary>
    /// Parse markdown content and import to database
    /// </summary>
    private async Task<int> ParseAndImportAsync(string content)
    {
        var lines = content.Split('\n');
        var dailyPlans = new List<DailyPlan>();

        DailyPlan? currentDay = null;
        var currentActivities = new List<string>();
        int weekNumber = 1;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // Match day header: **Day 1 (Sun, Dec 1)** - Planning & Setup
            var dayMatch = Regex.Match(trimmedLine, @"\*\*Day (\d+) \(([^)]+)\)\*\* - (.+)");
            if (dayMatch.Success)
            {
                // Save previous day if exists
                if (currentDay != null)
                {
                    AssignActivitiesToDay(currentDay, currentActivities);
                    dailyPlans.Add(currentDay);
                }

                // Create new day
                int dayNumber = int.Parse(dayMatch.Groups[1].Value);
                string dateStr = dayMatch.Groups[2].Value;
                string title = dayMatch.Groups[3].Value;

                var date = ParseDate(dateStr, dayNumber);

                currentDay = new DailyPlan
                {
                    DayNumber = dayNumber,
                    OriginalDate = date,
                    CurrentDate = date,
                    Title = title,
                    WeekNumber = weekNumber,
                    MonthNumber = date.Month,
                    MonthTheme = _monthlyThemes.GetValueOrDefault(date.Month, "")
                };

                currentActivities.Clear();
            }
            // Match activity bullets
            else if (trimmedLine.StartsWith("-") && currentDay != null)
            {
                var activity = trimmedLine.TrimStart('-').Trim();

                // Check for Focus and Interview Prep
                if (activity.StartsWith("**Focus**:"))
                {
                    currentDay.FocusArea = activity.Replace("**Focus**:", "").Trim();
                }
                else if (activity.StartsWith("**Interview Prep**:"))
                {
                    currentDay.InterviewPrepTopic = activity.Replace("**Interview Prep**:", "").Trim();
                }
                else
                {
                    currentActivities.Add(activity);
                }
            }
            // Detect week boundaries
            else if (trimmedLine.StartsWith("### Week"))
            {
                var weekMatch = Regex.Match(trimmedLine, @"Week (\d+)");
                if (weekMatch.Success)
                {
                    weekNumber = int.Parse(weekMatch.Groups[1].Value);
                }
            }
        }

        // Add the last day
        if (currentDay != null)
        {
            AssignActivitiesToDay(currentDay, currentActivities);
            dailyPlans.Add(currentDay);
        }

        // Save to database
        _context.DailyPlans.AddRange(dailyPlans);
        await _context.SaveChangesAsync();

        return dailyPlans.Count;
    }

    /// <summary>
    /// Assign activities to morning/evening/night based on typical pattern
    /// </summary>
    private void AssignActivitiesToDay(DailyPlan day, List<string> activities)
    {
        if (activities.Count == 0) return;

        // Heuristic:
        // - First activity or activities mentioning DSA/problems -> Morning
        // - Middle activities or main learning topics -> Evening
        // - Last activity or review/reading related -> Night

        var morningActivities = new List<string>();
        var eveningActivities = new List<string>();
        var nightActivities = new List<string>();

        foreach (var activity in activities)
        {
            var lowerActivity = activity.ToLower();

            if (lowerActivity.Contains("morning:"))
            {
                morningActivities.Add(activity.Replace("Morning:", "").Trim());
            }
            else if (lowerActivity.Contains("evening:"))
            {
                eveningActivities.Add(activity.Replace("Evening:", "").Trim());
            }
            else if (lowerActivity.Contains("night:"))
            {
                nightActivities.Add(activity.Replace("Night:", "").Trim());
            }
            else
            {
                // Auto-categorize based on content
                if (lowerActivity.Contains("dsa") || lowerActivity.Contains("problem") ||
                    lowerActivity.Contains("leetcode") || lowerActivity.Contains("array") ||
                    lowerActivity.Contains("string") || lowerActivity.Contains("tree") ||
                    lowerActivity.Contains("graph"))
                {
                    morningActivities.Add(activity);
                }
                else if (lowerActivity.Contains("read") || lowerActivity.Contains("review") ||
                         lowerActivity.Contains("study") || lowerActivity.Contains("document"))
                {
                    nightActivities.Add(activity);
                }
                else
                {
                    eveningActivities.Add(activity);
                }
            }
        }

        day.MorningActivity = string.Join("; ", morningActivities);
        day.EveningActivity = string.Join("; ", eveningActivities);
        day.NightActivity = string.Join("; ", nightActivities);
    }

    /// <summary>
    /// Parse date string from the markdown
    /// </summary>
    private DateTime ParseDate(string dateStr, int dayNumber)
    {
        try
        {
            // Format: "Sun, Dec 1" or "Mon, Jan 2"
            var parts = dateStr.Split(',');
            if (parts.Length != 2)
            {
                return CalculateDateFromDayNumber(dayNumber);
            }

            var monthDay = parts[1].Trim().Split(' ');
            if (monthDay.Length != 2)
            {
                return CalculateDateFromDayNumber(dayNumber);
            }

            var month = monthDay[0];
            var day = int.Parse(monthDay[1]);

            // Map month abbreviation to number
            var monthNumber = month switch
            {
                "Jan" => 1, "Feb" => 2, "Mar" => 3, "Apr" => 4,
                "May" => 5, "Jun" => 6, "Jul" => 7, "Aug" => 8,
                "Sep" => 9, "Oct" => 10, "Nov" => 11, "Dec" => 12,
                _ => 1
            };

            // Determine year (2025 for Dec, 2026 for Jan-Nov)
            var year = monthNumber == 12 ? 2025 : 2026;

            return new DateTime(year, monthNumber, day);
        }
        catch
        {
            return CalculateDateFromDayNumber(dayNumber);
        }
    }

    /// <summary>
    /// Calculate date from day number (starting Dec 1, 2025)
    /// </summary>
    private DateTime CalculateDateFromDayNumber(int dayNumber)
    {
        var startDate = new DateTime(2025, 12, 1);
        return startDate.AddDays(dayNumber - 1);
    }

    /// <summary>
    /// Display import summary (for console output)
    /// </summary>
    public void DisplayImportSummary(int importedCount)
    {
        Console.WriteLine("=== Import Summary ===");
        Console.WriteLine($"Total Days Imported: {importedCount}");
        Console.WriteLine($"Start Date: December 1, 2025");
        Console.WriteLine($"End Date: December 1, 2026");
        Console.WriteLine($"Duration: 368 days");
        Console.WriteLine($"✓ Successfully imported {importedCount} days!");
    }
}
