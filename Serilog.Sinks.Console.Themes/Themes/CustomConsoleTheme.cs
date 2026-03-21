// ReSharper disable CheckNamespace
namespace Serilog.Sinks.Console.Themes;

/// <summary>
/// Known-color palettes for the built-in dark/light themes. Themes are built via
/// <see cref="ConsoleThemes.Dark"/>, <see cref="ConsoleThemes.Light"/>, or <see cref="ConsoleThemes.UseTheme{T}"/>.
/// </summary>
/// <remarks>
/// Prefer static <see cref="KnownColor"/> entries (not OS-reserved names like <see cref="KnownColor.ActiveCaption"/>), which are stable across platforms.
/// With Serilog.Settings.Configuration, set <c>theme</c> to
/// <c>Serilog.Sinks.Console.Themes.ConsoleThemes::Dark, Serilog.Sinks.Console.Themes</c> (static property) or a custom template type.
/// </remarks>
public static class CustomConsoleTheme
{
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

    /// <summary>Dark palette (<see cref="ConsoleThemes.Dark"/>).</summary>
    public static ConsoleTheme DarkTheme => ConsoleThemes.Dark;

    /// <summary>Light palette (<see cref="ConsoleThemes.Light"/>).</summary>
    public static ConsoleTheme LightTheme => ConsoleThemes.Light;
}
