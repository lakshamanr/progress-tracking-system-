using ClosedXML.Excel;
using DailyPlanTracker.Data;
using DailyPlanTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DailyPlanTracker.Controllers;

public class ReportsController : Controller
{
    private readonly TrackerDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ReportsController(TrackerDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // GET: Reports/Import
    public IActionResult Import()
    {
        var hasPlans = _context.DailyPlans.Any();
        ViewBag.HasPlans = hasPlans;
        return View();
    }

    // POST: Reports/ImportPlan
    [HttpPost]
    public async Task<IActionResult> ImportPlan(IFormFile? planFile)
    {
        if (planFile == null || planFile.Length == 0)
        {
            TempData["ErrorMessage"] = "Please select a file to import.";
            return RedirectToAction(nameof(Import));
        }

        try
        {
            // Save uploaded file temporarily
            var tempPath = Path.Combine(Path.GetTempPath(), planFile.FileName);
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await planFile.CopyToAsync(stream);
            }

            // Import the plan
            var importer = new PlanImporter(_context);
            var importedCount = await importer.ImportFromFileAsync(tempPath);

            // Clean up temp file
            System.IO.File.Delete(tempPath);

            TempData["SuccessMessage"] = $"Successfully imported {importedCount} days!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error importing plan: {ex.Message}";
        }

        return RedirectToAction(nameof(Import));
    }

    // GET: Reports/Weekly
    public async Task<IActionResult> Weekly()
    {
        var summaries = await _context.WeeklySummaries
            .OrderByDescending(s => s.WeekStartDate)
            .Take(12)
            .ToListAsync();

        return View(summaries);
    }

    // GET: Reports/Monthly
    public async Task<IActionResult> Monthly()
    {
        var monthlyStats = await _context.DailyPlans
            .Include(p => p.Progress)
            .GroupBy(p => new { p.MonthNumber, p.MonthTheme })
            .Select(g => new
            {
                MonthNumber = g.Key.MonthNumber,
                MonthTheme = g.Key.MonthTheme,
                TotalDays = g.Count(),
                CompletedDays = g.Count(p => p.Progress != null && p.Progress.IsFullyCompleted),
                TotalDsaProblems = g.Sum(p => p.Progress != null ? p.Progress.DsaProblemsCompleted : 0),
                TotalHours = g.Sum(p => p.Progress != null ? p.Progress.HoursSpent : 0)
            })
            .OrderBy(m => m.MonthNumber)
            .ToListAsync();

        return View(monthlyStats);
    }

    // GET: Reports/ExportExcel
    public async Task<IActionResult> ExportExcel()
    {
        using var workbook = new XLWorkbook();

        // Overview Sheet
        var overviewSheet = workbook.Worksheets.Add("Overview");
        await AddOverviewSheet(overviewSheet);

        // Daily Progress Sheet
        var progressSheet = workbook.Worksheets.Add("Daily Progress");
        await AddDailyProgressSheet(progressSheet);

        // Weekly Summaries Sheet
        var weeklySheet = workbook.Worksheets.Add("Weekly Summaries");
        await AddWeeklySummariesSheet(weeklySheet);

        // Skip History Sheet
        var skipSheet = workbook.Worksheets.Add("Skip History");
        await AddSkipHistorySheet(skipSheet);

        // Save to memory stream
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        var fileName = $"DailyPlanTracker_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    // GET: Reports/ExportCsv
    public async Task<IActionResult> ExportCsv()
    {
        var plans = await _context.DailyPlans
            .Include(p => p.Progress)
            .OrderBy(p => p.DayNumber)
            .ToListAsync();

        var csv = new StringBuilder();
        csv.AppendLine("Day Number,Date,Title,Morning Activity,Evening Activity,Night Activity,Focus Area,Interview Prep,Morning Completed,Evening Completed,Night Completed,DSA Problems,Hours Spent,Energy Level,Confidence Level,Notes");

        foreach (var plan in plans)
        {
            var progress = plan.Progress;
            csv.AppendLine($"\"{plan.DayNumber}\",\"{plan.CurrentDate:yyyy-MM-dd}\",\"{plan.Title}\",\"{plan.MorningActivity}\",\"{plan.EveningActivity}\",\"{plan.NightActivity}\",\"{plan.FocusArea}\",\"{plan.InterviewPrepTopic}\"," +
                          $"{progress?.MorningCompleted},{progress?.EveningCompleted},{progress?.NightCompleted},{progress?.DsaProblemsCompleted ?? 0},{progress?.HoursSpent ?? 0},{progress?.EnergyLevel ?? 0},{progress?.ConfidenceLevel ?? 0},\"{progress?.Notes ?? ""}\"");
        }

        var fileName = $"DailyPlanTracker_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", fileName);
    }

