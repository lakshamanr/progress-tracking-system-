using DailyPlanTracker.Data;
using DailyPlanTracker.Models;
using DailyPlanTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanTracker.Controllers;

public class ProgressController : Controller
{
    private readonly TrackerDbContext _context;

    public ProgressController(TrackerDbContext context)
    {
        _context = context;
    }

    // GET: Progress/Today
    public async Task<IActionResult> Today()
    {
        var today = DateTime.Today;
        var plan = await _context.DailyPlans
            .Include(p => p.Progress)
            .FirstOrDefaultAsync(p => p.CurrentDate.Date == today);

        if (plan == null)
        {
            ViewBag.ErrorMessage = "No plan found for today. Please import your plan first.";
            return View();
        }

        return View(plan);
    }

    // POST: Progress/UpdateToday
    [HttpPost]
    public async Task<IActionResult> UpdateToday(int planId, bool morningCompleted, bool eveningCompleted,
        bool nightCompleted, int dsaProblems, double hoursSpent, int energyLevel, int confidenceLevel, string? notes)
    {
        var plan = await _context.DailyPlans
            .Include(p => p.Progress)
            .FirstOrDefaultAsync(p => p.Id == planId);

        if (plan == null)
            return NotFound();

        if (plan.Progress == null)
        {
            plan.Progress = new DailyProgress
            {
                DailyPlanId = planId,
                CompletedDate = DateTime.Now
            };
            _context.DailyProgress.Add(plan.Progress);
        }

        plan.Progress.MorningCompleted = morningCompleted;
        plan.Progress.EveningCompleted = eveningCompleted;
        plan.Progress.NightCompleted = nightCompleted;
        plan.Progress.DsaProblemsCompleted = dsaProblems;
        plan.Progress.HoursSpent = hoursSpent;
        plan.Progress.EnergyLevel = energyLevel;
        plan.Progress.ConfidenceLevel = confidenceLevel;
        plan.Progress.Notes = notes ?? string.Empty;

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Progress updated successfully!";
        return RedirectToAction(nameof(Today));
    }

    // GET: Progress/Week
    public async Task<IActionResult> Week()
    {
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
        var endOfWeek = startOfWeek.AddDays(6);

        var weekPlans = await _context.DailyPlans
            .Include(p => p.Progress)
            .Where(p => p.CurrentDate >= startOfWeek && p.CurrentDate <= endOfWeek)
            .OrderBy(p => p.CurrentDate)
            .ToListAsync();

        ViewBag.WeekStart = startOfWeek;
        ViewBag.WeekEnd = endOfWeek;

        return View(weekPlans);
    }

    // GET: Progress/Month
    public async Task<IActionResult> Month()
    {
        var today = DateTime.Today;
        var startOfMonth = new DateTime(today.Year, today.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        var monthPlans = await _context.DailyPlans
            .Include(p => p.Progress)
            .Where(p => p.CurrentDate >= startOfMonth && p.CurrentDate <= endOfMonth)
            .OrderBy(p => p.CurrentDate)
            .ToListAsync();

        ViewBag.MonthName = today.ToString("MMMM yyyy");
        ViewBag.MonthTheme = monthPlans.FirstOrDefault()?.MonthTheme ?? "";

        return View(monthPlans);
    }

    // GET: Progress/Calendar
    public async Task<IActionResult> Calendar()
    {
        var allPlans = await _context.DailyPlans
            .Include(p => p.Progress)
            .OrderBy(p => p.CurrentDate)
            .ToListAsync();

        return View(allPlans);
    }

    // GET: Progress/Stats
    public async Task<IActionResult> Stats()
    {
        var allProgress = await _context.DailyProgress
            .Include(p => p.DailyPlan)
            .ToListAsync();

        var stats = new
        {
            TotalDays = await _context.DailyPlans.CountAsync(),
            CompletedDays = allProgress.Count(p => p.IsFullyCompleted),
            PartialDays = allProgress.Count(p => !p.IsFullyCompleted && (p.MorningCompleted || p.EveningCompleted || p.NightCompleted)),
            TotalDsaProblems = allProgress.Sum(p => p.DsaProblemsCompleted),
            TotalHours = allProgress.Sum(p => p.HoursSpent),
            AverageEnergy = allProgress.Any() ? allProgress.Average(p => p.EnergyLevel) : 0,
            AverageConfidence = allProgress.Any() ? allProgress.Average(p => p.ConfidenceLevel) : 0,
            CurrentStreak = await CalculateCurrentStreakAsync()
        };

        ViewBag.Stats = stats;

        return View(allProgress);
    }

    // GET: Progress/Timeline
    public async Task<IActionResult> Timeline()
    {
        var allPlans = await _context.DailyPlans
            .Include(p => p.Progress)
            .OrderBy(p => p.CurrentDate)
            .ToListAsync();

        return View(allPlans);
    }

    // POST: Progress/Skip
    [HttpPost]
    public async Task<IActionResult> Skip(int planId, string reason, bool autoReschedule)
    {
        var plan = await _context.DailyPlans.FindAsync(planId);
        if (plan == null)
            return NotFound();

        var skipRecord = new SkipRecord
        {
            DailyPlanId = planId,
            SkippedDate = DateTime.Now,
            Reason = reason,
            AutoRescheduled = autoReschedule
        };

        _context.SkipRecords.Add(skipRecord);

        if (autoReschedule)
        {
            // Reschedule all future days by 1 day
            var futurePlans = await _context.DailyPlans
                .Where(p => p.CurrentDate > plan.CurrentDate)
                .ToListAsync();

            foreach (var futurePlan in futurePlans)
            {
                futurePlan.CurrentDate = futurePlan.CurrentDate.AddDays(1);
            }
        }

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Day skipped successfully!";
        return RedirectToAction(nameof(Today));
    }

    private async Task<int> CalculateCurrentStreakAsync()
    {
        var today = DateTime.Today;
        var streak = 0;
        var currentDate = today;

        while (true)
        {
            var plan = await _context.DailyPlans
                .Include(p => p.Progress)
                .FirstOrDefaultAsync(p => p.CurrentDate.Date == currentDate);

            if (plan?.Progress == null || !plan.Progress.IsFullyCompleted)
                break;

            streak++;
            currentDate = currentDate.AddDays(-1);
        }

        return streak;
    }
}
