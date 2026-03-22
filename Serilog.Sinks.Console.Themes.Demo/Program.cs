var themeArg = args.FirstOrDefault(a => !a.StartsWith("-", StringComparison.Ordinal)) ?? "dark";

ConsoleTheme theme;
string themeLabel;
if (themeArg.Equals("light", StringComparison.OrdinalIgnoreCase))
{
    theme = CustomConsoleTheme.LightTheme;
    themeLabel = "LightTheme";
}
else if (themeArg.Equals("dark", StringComparison.OrdinalIgnoreCase))
{
    theme = ConsoleThemes.Dark;
    themeLabel = "DarkTheme";
}
else if (themeArg.Equals("custom", StringComparison.OrdinalIgnoreCase))
{
    theme = ConsoleThemes.UseTheme<MyTheme>();
    themeLabel = "MyTheme (custom)";
}
else
{
    theme = AnsiConsoleTheme.Sixteen;
    themeLabel = "AnsiConsoleTheme.Sixteen";
}

const string outputTemplate =
    "{Timestamp:HH:mm:ss.fff} [{Level,-11}] {SourceContext:l}: {Message:lj}{NewLine}{Exception}";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: theme, outputTemplate: outputTemplate)
    .CreateLogger();

using (LogContext.PushProperty("DemoRunId", Guid.NewGuid().ToString("N")[..8]))
{
    Log.Verbose("Verbose level — detailed diagnostic output.");
    Log.Debug("Debug level — debug message.");
    Log.Information("Information level — standard informational message.");
    Log.Warning("Warning level — warning.");
    Log.Error("Error level — error without an exception.");
    Log.Fatal("Fatal level — critical error (demo: the app will keep running).");

    Log.Information(
        "Scalars in template: string {StringProp}, number {Number}, bool {Flag}, enum {Day}, GUID {Guid}, URI {Uri}, date/time {When}",
        "sample",
        42,
        true,
        DayOfWeek.Friday,
        Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
        new Uri("https://example.com/path"),
        new DateTimeOffset(2026, 3, 21, 12, 0, 0, TimeSpan.FromHours(3)));

    Log.Information("Null properties: {NullableString} and {NullableObject}", (string?)null, (object?)null);

    Log.Information(
        "Destructured object {@Payload}",
        new
        {
            Id = 7,
            Name = "alpha",
            Ratio = 1.5,
            Nested = new { Tags = new[] { "one", "two" } },
        });

    Log.ForContext("SourceContext", "Serilog.Sinks.Console.Themes.Demo.SampleService")
        .Warning("Message with a different SourceContext to compare context name styling.");

    try
    {
        throw new InvalidOperationException("Demo inner exception.", new ArgumentException("Cause."));
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Exception with stack trace.");
    }

    Log.Information(
        "Structured output complete. Theme: {Theme}. Run: dotnet run --project Serilog.Sinks.Console.Themes.Demo -- dark|light|custom|sixteen",
        themeLabel);
}

Log.CloseAndFlush();

if (Debugger.IsAttached)
{
    Console.WriteLine();
    Console.WriteLine("Press Enter to exit…");
    Console.ReadLine();
}
