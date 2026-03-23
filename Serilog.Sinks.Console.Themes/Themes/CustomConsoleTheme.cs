// ReSharper disable CheckNamespace
namespace Serilog.Sinks.Console.Themes;

/// <summary>
/// Built-in dark/light console themes, palette constants, <see cref="UseTheme{T}"/> for any <see cref="BaseTheme"/> template,
/// and <see cref="TemplateTheme"/> aliases for <see cref="Serilog.Templates.ExpressionTemplate"/>.
/// </summary>
/// <remarks>
/// Prefer static <see cref="KnownColor"/> entries (not OS-reserved names like <see cref="KnownColor.ActiveCaption"/>), which are stable across platforms.
/// With Serilog.Settings.Configuration, set <c>theme</c> to
/// <c>Serilog.Sinks.Console.Themes.CustomConsoleTheme::Dark, Serilog.Sinks.Console.Themes</c> (or <c>::Light</c>) for built-in presets.
/// </remarks>
public static class CustomConsoleTheme
{
    private static readonly AnsiConsoleTheme DarkInstance = Create<DarkTheme>();

    private static readonly AnsiConsoleTheme LightInstance = Create<LightTheme>();

    /// <summary>Adjust these <see cref="KnownColor"/> values; escape sequences are built in <see cref="TrueColor"/> / <see cref="ThemeStyle"/>.</summary>
    public static class DarkColors
    {
        public const KnownColor Text          = KnownColor.GhostWhite;
        public const KnownColor SecondaryText = KnownColor.DarkGray;
        public const KnownColor TertiaryText  = KnownColor.DimGray;
        public const KnownColor Invalid       = KnownColor.Khaki;
        public const KnownColor Null          = KnownColor.MediumTurquoise;
        public const KnownColor Name          = KnownColor.DarkGray;
        public const KnownColor String        = KnownColor.CadetBlue;
        public const KnownColor Number        = KnownColor.HotPink;
        public const KnownColor Boolean       = KnownColor.MediumTurquoise;
        public const KnownColor Scalar        = KnownColor.MediumPurple;

        public const KnownColor LevelVerbose         = KnownColor.SlateGray;
        public const KnownColor LevelDebug           = KnownColor.CadetBlue;
        public const KnownColor LevelInformation     = KnownColor.SkyBlue;
        public const KnownColor LevelWarning         = KnownColor.Orange;
        public const KnownColor LevelErrorForeground = KnownColor.GhostWhite;
        public const KnownColor LevelErrorBackground = KnownColor.Maroon;
        public const KnownColor LevelFatalForeground = KnownColor.White;
        public const KnownColor LevelFatalBackground = KnownColor.Red;
    }

    /// <summary>Adjust these <see cref="KnownColor"/> values for terminals with a light background.</summary>
    public static class LightColors
    {
        public const KnownColor Text          = KnownColor.Black;
        public const KnownColor SecondaryText = KnownColor.DimGray;
        public const KnownColor TertiaryText  = KnownColor.Gray;
        public const KnownColor Invalid       = KnownColor.DarkGoldenrod;
        public const KnownColor Null          = KnownColor.DarkCyan;
        public const KnownColor Name          = KnownColor.DimGray;
        public const KnownColor String        = KnownColor.MidnightBlue;
        public const KnownColor Number        = KnownColor.DarkMagenta;
        public const KnownColor Boolean       = KnownColor.DarkCyan;
        public const KnownColor Scalar        = KnownColor.Indigo;

        public const KnownColor LevelVerbose         = KnownColor.Gray;
        public const KnownColor LevelDebug           = KnownColor.SteelBlue;
        public const KnownColor LevelInformation     = KnownColor.DodgerBlue;
        public const KnownColor LevelWarning         = KnownColor.DarkOrange;
        public const KnownColor LevelErrorForeground = KnownColor.GhostWhite;
        public const KnownColor LevelErrorBackground = KnownColor.Maroon;
        public const KnownColor LevelFatalForeground = KnownColor.White;
        public const KnownColor LevelFatalBackground = KnownColor.Red;
    }

    /// <summary>Dark palette (<see cref="DarkTheme"/>).</summary>
    public static ConsoleTheme Dark => DarkInstance;

    /// <summary>Light palette (<see cref="LightTheme"/>).</summary>
    public static ConsoleTheme Light => LightInstance;

    /// <summary>Dark palette for <see cref="Serilog.Templates.ExpressionTemplate"/> (<see cref="TemplateThemes.Dark"/>).</summary>
    public static TemplateTheme DarkTemplateTheme => TemplateThemes.Dark;

    /// <summary>Light palette for <see cref="Serilog.Templates.ExpressionTemplate"/> (<see cref="TemplateThemes.Light"/>).</summary>
    public static TemplateTheme LightTemplateTheme => TemplateThemes.Light;

    /// <summary>Builds a <see cref="ConsoleTheme"/> from a <see cref="BaseTheme"/> template.</summary>
    public static ConsoleTheme UseTheme<T>() where T : BaseTheme, new() => Create<T>();

    private static AnsiConsoleTheme Create<T>() where T : BaseTheme, new() =>
        new AnsiConsoleTheme(new T().ToStyleDictionary());
}
