namespace Serilog.Sinks.Console.Themes;

/// <summary>Maps <see cref="KnownColor"/> / <see cref="ConsoleColor"/> to <c>38;2</c> / <c>48;2</c> ANSI fragments.</summary>
public static class TrueColor
{
    private const string SgrBold = "\x1b[1m";

    public static string Foreground(KnownColor known) =>
        Foreground(ToRgbColor(known));

    public static string Background(KnownColor known) =>
        Background(ToRgbColor(known));

    /// <summary>Uses the same RGB as <see cref="Color.FromName"/> for the enum name (e.g. <see cref="ConsoleColor.DarkYellow"/> → "DarkYellow").</summary>
    public static string Foreground(ConsoleColor console) =>
        Foreground(Color.FromName(console.ToString()));

    public static string Background(ConsoleColor console) =>
        Background(Color.FromName(console.ToString()));

    public static string BoldForegroundBackground(KnownColor foreground, KnownColor background) =>
        SgrBold + Foreground(foreground) + Background(background);

    public static string BoldForegroundBackground(ConsoleColor foreground, ConsoleColor background) =>
        SgrBold + Foreground(foreground) + Background(background);

    private static Color ToRgbColor(KnownColor known)
    {
        var c = Color.FromKnownColor(known);
        if (c.IsSystemColor)
            throw new ArgumentException("Use a static web color, not a system UI KnownColor.", nameof(known));
        return c;
    }

    private static string Foreground(Color c) =>
        $"\x1b[38;2;{c.R};{c.G};{c.B}m";

    private static string Background(Color c) =>
        $"\x1b[48;2;{c.R};{c.G};{c.B}m";
}
