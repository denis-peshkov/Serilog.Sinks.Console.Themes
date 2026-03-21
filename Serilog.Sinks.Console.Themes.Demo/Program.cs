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
    theme = CustomConsoleTheme.DarkTheme;
    themeLabel = "DarkTheme";
}
else
{
    theme = ConsoleThemes.UseTheme<MyTheme>();
    themeLabel = "MyThemeTemplate (custom)";
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
    Log.Verbose("Уровень Verbose — детальная отладочная информация.");
    Log.Debug("Уровень Debug — сообщение отладки.");
    Log.Information("Уровень Information — обычное информационное сообщение.");
    Log.Warning("Уровень Warning — предупреждение.");
    Log.Error("Уровень Error — ошибка без исключения.");
    Log.Fatal("Уровень Fatal — критическая ошибка (демо: приложение продолжит работу).");

    Log.Information(
        "Скаляры в шаблоне: строка {StringProp}, число {Number}, bool {Flag}, перечисление {Day}, GUID {Guid}, URI {Uri}, дата {When}",
        "пример",
        42,
        true,
        DayOfWeek.Friday,
        Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
        new Uri("https://example.com/path"),
        new DateTimeOffset(2026, 3, 21, 12, 0, 0, TimeSpan.FromHours(3)));

    Log.Information("Null в свойстве: {NullableString} и {NullableObject}", (string?)null, (object?)null);

    Log.Information(
        "Деструктурированный объект {@Payload}",
        new
        {
            Id = 7,
            Name = "alpha",
            Ratio = 1.5,
            Nested = new { Tags = new[] { "one", "two" } },
        });

    Log.ForContext("SourceContext", "Serilog.Sinks.Console.Themes.Demo.SampleService")
        .Warning("Сообщение с другим SourceContext для сравнения цвета имени контекста.");

    try
    {
        throw new InvalidOperationException("Внутреннее исключение демо.", new ArgumentException("Причина."));
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Исключение с трассировкой стека (Exception).");
    }

    Log.Information(
        "Структурированный вывод завершён. Тема: {Theme}. Запуск: dotnet run --project Serilog.Sinks.Console.Themes.Demo -- dark|light|custom",
        themeLabel);
}

Log.CloseAndFlush();

if (Debugger.IsAttached)
{
    Console.WriteLine();
    Console.WriteLine("Нажмите Enter для выхода…");
    Console.ReadLine();
}
