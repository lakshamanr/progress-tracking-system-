# Daily Plan Tracker

A comprehensive ASP.NET Core MVC web application for tracking your 368-day learning journey.

## Features

### 📊 Dashboard
- Overview of your learning progress
- Today's plan at a glance
- Key statistics (completion rate, DSA problems, total hours, current streak)
- Quick access to important features

### 📅 Daily Tracking
- Interactive tracking of morning, evening, and night activities
- Record DSA problems completed and hours spent
- Rate your energy and confidence levels (1-5 scale)
- Add notes and reflections for each day
- Skip days with automatic rescheduling

### 📈 Progress Visualization
- **Today**: View and track today's plan with detailed activities
- **Week View**: See your progress for the current week
- **Month View**: Monthly overview with themes and milestones
- **Calendar View**: Visual calendar with completion status
- **Statistics**: Comprehensive stats with charts and metrics
- **Timeline**: Visual timeline of your entire journey

### 📋 Reports & Export
- Import your daily plan from markdown files
- Export progress to Excel with multiple sheets (Overview, Daily Progress, Weekly Summaries, Skip History)
- Export to CSV for data analysis
- Weekly and monthly summary reports

## Technology Stack

- **Framework**: ASP.NET Core 8.0 MVC
- **Database**: SQLite with Entity Framework Core
- **UI**: Bootstrap 5.3 with Bootstrap Icons
- **Charts**: Chart.js for data visualization
- **Export**: ClosedXML for Excel generation

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

## Getting Started

### 1. Clone the Repository

```bash
git clone <your-repo-url>
cd progress-tracking-system-
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Run the Application

```bash
dotnet run
```

The application will start and be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

### 4. Import Your Plan

1. Navigate to **Reports → Import Plan**
2. Upload your `daily_plan_2026.md` file
3. Click **Import Plan**
4. Your 368-day learning journey will be imported into the database

### 5. Start Tracking!

1. Go to **Dashboard** or **Progress → Today's Plan**
2. Check off completed activities
3. Record your DSA problems and hours spent
4. Rate your energy and confidence
5. Add notes about your learnings
6. Save your progress!

## Project Structure

```
DailyPlanTracker/
├── Controllers/          # MVC Controllers
│   ├── HomeController.cs
│   ├── ProgressController.cs
│   └── ReportsController.cs
├── Data/                 # Database Context
│   ├── TrackerDbContext.cs
│   └── DbInitializer.cs
├── Models/               # Data Models
│   ├── DailyPlan.cs
│   ├── DailyProgress.cs
│   ├── SkipRecord.cs
│   └── WeeklySummary.cs
├── Services/             # Business Logic
│   └── PlanImporter.cs
├── Views/                # Razor Views
│   ├── Home/
│   ├── Progress/
│   ├── Reports/
│   └── Shared/
├── wwwroot/              # Static Files
│   ├── css/
│   └── js/
├── Program.cs            # Application Entry Point
└── appsettings.json      # Configuration
```

## Database

The application uses SQLite and automatically creates a `tracker.db` file in the project root. The database includes:

- **DailyPlans**: Your 368-day plan with activities and metadata
- **DailyProgress**: Daily progress records with completion status
- **SkipRecords**: History of skipped days with reasons
- **WeeklySummaries**: Weekly summary reports

## Features in Detail

### Plan Import
The markdown parser automatically extracts:
- Day number and date
- Morning, evening, and night activities
- Focus areas and interview prep topics
- Week and month information
- Monthly themes

### Progress Tracking
Track your daily progress with:
- Checkbox completion for morning/evening/night activities
- DSA problems count
- Hours spent (with decimal precision)
- Energy level slider (1-5)
- Confidence level slider (1-5)
- Notes field for reflections

### Statistics Dashboard
View comprehensive statistics:
- Total days in plan
- Days completed (full and partial)
- Total DSA problems solved
- Total hours logged
- Average energy and confidence levels
- Current streak calculation

### Export Features
Export your data in multiple formats:
- **Excel**: Multi-sheet workbook with overview, daily progress, weekly summaries, and skip history
- **CSV**: Single file with all daily data for analysis

## Customization

### Themes
Modify `wwwroot/css/site.css` to customize:
- Primary colors
- Card styles
- Button styles
- Animations

### Adding Features
The MVC architecture makes it easy to add new features:
1. Add methods to controllers for new actions
2. Create corresponding views in the Views folder
3. Update navigation in `_Layout.cshtml`

## Tips for Success

1. **Be Consistent**: Track your progress daily for best results
2. **Be Honest**: Accurate energy and confidence ratings help identify patterns
3. **Add Notes**: Document learnings, challenges, and wins
4. **Review Weekly**: Use the weekly view to reflect on progress
5. **Export Regularly**: Backup your data with Excel exports

## Troubleshooting

### Database Issues
If you encounter database errors:
```bash
# Delete the database and restart the app
rm tracker.db
dotnet run
```

### Port Already in Use
Change the port in `Properties/launchSettings.json` or use:
```bash
dotnet run --urls="http://localhost:5002"
```

## Future Enhancements

- [ ] Dashboard with Chart.js visualizations
- [ ] Calendar heatmap view
- [ ] Advanced analytics and insights
- [ ] Weekly summary auto-generation
- [ ] Email reminders
- [ ] Mobile-responsive improvements
- [ ] Dark mode
- [ ] Multi-user support with authentication

## Contributing

This is a personal project for tracking your learning journey. Feel free to fork and customize for your own use!

## License

This project is for personal use.

## Support

For issues or questions, please create an issue in the repository.

---

**Happy Learning! 🚀**

Track your progress, stay consistent, and achieve your goals one day at a time!
