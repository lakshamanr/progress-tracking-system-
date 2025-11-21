using System.Text;
using DailyPlanTracker.Data;
using DailyPlanTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanTracker.Services;

/// <summary>
/// Service to import progress data from CSV files
/// </summary>
public class CsvProgressImporter
{
    private readonly TrackerDbContext _context;

    public CsvProgressImporter(TrackerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Import progress data from CSV file
    /// </summary>
    public async Task<ImportResult> ImportProgressFromCsvAsync(Stream csvStream)
    {
        var result = new ImportResult();

        using var reader = new StreamReader(csvStream);

        // Skip header line
        var header = await reader.ReadLineAsync();
        if (header == null)
        {
            result.Errors.Add("CSV file is empty");
            return result;
        }

        int lineNumber = 1;
        while (!reader.EndOfStream)
        {
            lineNumber++;
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            try
            {
                var fields = ParseCsvLine(line);
                await ProcessCsvLineAsync(fields, lineNumber, result);
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Line {lineNumber}: {ex.Message}");
            }
        }

        return result;
    }

    /// <summary>
    /// Process a single CSV line
    /// </summary>
    private async Task ProcessCsvLineAsync(string[] fields, int lineNumber, ImportResult result)
    {
        if (fields.Length < 10)
        {
            result.Errors.Add($"Line {lineNumber}: Invalid number of fields (expected 10, got {fields.Length})");
            return;
        }

        // Parse fields
        if (!int.TryParse(fields[0], out int dayNumber))
        {
            result.Errors.Add($"Line {lineNumber}: Invalid day number '{fields[0]}'");
            return;
        }

        // Find the daily plan
        var plan = await _context.DailyPlans
            .Include(p => p.Progress)
            .FirstOrDefaultAsync(p => p.DayNumber == dayNumber);

        if (plan == null)
        {
            result.Errors.Add($"Line {lineNumber}: Day {dayNumber} not found in the plan");
            return;
        }

        // Parse progress data
        var morningCompleted = ParseBool(fields[2]);
        var eveningCompleted = ParseBool(fields[3]);
        var nightCompleted = ParseBool(fields[4]);
        var dsaProblems = int.TryParse(fields[5], out int dsa) ? dsa : 0;
        var hoursSpent = double.TryParse(fields[6], out double hours) ? hours : 0;
        var energyLevel = int.TryParse(fields[7], out int energy) ? Math.Clamp(energy, 1, 5) : 3;
        var confidenceLevel = int.TryParse(fields[8], out int confidence) ? Math.Clamp(confidence, 1, 5) : 3;
        var notes = fields.Length > 9 ? fields[9] : "";

        // Create or update progress
        if (plan.Progress == null)
        {
            plan.Progress = new DailyProgress
            {
                DailyPlanId = plan.Id,
                CompletedDate = DateTime.Now
            };
            _context.DailyProgress.Add(plan.Progress);
            result.Created++;
        }
        else
        {
            result.Updated++;
        }

        // Update progress fields
        plan.Progress.MorningCompleted = morningCompleted;
        plan.Progress.EveningCompleted = eveningCompleted;
        plan.Progress.NightCompleted = nightCompleted;
        plan.Progress.DsaProblemsCompleted = dsaProblems;
        plan.Progress.HoursSpent = hoursSpent;
        plan.Progress.EnergyLevel = energyLevel;
        plan.Progress.ConfidenceLevel = confidenceLevel;
        plan.Progress.Notes = notes;

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Parse CSV line handling quoted fields
    /// </summary>
    private string[] ParseCsvLine(string line)
    {
        var fields = new List<string>();
        var currentField = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(currentField.ToString().Trim());
                currentField.Clear();
            }
            else
            {
                currentField.Append(c);
            }
        }

        fields.Add(currentField.ToString().Trim());
        return fields.ToArray();
    }

    /// <summary>
    /// Parse boolean from string (handles various formats)
    /// </summary>
    private bool ParseBool(string value)
    {
        value = value.Trim().ToLower();
        return value == "true" || value == "yes" || value == "1" || value == "✓" || value == "x";
    }

    /// <summary>
    /// Generate CSV template
    /// </summary>
    public async Task<string> GenerateCsvTemplateAsync()
    {
        var csv = new StringBuilder();
        csv.AppendLine("Day Number,Date,Morning Completed,Evening Completed,Night Completed,DSA Problems,Hours Spent,Energy Level (1-5),Confidence Level (1-5),Notes");

        var plans = await _context.DailyPlans
            .Include(p => p.Progress)
            .OrderBy(p => p.DayNumber)
            .Take(10) // Include first 10 days as examples
            .ToListAsync();

        foreach (var plan in plans)
        {
            csv.AppendLine($"{plan.DayNumber},{plan.CurrentDate:yyyy-MM-dd}," +
                          $"{(plan.Progress?.MorningCompleted ?? false)}," +
                          $"{(plan.Progress?.EveningCompleted ?? false)}," +
                          $"{(plan.Progress?.NightCompleted ?? false)}," +
                          $"{plan.Progress?.DsaProblemsCompleted ?? 0}," +
                          $"{plan.Progress?.HoursSpent ?? 0}," +
                          $"{plan.Progress?.EnergyLevel ?? 3}," +
                          $"{plan.Progress?.ConfidenceLevel ?? 3}," +
                          $"\"{plan.Progress?.Notes ?? ""}\"");
        }

        return csv.ToString();
    }
}

/// <summary>
/// Result of CSV import operation
/// </summary>
public class ImportResult
{
    public int Created { get; set; }
    public int Updated { get; set; }
    public List<string> Errors { get; set; } = new();

    public bool HasErrors => Errors.Any();
    public int TotalProcessed => Created + Updated;
}
