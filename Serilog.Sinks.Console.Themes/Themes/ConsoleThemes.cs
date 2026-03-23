// ReSharper disable CheckNamespace
namespace Serilog.Sinks.Console.Themes;

/// <summary>Factory methods and built-in templates for Serilog's console sink.</summary>
public static class ConsoleThemes
{
    private static readonly AnsiConsoleTheme DarkInstance = Create<DarkTheme>();

    private static readonly AnsiConsoleTheme LightInstance = Create<LightTheme>();

    /// <summary>Dark palette (<see cref="DarkTheme"/>).</summary>
    public static ConsoleTheme Dark => DarkInstance;

    /// <summary>Light palette (<see cref="LightTheme"/>).</summary>
    public static ConsoleTheme Light => LightInstance;

    /// <summary>Builds a <see cref="ConsoleTheme"/> from a <see cref="BaseTheme"/> template.</summary>
    public static ConsoleTheme UseTheme<T>() where T : BaseTheme, new() => Create<T>();

    private static AnsiConsoleTheme Create<T>() where T : BaseTheme, new() =>
        new AnsiConsoleTheme(new T().ToStyleDictionary());
}
