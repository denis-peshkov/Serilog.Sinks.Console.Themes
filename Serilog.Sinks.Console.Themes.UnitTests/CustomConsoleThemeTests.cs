namespace Serilog.Sinks.Console.Themes.UnitTests;

[TestFixture]
internal sealed class CustomConsoleThemeTests
{
    [Test]
    public void DarkTheme_and_LightTheme_are_console_themes_with_full_style_map()
    {
        CustomConsoleTheme.DarkTheme.Should().BeAssignableTo<ConsoleTheme>();
        CustomConsoleTheme.LightTheme.Should().BeAssignableTo<ConsoleTheme>();

        // ConsoleThemeStyle.Object is obsolete and shares its underlying value with Scalar, so the theme dictionary has one entry per distinct value.
        var expected = Enum.GetValues<ConsoleThemeStyle>().Cast<int>().Distinct().Count();
        GetStyleCount(CustomConsoleTheme.DarkTheme).Should().Be(expected);
        GetStyleCount(CustomConsoleTheme.LightTheme).Should().Be(expected);
    }

    [Test]
    public void DarkTheme_and_LightTheme_define_distinct_level_information_colors()
    {
        var dark = GetStyle(CustomConsoleTheme.DarkTheme, ConsoleThemeStyle.LevelInformation);
        var light = GetStyle(CustomConsoleTheme.LightTheme, ConsoleThemeStyle.LevelInformation);
        dark.Should().NotBe(light);
    }

    private static int GetStyleCount(ConsoleTheme theme)
    {
        var field = theme.GetType().GetField("_styles", BindingFlags.Instance | BindingFlags.NonPublic);
        field.Should().NotBeNull();
        var dict = field!.GetValue(theme) as IReadOnlyDictionary<ConsoleThemeStyle, string>;
        dict.Should().NotBeNull();
        return dict!.Count;
    }

    private static string GetStyle(ConsoleTheme theme, ConsoleThemeStyle style)
    {
        var field = theme.GetType().GetField("_styles", BindingFlags.Instance | BindingFlags.NonPublic);
        var dict = (IReadOnlyDictionary<ConsoleThemeStyle, string>)field!.GetValue(theme)!;
        return dict[style];
    }
}
