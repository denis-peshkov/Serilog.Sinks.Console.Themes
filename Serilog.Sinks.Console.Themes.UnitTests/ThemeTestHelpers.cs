using System.Reflection;

namespace Serilog.Sinks.Console.Themes.UnitTests;

internal static class ThemeTestHelpers
{
    internal static int GetStyleCount(ConsoleTheme theme)
    {
        var dict = GetStyles(theme);
        return dict.Count;
    }

    internal static string GetStyle(ConsoleTheme theme, ConsoleThemeStyle style) =>
        GetStyles(theme)[style];

    internal static IReadOnlyDictionary<ConsoleThemeStyle, string> GetStyles(ConsoleTheme theme)
    {
        var field = theme.GetType().GetField("_styles", BindingFlags.Instance | BindingFlags.NonPublic);
        field.Should().NotBeNull();
        var dict = field!.GetValue(theme) as IReadOnlyDictionary<ConsoleThemeStyle, string>;
        dict.Should().NotBeNull();
        return dict!;
    }
}
