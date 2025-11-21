using DailyPlanTracker.Data;
using DailyPlanTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanTracker.Controllers;

public class HomeController : Controller
{
    private readonly TrackerDbContext _context;

    public HomeController(TrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;

        // Get today's plan
        var todayPlan = await _context.DailyPlans
            .Include(p => p.Progress)
            .FirstOrDefaultAsync(p => p.CurrentDate.Date == today);

        // Get overall stats
        var totalDays = await _context.DailyPlans.CountAsync();
        var completedDays = await _context.DailyProgress
            .CountAsync(p => p.MorningCompleted && p.EveningCompleted && p.NightCompleted);
        var totalDsaProblems = await _context.DailyProgress.SumAsync(p => (int?)p.DsaProblemsCompleted) ?? 0;
        var totalHours = await _context.DailyProgress.SumAsync(p => (double?)p.HoursSpent) ?? 0;

        // Get current streak
        var streak = await CalculateCurrentStreakAsync();

        ViewBag.TodayPlan = todayPlan;
        ViewBag.TotalDays = totalDays;
        ViewBag.CompletedDays = completedDays;
        ViewBag.TotalDsaProblems = totalDsaProblems;
        ViewBag.TotalHours = totalHours;
        ViewBag.CurrentStreak = streak;
        ViewBag.CompletionRate = totalDays > 0 ? (int)((completedDays / (double)totalDays) * 100) : 0;

        return View();
    }

    public IActionResult About()
    {
        return View();
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
