using DailyPlanTracker.Data;
using DailyPlanTracker.Services;
using Spectre.Console;

namespace DailyPlanTracker;

class Program
{
    static async Task<int> Main(string[] args)
    {
        try
        {
            // Display banner
            DisplayBanner();

            // Initialize database
            DbInitializer.Initialize();

            // Parse command
            if (args.Length == 0)
            {
                DisplayHelp();
                return 0;
            }

            var command = args[0].ToLower();

            return command switch
            {
                "init" => await InitializeCommand(),
                "import-plan" => await ImportPlanCommand(args),
                "today" => await TodayCommand(),
                "complete" => await CompleteCommand(),
                "skip" => await SkipCommand(args),
                "stats" => await StatsCommand(),
                "week" => await WeekCommand(),
                "month" => await MonthCommand(),
                "timeline" => await TimelineCommand(),
                "help" or "--help" or "-h" => DisplayHelp(),
                _ => UnknownCommand(command)
            };
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            return 1;
        }
    }

    static void DisplayBanner()
    {
        AnsiConsole.Write(
            new FigletText("Daily Plan Tracker")
                .LeftJustified()
                .Color(Color.Blue));

        AnsiConsole.MarkupLine("[dim]368-Day Learning Journey Tracker[/]");
        AnsiConsole.WriteLine();
    }

    static int DisplayHelp()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]Command[/]")
            .AddColumn("[yellow]Description[/]");

        table.AddRow("init", "Initialize the database");
        table.AddRow("import-plan <file>", "Import daily plan from markdown file");
        table.AddRow("today", "View and track today's plan");
        table.AddRow("complete", "Mark today as complete");
        table.AddRow("skip [reason]", "Skip today with optional reason");
        table.AddRow("stats", "View overall statistics");
        table.AddRow("week", "View this week's progress");
        table.AddRow("month", "View this month's progress");
        table.AddRow("timeline", "View progress timeline");
        table.AddRow("help", "Display this help message");

        AnsiConsole.Write(table);
        return 0;
    }

    static async Task<int> InitializeCommand()
    {
        AnsiConsole.Status()
            .Start("Initializing database...", ctx =>
            {
                DbInitializer.Initialize();
            });

        AnsiConsole.MarkupLine("[green]✓[/] Database initialized successfully!");
        return 0;
    }

    static async Task<int> ImportPlanCommand(string[] args)
    {
        if (args.Length < 2)
        {
            AnsiConsole.MarkupLine("[red]Error:[/] Please specify the plan file path");
            AnsiConsole.MarkupLine("[dim]Usage: tracker import-plan <file>[/]");
            return 1;
        }

        var filePath = args[1];

        var importedCount = await AnsiConsole.Status()
            .StartAsync("Importing daily plan...", async ctx =>
            {
                var importer = new PlanImporter();
                return await importer.ImportFromFileAsync(filePath);
            });

        var importer = new PlanImporter();
        importer.DisplayImportSummary(importedCount);

        return 0;
    }

    static async Task<int> TodayCommand()
    {
        AnsiConsole.MarkupLine("[yellow]Today's plan view - Coming soon![/]");
        // TODO: Implement today's plan view
        return 0;
    }

    static async Task<int> CompleteCommand()
    {
        AnsiConsole.MarkupLine("[yellow]Complete command - Coming soon![/]");
        // TODO: Implement complete command
        return 0;
    }

    static async Task<int> SkipCommand(string[] args)
    {
        AnsiConsole.MarkupLine("[yellow]Skip command - Coming soon![/]");
        // TODO: Implement skip command
        return 0;
    }

    static async Task<int> StatsCommand()
    {
        AnsiConsole.MarkupLine("[yellow]Stats view - Coming soon![/]");
        // TODO: Implement stats view
        return 0;
    }

    static async Task<int> WeekCommand()
    {
        AnsiConsole.MarkupLine("[yellow]Week view - Coming soon![/]");
        // TODO: Implement week view
        return 0;
    }

    static async Task<int> MonthCommand()
    {
        AnsiConsole.MarkupLine("[yellow]Month view - Coming soon![/]");
        // TODO: Implement month view
        return 0;
    }

    static async Task<int> TimelineCommand()
    {
        AnsiConsole.MarkupLine("[yellow]Timeline view - Coming soon![/]");
        // TODO: Implement timeline view
        return 0;
    }

    static int UnknownCommand(string command)
    {
        AnsiConsole.MarkupLine($"[red]Unknown command:[/] {command}");
        AnsiConsole.MarkupLine("[dim]Run 'tracker help' for available commands[/]");
        return 1;
    }
}
