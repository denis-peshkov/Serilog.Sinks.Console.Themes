namespace Serilog.Sinks.Console.Themes;

/// <summary>
/// Custom true-color ANSI palette for Serilog's console sink (<c>WriteTo.Console(theme: ...)</c>).
/// Edit <see cref="DarkColors"/> using <see cref="KnownColor"/> or map from <see cref="ConsoleColor"/> via <see cref="TrueColor"/> overloads.
/// With Serilog.Settings.Configuration, set <c>theme</c> to
/// <c>Serilog.Sinks.Console.Themes.CustomConsoleTheme::DarkTheme, Serilog.Sinks.Console.Themes</c> or <c>::LightTheme</c>.
/// </summary>
/// <remarks>
/// Prefer static <see cref="KnownColor"/> entries (not system UI colors like <see cref="KnownColor.ActiveCaption"/>), which are stable across platforms.
/// </remarks>
public static class CustomConsoleTheme
{
    /// <summary>Adjust these <see cref="KnownColor"/> values; ANSI is built in <see cref="TrueColor"/>.</summary>
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

    /// <summary>Built from <see cref="DarkColors"/> via <see cref="TrueColor"/>.</summary>
    public static ConsoleTheme Dark { get; } = new AnsiConsoleTheme(
        new Dictionary<ConsoleThemeStyle, string>
        {
            [ConsoleThemeStyle.Text] = TrueColor.Foreground(DarkColors.Text),
            [ConsoleThemeStyle.SecondaryText] = TrueColor.Foreground(DarkColors.SecondaryText),
            [ConsoleThemeStyle.TertiaryText] = TrueColor.Foreground(DarkColors.TertiaryText),
            [ConsoleThemeStyle.Invalid] = TrueColor.Foreground(DarkColors.Invalid),
            [ConsoleThemeStyle.Null] = TrueColor.Foreground(DarkColors.Null),
            [ConsoleThemeStyle.Name] = TrueColor.Foreground(DarkColors.Name),
            [ConsoleThemeStyle.String] = TrueColor.Foreground(DarkColors.String),
            [ConsoleThemeStyle.Number] = TrueColor.Foreground(DarkColors.Number),
            [ConsoleThemeStyle.Boolean] = TrueColor.Foreground(DarkColors.Boolean),
            [ConsoleThemeStyle.Scalar] = TrueColor.Foreground(DarkColors.Scalar),
            [ConsoleThemeStyle.LevelVerbose] = TrueColor.Foreground(DarkColors.LevelVerbose),
            [ConsoleThemeStyle.LevelDebug] = TrueColor.Foreground(DarkColors.LevelDebug),
            [ConsoleThemeStyle.LevelInformation] = TrueColor.Foreground(DarkColors.LevelInformation),
            [ConsoleThemeStyle.LevelWarning] = TrueColor.Foreground(DarkColors.LevelWarning),
            [ConsoleThemeStyle.LevelError] = TrueColor.Foreground(DarkColors.LevelErrorForeground)
                + TrueColor.Background(DarkColors.LevelErrorBackground),
            [ConsoleThemeStyle.LevelFatal] = TrueColor.BoldForegroundBackground(
                DarkColors.LevelFatalForeground,
                DarkColors.LevelFatalBackground),
        });

    /// <summary>Built from <see cref="LightColors"/> via <see cref="TrueColor"/>.</summary>
    public static ConsoleTheme Light { get; } = new AnsiConsoleTheme(
        new Dictionary<ConsoleThemeStyle, string>
        {
            [ConsoleThemeStyle.Text] = TrueColor.Foreground(LightColors.Text),
            [ConsoleThemeStyle.SecondaryText] = TrueColor.Foreground(LightColors.SecondaryText),
            [ConsoleThemeStyle.TertiaryText] = TrueColor.Foreground(LightColors.TertiaryText),
            [ConsoleThemeStyle.Invalid] = TrueColor.Foreground(LightColors.Invalid),
            [ConsoleThemeStyle.Null] = TrueColor.Foreground(LightColors.Null),
            [ConsoleThemeStyle.Name] = TrueColor.Foreground(LightColors.Name),
            [ConsoleThemeStyle.String] = TrueColor.Foreground(LightColors.String),
            [ConsoleThemeStyle.Number] = TrueColor.Foreground(LightColors.Number),
            [ConsoleThemeStyle.Boolean] = TrueColor.Foreground(LightColors.Boolean),
            [ConsoleThemeStyle.Scalar] = TrueColor.Foreground(LightColors.Scalar),
            [ConsoleThemeStyle.LevelVerbose] = TrueColor.Foreground(LightColors.LevelVerbose),
            [ConsoleThemeStyle.LevelDebug] = TrueColor.Foreground(LightColors.LevelDebug),
            [ConsoleThemeStyle.LevelInformation] = TrueColor.Foreground(LightColors.LevelInformation),
            [ConsoleThemeStyle.LevelWarning] = TrueColor.Foreground(LightColors.LevelWarning),
            [ConsoleThemeStyle.LevelError] = TrueColor.Foreground(LightColors.LevelErrorForeground)
                + TrueColor.Background(LightColors.LevelErrorBackground),
            [ConsoleThemeStyle.LevelFatal] = TrueColor.BoldForegroundBackground(
                LightColors.LevelFatalForeground,
                LightColors.LevelFatalBackground),
        });
}
