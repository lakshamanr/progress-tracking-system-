using DailyPlanTracker.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<TrackerDbContext>(options =>
    options.UseSqlite("Data Source=tracker.db"));

// Add runtime compilation for development
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Initialize database and auto-import plan if empty
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TrackerDbContext>();
    context.Database.EnsureCreated();

    // Auto-import daily_plan_2026.md if database is empty
    if (!context.DailyPlans.Any())
    {
        var planFilePath = Path.Combine(Directory.GetCurrentDirectory(), "daily_plan_2026.md");
        if (File.Exists(planFilePath))
        {
            try
            {
                var importer = new DailyPlanTracker.Services.PlanImporter(context);
                var importedCount = await importer.ImportFromFileAsync(planFilePath);
                Console.WriteLine($"✓ Auto-imported {importedCount} days from daily_plan_2026.md");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not auto-import plan: {ex.Message}");
            }
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