    private async Task AddOverviewSheet(IXLWorksheet sheet)
    {
        var totalDays = await _context.DailyPlans.CountAsync();
        var completedDays = await _context.DailyProgress.CountAsync(p => p.IsFullyCompleted);
        var totalDsaProblems = await _context.DailyProgress.SumAsync(p => (int?)p.DsaProblemsCompleted) ?? 0;
        var totalHours = await _context.DailyProgress.SumAsync(p => (double?)p.HoursSpent) ?? 0;

        sheet.Cell("A1").Value = "Daily Plan Tracker - Overview";
        sheet.Cell("A1").Style.Font.Bold = true;
        sheet.Cell("A1").Style.Font.FontSize = 16;

        sheet.Cell("A3").Value = "Metric";
        sheet.Cell("B3").Value = "Value";
        sheet.Range("A3:B3").Style.Font.Bold = true;

        sheet.Cell("A4").Value = "Total Days";
        sheet.Cell("B4").Value = totalDays;

        sheet.Cell("A5").Value = "Completed Days";
        sheet.Cell("B5").Value = completedDays;

        sheet.Cell("A6").Value = "Completion Rate";
        sheet.Cell("B6").Value = totalDays > 0 ? $"{(completedDays * 100.0 / totalDays):F1}%" : "0%";

        sheet.Cell("A7").Value = "Total DSA Problems";
        sheet.Cell("B7").Value = totalDsaProblems;

        sheet.Cell("A8").Value = "Total Hours";
        sheet.Cell("B8").Value = $"{totalHours:F1}";

        sheet.Columns().AdjustToContents();
    }

    private async Task AddDailyProgressSheet(IXLWorksheet sheet)
    {
        var plans = await _context.DailyPlans
            .Include(p => p.Progress)
            .OrderBy(p => p.DayNumber)
            .ToListAsync();

        // Headers
        sheet.Cell("A1").Value = "Day";
        sheet.Cell("B1").Value = "Date";
        sheet.Cell("C1").Value = "Title";
        sheet.Cell("D1").Value = "Status";
        sheet.Cell("E1").Value = "DSA Problems";
        sheet.Cell("F1").Value = "Hours";
        sheet.Cell("G1").Value = "Energy";
        sheet.Cell("H1").Value = "Confidence";
        sheet.Range("A1:H1").Style.Font.Bold = true;

        int row = 2;
        foreach (var plan in plans)
        {
            sheet.Cell($"A{row}").Value = plan.DayNumber;
            sheet.Cell($"B{row}").Value = plan.CurrentDate.ToString("yyyy-MM-dd");
            sheet.Cell($"C{row}").Value = plan.Title;
            sheet.Cell($"D{row}").Value = plan.Progress == null ? "Not Started" :
                plan.Progress.IsFullyCompleted ? "Completed" : "In Progress";
            sheet.Cell($"E{row}").Value = plan.Progress?.DsaProblemsCompleted ?? 0;
            sheet.Cell($"F{row}").Value = plan.Progress?.HoursSpent ?? 0;
            sheet.Cell($"G{row}").Value = plan.Progress?.EnergyLevel ?? 0;
            sheet.Cell($"H{row}").Value = plan.Progress?.ConfidenceLevel ?? 0;
            row++;
        }

        sheet.Columns().AdjustToContents();
    }

    private async Task AddWeeklySummariesSheet(IXLWorksheet sheet)
    {
        var summaries = await _context.WeeklySummaries
            .OrderBy(s => s.WeekNumber)
            .ToListAsync();

        // Headers
        sheet.Cell("A1").Value = "Week";
        sheet.Cell("B1").Value = "Start Date";
        sheet.Cell("C1").Value = "End Date";
        sheet.Cell("D1").Value = "Days Completed";
        sheet.Cell("E1").Value = "DSA Problems";
        sheet.Cell("F1").Value = "Hours";
        sheet.Range("A1:F1").Style.Font.Bold = true;

        int row = 2;
        foreach (var summary in summaries)
        {
            sheet.Cell($"A{row}").Value = summary.WeekNumber;
            sheet.Cell($"B{row}").Value = summary.WeekStartDate.ToString("yyyy-MM-dd");
            sheet.Cell($"C{row}").Value = summary.WeekEndDate.ToString("yyyy-MM-dd");
            sheet.Cell($"D{row}").Value = summary.DaysCompleted;
            sheet.Cell($"E{row}").Value = summary.TotalDsaProblems;
            sheet.Cell($"F{row}").Value = summary.TotalHours;
            row++;
        }

        sheet.Columns().AdjustToContents();
    }

    private async Task AddSkipHistorySheet(IXLWorksheet sheet)
    {
        var skipRecords = await _context.SkipRecords
            .Include(s => s.DailyPlan)
            .OrderBy(s => s.SkippedDate)
            .ToListAsync();

        // Headers
        sheet.Cell("A1").Value = "Day Number";
        sheet.Cell("B1").Value = "Skipped Date";
        sheet.Cell("C1").Value = "Reason";
        sheet.Cell("D1").Value = "Auto Rescheduled";
        sheet.Range("A1:D1").Style.Font.Bold = true;

        int row = 2;
        foreach (var record in skipRecords)
        {
            sheet.Cell($"A{row}").Value = record.DailyPlan.DayNumber;
            sheet.Cell($"B{row}").Value = record.SkippedDate.ToString("yyyy-MM-dd");
            sheet.Cell($"C{row}").Value = record.Reason;
            sheet.Cell($"D{row}").Value = record.AutoRescheduled ? "Yes" : "No";
            row++;
        }

        sheet.Columns().AdjustToContents();
    }
}
