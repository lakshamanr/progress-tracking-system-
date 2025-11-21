# 🎯 Daily Plan Tracker

<div align="center">

**A beautiful, modern ASP.NET Core MVC web application for tracking your 368-day learning journey**

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue)
![SQLite](https://img.shields.io/badge/Database-SQLite-green)
![Bootstrap](https://img.shields.io/badge/UI-Bootstrap%205.3-purple)
![Chart.js](https://img.shields.io/badge/Charts-Chart.js-orange)

</div>

---

## 📖 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Screenshots](#screenshots)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage Guide](#usage-guide)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Data Import/Export](#data-importexport)
- [Customization](#customization)
- [Troubleshooting](#troubleshooting)
- [Future Enhancements](#future-enhancements)
- [Contributing](#contributing)

---

## 🌟 Overview

Daily Plan Tracker is a comprehensive web application designed to help you track and manage your 368-day learning journey. Built with modern web technologies and featuring a beautiful gradient-based UI, it provides everything you need to stay consistent, monitor progress, and achieve your goals.

**Perfect for:**
- 🎓 Students on a structured learning path
- 💻 Developers preparing for technical interviews
- 📚 Anyone on a year-long learning journey
- 🏆 Goal-oriented individuals who value tracking progress

---

## ✨ Features

### 🏠 **Beautiful Dashboard**
- Stunning gradient header with modern design
- Overview of your learning progress at a glance
- Today's plan preview with quick actions
- Key statistics: completion rate, DSA problems, total hours, current streak
- Animated cards with hover effects
- Color-coded progress indicators

### 📅 **Daily Progress Tracking**
- Interactive tracking interface for daily activities
- **Morning Activities**: Track completion with checkboxes
- **Evening Activities**: Monitor evening tasks
- **Night Activities**: Log nighttime learning
- **DSA Problems**: Record problems solved (0-100)
- **Hours Spent**: Track time invested (with decimal precision)
- **Energy Level**: Rate your energy (1-5 scale) with visual slider
- **Confidence Level**: Track confidence (1-5 scale) with visual slider
- **Notes**: Add reflections, learnings, and observations
- **Skip Days**: Skip days with reason and optional auto-reschedule

### 📊 **Multiple View Options**

#### Today's Plan
- Detailed view of current day's activities
- Quick completion toggles
- Save progress with one click
- Skip day modal with auto-reschedule option

#### Weekly View
- See all 7 days of the current week
- Daily breakdown table with all metrics
- **2 Interactive Charts**:
  - Bar chart showing daily completion percentages
  - Line chart tracking energy and confidence trends
- Week-at-a-glance summary cards

#### Monthly View
- Calendar grid showing all days in the month
- **4 Summary Cards**:
  - Completed days count and percentage
  - Days in progress
  - Total DSA problems solved
  - Total hours invested
- **2 Charts**:
  - Pie chart showing completion status distribution
  - Bar chart displaying DSA problems per day
- Focus areas breakdown
- Monthly theme display

#### Calendar View (Year Overview)
- Full 368-day calendar heatmap
- Color-coded completion status (completed, in-progress, not started)
- Organized by months with themes
- Hover tooltips with day details
- Today marker
- Yearly progress trend chart

#### Statistics Dashboard
- **6 Interactive Chart.js Charts**:
  - Overall completion pie chart
  - Daily DSA problems bar chart
  - Hours spent trend line chart
  - Energy levels over time
  - Confidence progression
  - Activity breakdown (morning/evening/night)
- Comprehensive metrics and averages
- Visual progress indicators

#### Timeline View
- Linear timeline of your entire journey
- Day-by-day progress visualization
- Color-coded status indicators
- Detailed notes for each day

### 📋 **Reports & Data Management**

#### Import Features
- **Import Plan**: Upload your 368-day plan from markdown file
  - Automatic parsing of day numbers, dates, activities
  - Extract focus areas and interview topics
  - Organize by weeks and months
  - Apply monthly themes

- **Import Progress CSV**: Bulk update progress from CSV
  - Upload CSV file with progress data
  - Download template with examples
  - Update existing records or create new ones
  - Detailed error reporting
  - Support for multiple boolean formats (true/false, yes/no, 1/0, ✓/x)

#### Export Features
- **Export to Excel**: Multi-sheet workbook with:
  - Overview sheet with summary statistics
  - Daily Progress sheet with all details
  - Weekly Summaries sheet
  - Skip History sheet
  - Formatted tables with colors and borders

- **Export to CSV**: Single file with all daily data
  - Compatible with Excel, Google Sheets
  - Perfect for custom analysis
  - Easy to import back

#### Generated Reports
- **Weekly Summary**: Detailed week-by-week breakdown
- **Monthly Report**: Month-by-month progress with themes
  - Completion rates
  - DSA problems per month
  - Hours invested
  - Achievement badges (🏆 for 80%+ completion)

### 🎨 **Modern UI/UX**

#### Design Features
- **Vibrant Gradient Color Scheme**:
  - Primary: Pink/Coral to Teal (#ff6b6b → #4ecdc4)
  - Success: Green gradient (#56ab2f → #a8e063)
  - Info: Blue gradient (#4facfe → #00f2fe)
  - Warning: Pink to Red gradient
  - Purple: Deep purple gradient

- **Beautiful Header**: Gradient background with curved bottom edge
- **Modern Navigation**: Purple gradient navbar with centered menu
- **Enhanced Cards**: Rounded corners, shadows, gradient headers
- **Smooth Animations**: fadeIn, scaleIn, slideIn effects
- **Hover Effects**: Lift animations on buttons and cards
- **Gradient Buttons**: Colorful gradients with enhanced hover states
- **Modern Alerts**: Colored left borders with scale-in animation
- **Beautiful Footer**: Gradient background with inspirational text
- **Responsive Design**: Works on desktop, tablet, and mobile

---

## 📸 Screenshots

### Dashboard
Beautiful gradient header with key statistics and today's plan overview.

### Today's Plan
Interactive daily tracking with checkboxes, sliders, and notes field.

### Weekly View
7-day breakdown with charts showing completion and energy/confidence trends.

### Monthly Calendar
Grid view with color-coded completion status and monthly statistics.

### Statistics Dashboard
6 comprehensive charts visualizing all aspects of your progress.

### CSV Import
Upload CSV files to bulk update progress with template download option.

---

## 🛠️ Technology Stack

| Technology | Purpose | Version |
|------------|---------|---------|
| **ASP.NET Core MVC** | Web Framework | 8.0 |
| **Entity Framework Core** | ORM | 8.0 |
| **SQLite** | Database | Latest |
| **Bootstrap** | UI Framework | 5.3 |
| **Bootstrap Icons** | Icon Library | 1.11 |
| **Chart.js** | Data Visualization | 4.4 |
| **ClosedXML** | Excel Generation | 0.102 |
| **C#** | Programming Language | 12.0 |

---

## 📋 Prerequisites

Before you begin, ensure you have the following installed:

- **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** or later
- **Code Editor**: Visual Studio 2022, VS Code, or JetBrains Rider (optional)
- **Git** (for cloning the repository)

---

## 🚀 Installation

### Step 1: Clone the Repository

```bash
git clone <your-repository-url>
cd progress-tracking-system-
```

### Step 2: Restore Dependencies

```bash
dotnet restore
```

This will download all required NuGet packages:
- Microsoft.EntityFrameworkCore.Sqlite
- ClosedXML
- Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation (for development)

### Step 3: Build the Project

```bash
dotnet build
```

### Step 4: Run the Application

```bash
dotnet run
```

Or for development with hot reload:

```bash
dotnet watch run
```

### Step 5: Access the Application

Open your browser and navigate to:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001

The application will automatically:
- Create the SQLite database (`tracker.db`)
- Initialize the database schema
- Auto-import `daily_plan_2026.md` if it exists

---

## 📚 Usage Guide

### First Time Setup

#### 1. Import Your Learning Plan

**Option A: Automatic Import (Recommended)**
- Place your `daily_plan_2026.md` file in the project root
- Start the application
- The plan will be automatically imported on first run

**Option B: Manual Import**
1. Navigate to **Reports → Import Plan**
2. Click **Choose File** and select your markdown plan
3. Click **Import Plan**
4. Wait for confirmation message
5. You'll see "Successfully imported X days"

#### 2. Verify Import
1. Go to **Dashboard** to see total days
2. Check **Progress → Calendar View** to see all days
3. Navigate to **Progress → Today's Plan** to start tracking

### Daily Workflow

#### Morning Routine (5 minutes)
1. Open **Dashboard** or **Progress → Today's Plan**
2. Review today's activities:
   - Morning tasks
   - Evening tasks
   - Night tasks
   - Focus area
   - Interview prep topic

#### Throughout the Day
1. Check off completed activities as you finish them
2. Update DSA problems count as you solve them
3. Track hours spent on learning

#### Evening Reflection (10 minutes)
1. Complete any remaining checkboxes
2. Fill in final counts (DSA problems, hours)
3. Rate your **Energy Level** (1-5):
   - 1: Exhausted
   - 2: Low energy
   - 3: Normal
   - 4: Energetic
   - 5: Highly energetic
4. Rate your **Confidence Level** (1-5):
   - 1: Not confident
   - 2: Slightly confident
   - 3: Moderately confident
   - 4: Confident
   - 5: Very confident
5. Add **Notes**: Document learnings, challenges, wins
6. Click **Save Progress**

#### Skip a Day (When Needed)
1. Click **Skip This Day** button
2. Select a reason:
   - Personal
   - Sick
   - Travel
   - Work
   - Other
3. Optionally check **Auto-reschedule** to move activities to the next day
4. Confirm skip

### Weekly Review (15-30 minutes)

1. Navigate to **Progress → This Week**
2. Review the **Daily Breakdown Table**:
   - See all activities completion status
   - Check DSA problems and hours
   - Review energy and confidence trends
3. Analyze the **Charts**:
   - Completion percentage bar chart
   - Energy/confidence line chart
4. Identify patterns and areas for improvement
5. Plan adjustments for next week

### Monthly Review (30-60 minutes)

1. Go to **Progress → This Month**
2. Review **Monthly Statistics**:
   - Total completion percentage
   - Days completed vs in-progress
   - Total DSA problems solved
   - Total hours invested
3. Check **Monthly Calendar Grid**:
   - Visual overview of all days
   - Spot gaps or low-activity periods
4. Analyze **Charts**:
   - Completion status pie chart
   - Daily DSA problems distribution
5. Review **Focus Areas Summary**:
   - See which topics you covered most
6. Reflect on monthly theme achievement

### Progress Analysis

#### Statistics Dashboard
1. Navigate to **Progress → Statistics**
2. Explore **6 comprehensive charts**:
   - Overall completion (pie chart)
   - DSA problems trend (bar chart)
   - Hours spent over time (line chart)
   - Energy levels progression
   - Confidence growth
   - Activity breakdown
3. Review key metrics:
   - Average hours per day
   - Average DSA problems per day
   - Completion rates by activity type

#### Calendar Heatmap
1. Go to **Progress → Calendar View**
2. See **entire 368-day journey** at a glance
3. Hover over days for details
4. Identify long streaks and gaps
5. Review yearly progress trend chart

### Data Management

#### Bulk Import Progress (CSV)
1. Navigate to **Reports → Import Progress CSV**
2. Download the **CSV Template** (includes examples)
3. Fill in your progress data in Excel/Google Sheets:
   ```csv
   Day Number,Date,Morning Completed,Evening Completed,Night Completed,DSA Problems,Hours Spent,Energy Level (1-5),Confidence Level (1-5),Notes
   1,2025-12-01,true,true,false,3,2.5,4,3,"Great start!"
   2,2025-12-02,true,true,true,5,4.0,5,4,"Completed all activities"
   ```
4. Save as CSV format
5. Upload the file
6. Click **Import Progress**
7. Review any errors or warnings
8. Check imported data in dashboard

#### Export Your Data

**Export to Excel:**
1. Go to **Reports → Export to Excel**
2. Download the multi-sheet workbook
3. Open in Excel to see:
   - Overview (summary statistics)
   - Daily Progress (all days with details)
   - Weekly Summaries (week-by-week)
   - Skip History (all skipped days)

**Export to CSV:**
1. Navigate to **Reports → Export to CSV**
2. Download the CSV file
3. Open in Excel, Google Sheets, or use for analysis

### Best Practices

#### For Consistency
- ✅ Track daily, even if just marking checkboxes
- ✅ Set a specific time for logging (e.g., before bed)
- ✅ Use notes to capture insights while fresh
- ✅ Review weekly to maintain awareness
- ✅ Export monthly for backup

#### For Accuracy
- ✅ Be honest with energy/confidence ratings
- ✅ Log hours as you work (don't estimate later)
- ✅ Count all DSA problems, even partial attempts
- ✅ Mark activities complete only when truly done
- ✅ Use skip feature when genuinely unable to work

#### For Insights
- ✅ Look for patterns in energy levels
- ✅ Correlate confidence with hours spent
- ✅ Identify most productive days of week
- ✅ Track focus areas coverage
- ✅ Monitor streak maintenance

---

## 📁 Project Structure

```
DailyPlanTracker/
│
├── 📂 Controllers/              # MVC Controllers
│   ├── HomeController.cs        # Dashboard and home page
│   ├── ProgressController.cs    # All progress tracking views
│   └── ReportsController.cs     # Import/export functionality
│
├── 📂 Data/                     # Database Layer
│   ├── TrackerDbContext.cs     # EF Core database context
│   └── DbInitializer.cs        # Database initialization (future)
│
├── 📂 Models/                   # Data Models
│   ├── DailyPlan.cs            # 368-day plan structure
│   ├── DailyProgress.cs        # Daily progress tracking
│   ├── SkipRecord.cs           # Skipped days history
│   └── WeeklySummary.cs        # Weekly summary reports
│
├── 📂 Services/                 # Business Logic Services
│   ├── PlanImporter.cs         # Markdown plan parser
│   └── CsvProgressImporter.cs  # CSV progress importer
│
├── 📂 Views/                    # Razor Views
│   ├── 📂 Home/
│   │   ├── Index.cshtml        # Dashboard view
│   │   └── About.cshtml        # About page
│   ├── 📂 Progress/
│   │   ├── Today.cshtml        # Today's plan tracking
│   │   ├── Week.cshtml         # Weekly view with charts
│   │   ├── Month.cshtml        # Monthly calendar view
│   │   ├── Calendar.cshtml     # Year-long heatmap
│   │   ├── Stats.cshtml        # Statistics dashboard
│   │   └── Timeline.cshtml     # Timeline view
│   ├── 📂 Reports/
│   │   ├── Import.cshtml       # Plan import page
│   │   ├── ImportProgress.cshtml  # CSV import page
│   │   ├── Weekly.cshtml       # Weekly reports
│   │   └── Monthly.cshtml      # Monthly reports
│   └── 📂 Shared/
│       ├── _Layout.cshtml      # Main layout with gradient header
│       └── Error.cshtml        # Error page
│
├── 📂 wwwroot/                  # Static Files
│   ├── 📂 css/
│   │   └── site.css            # Custom styles with gradients
│   └── 📂 js/
│       └── site.js             # Custom JavaScript
│
├── 📄 Program.cs                # Application entry point
├── 📄 appsettings.json         # Configuration settings
├── 📄 DailyPlanTracker.csproj  # Project file
├── 📄 daily_plan_2026.md       # Your 368-day plan (user-provided)
├── 📄 tracker.db               # SQLite database (auto-created)
└── 📄 README.md                # This file
```

---

## ⚙️ Configuration

### Database Configuration

The application uses SQLite by default. Configuration is in `Program.cs`:

```csharp
builder.Services.AddDbContext<TrackerDbContext>(options =>
    options.UseSqlite("Data Source=tracker.db"));
```

**To change database location:**

```csharp
options.UseSqlite("Data Source=/path/to/your/tracker.db")
```

**To use SQL Server instead:**

1. Install package: `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
2. Update connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=DailyPlanTracker;Trusted_Connection=True;"
     }
   }
   ```
3. Update `Program.cs`:
   ```csharp
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
   ```

### Application Settings

Edit `appsettings.json` to configure:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Port Configuration

Change ports in `Properties/launchSettings.json`:

```json
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5002",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## 📤 Data Import/Export

### Plan Import Format

Your `daily_plan_2026.md` should follow this structure:

```markdown
# 368-Day Learning Plan (December 2025 - December 2026)

## Month 1: Foundation Building (December 2025)

### Week 1 (Days 1-7) - December 1-7, 2025
**Focus**: Getting Started, Environment Setup

#### Day 1 - December 1, 2025
**Morning (2 hours)**: Setup development environment
**Evening (2 hours)**: Learn Git basics
**Night (1 hour)**: LeetCode Easy Arrays
**Interview Prep**: Two-pointer technique
**DSA Focus**: Arrays and Strings

#### Day 2 - December 2, 2025
...
```

### CSV Import Format

Use this format for bulk progress import:

```csv
Day Number,Date,Morning Completed,Evening Completed,Night Completed,DSA Problems,Hours Spent,Energy Level (1-5),Confidence Level (1-5),Notes
1,2025-12-01,true,true,false,3,2.5,4,3,"Great start!"
2,2025-12-02,yes,yes,yes,5,4.0,5,4,"Feeling good"
3,2025-12-03,1,0,0,2,1.5,3,2,"Tired today"
```

**Supported boolean formats:**
- `true` / `false`
- `yes` / `no`
- `1` / `0`
- `✓` / `x`

### Excel Export Structure

**Sheet 1 - Overview:**
- Total days
- Completed days
- Completion percentage
- Total DSA problems
- Total hours
- Average energy
- Average confidence

**Sheet 2 - Daily Progress:**
- All 368 days with full details
- Formatted table with colors

**Sheet 3 - Weekly Summaries:**
- Week-by-week breakdown
- Completion rates
- Totals per week

**Sheet 4 - Skip History:**
- All skipped days
- Reasons for skipping
- Reschedule status

---

## 🎨 Customization

### Change Color Scheme

Edit `wwwroot/css/site.css` variables:

```css
:root {
    --gradient-primary: linear-gradient(135deg, #ff6b6b 0%, #4ecdc4 100%);
    --gradient-success: linear-gradient(135deg, #56ab2f 0%, #a8e063 100%);
    --gradient-info: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
    /* Add your custom gradients */
}
```

### Modify Header Text

Edit `Views/Shared/_Layout.cshtml`:

```html
<h1>🎯 Your Custom Title</h1>
<p class="subtitle">Your custom subtitle</p>
```

### Add New Dashboard Widgets

1. Open `Controllers/HomeController.cs`
2. Add data to ViewBag in `Index()` method
3. Edit `Views/Home/Index.cshtml`
4. Add your custom cards/widgets

### Customize Chart Colors

Edit chart configurations in view files:

```javascript
backgroundColor: 'rgba(255, 107, 107, 0.8)', // Custom color
borderColor: 'rgba(255, 107, 107, 1)',
```

---

## 🔧 Troubleshooting

### Common Issues

#### 1. Database Locked Error

**Error**: `SQLite Error 5: 'database is locked'`

**Solution**:
```bash
# Close all connections, delete database, restart
rm tracker.db
dotnet run
```

#### 2. Port Already in Use

**Error**: `Address already in use`

**Solution**:
```bash
# Use different port
dotnet run --urls="http://localhost:5002"

# Or kill process on port 5000
# Windows:
netstat -ano | findstr :5000
taskkill /PID <process-id> /F

# Linux/Mac:
lsof -ti:5000 | xargs kill -9
```

#### 3. Import Plan Not Working

**Checks**:
- Ensure `daily_plan_2026.md` is in project root
- Check file format matches expected structure
- Look for parsing errors in console output
- Verify database was created (`tracker.db` exists)

#### 4. Charts Not Displaying

**Solutions**:
- Check browser console for JavaScript errors
- Ensure Chart.js CDN is accessible
- Clear browser cache (Ctrl+Shift+Delete)
- Verify data is present (check API responses)

#### 5. EF Core Migration Errors

**Solution**:
```bash
# Delete migrations and recreate
rm -rf Migrations/
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### 6. CSV Import Errors

**Common Issues**:
- **Date format**: Use `YYYY-MM-DD` format
- **Boolean values**: Use supported formats (true/false, yes/no, etc.)
- **Missing columns**: Ensure all required columns present
- **Day not found**: Day number must exist in plan

#### 7. Build Errors

```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

---

## 🚀 Future Enhancements

### Planned Features

- [ ] **Dashboard Widgets**
  - [ ] Upcoming milestones
  - [ ] Recent achievements
  - [ ] Streak calendar

- [ ] **Advanced Analytics**
  - [ ] Productivity heatmap
  - [ ] Correlation analysis (energy vs DSA problems)
  - [ ] Predictive insights
  - [ ] Goal projection

- [ ] **Notifications & Reminders**
  - [ ] Daily tracking reminders
  - [ ] Email digest
  - [ ] Weekly review prompts
  - [ ] Milestone celebrations

- [ ] **Social Features**
  - [ ] Share progress snapshots
  - [ ] Compare with friends (anonymous)
  - [ ] Public progress page

- [ ] **Customization**
  - [ ] Dark mode
  - [ ] Custom themes
  - [ ] Configurable dashboard
  - [ ] Custom metrics

- [ ] **Mobile**
  - [ ] Progressive Web App (PWA)
  - [ ] Mobile-optimized views
  - [ ] Offline support

- [ ] **Authentication**
  - [ ] Multi-user support
  - [ ] User accounts
  - [ ] Cloud sync
  - [ ] Data privacy controls

- [ ] **AI Features**
  - [ ] Smart suggestions
  - [ ] Pattern recognition
  - [ ] Personalized insights
  - [ ] Automated weekly summaries

### Want to Contribute?

See [Contributing](#contributing) section below!

---

## 🤝 Contributing

This is currently a personal project, but contributions are welcome!

### How to Contribute

1. **Fork the Repository**
   ```bash
   git clone https://github.com/yourusername/progress-tracking-system-.git
   ```

2. **Create a Feature Branch**
   ```bash
   git checkout -b feature/AmazingFeature
   ```

3. **Make Your Changes**
   - Follow existing code style
   - Add comments for complex logic
   - Update README if needed

4. **Commit Your Changes**
   ```bash
   git commit -m "Add some AmazingFeature"
   ```

5. **Push to Branch**
   ```bash
   git push origin feature/AmazingFeature
   ```

6. **Open a Pull Request**
   - Describe your changes
   - Reference any related issues
   - Add screenshots if UI changes

### Contribution Guidelines

- **Code Style**: Follow C# coding conventions
- **Testing**: Test your changes thoroughly
- **Documentation**: Update README and code comments
- **Commits**: Use clear, descriptive commit messages
- **Pull Requests**: One feature per PR

### Areas for Contribution

- 🐛 Bug fixes
- ✨ New features
- 📝 Documentation improvements
- 🎨 UI/UX enhancements
- 🧪 Test coverage
- ♿ Accessibility improvements
- 🌍 Internationalization

---

## 📄 License

This project is for personal use and educational purposes.

Feel free to fork and customize for your own learning journey!

---

## 💡 Tips for Success

### Daily Habits
1. **Track Immediately**: Log progress right after completing activities
2. **Be Honest**: Accurate data leads to better insights
3. **Add Context**: Notes help you remember and learn
4. **Review Regularly**: Weekly reviews keep you accountable

### Using the Data
1. **Identify Patterns**: When are you most productive?
2. **Optimize Energy**: Plan hard tasks when energy is highest
3. **Track Confidence**: See how it grows over time
4. **Celebrate Wins**: Review your progress to stay motivated

### Staying Consistent
1. **Set Reminders**: Daily tracking reminder
2. **Make it Routine**: Same time each day
3. **Keep it Simple**: Don't overcomplicate entries
4. **Focus on Streaks**: Build momentum with consistency

---

## 📞 Support

### Getting Help

- **Issues**: Create an issue in the repository
- **Questions**: Open a discussion
- **Bugs**: Provide detailed steps to reproduce

### Useful Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Chart.js Documentation](https://www.chartjs.org/docs/latest/)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.3/)

---

## 🎉 Acknowledgments

- **Bootstrap** - For the excellent UI framework
- **Chart.js** - For beautiful, responsive charts
- **ClosedXML** - For Excel generation capabilities
- **ASP.NET Core Team** - For an amazing web framework
- **SQLite** - For a simple, embedded database

---

<div align="center">

## ⭐ Star this repo if you find it helpful!

### Happy Learning! 🚀

**Track your progress, stay consistent, and achieve your goals one day at a time!**

*Built with ❤️ using ASP.NET Core*

</div>
