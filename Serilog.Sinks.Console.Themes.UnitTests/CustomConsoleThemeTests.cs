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
        ThemeTestHelpers.GetStyleCount(CustomConsoleTheme.DarkTheme).Should().Be(expected);
        ThemeTestHelpers.GetStyleCount(CustomConsoleTheme.LightTheme).Should().Be(expected);
    }

    [Test]
    public void DarkTheme_and_LightTheme_define_distinct_level_information_colors()
    {
        var dark = ThemeTestHelpers.GetStyle(CustomConsoleTheme.DarkTheme, ConsoleThemeStyle.LevelInformation);
        var light = ThemeTestHelpers.GetStyle(CustomConsoleTheme.LightTheme, ConsoleThemeStyle.LevelInformation);
        dark.Should().NotBe(light);
    }

    [Test]
    public void CustomConsoleTheme_aliases_reference_same_instances_as_ConsoleThemes()
    {
        ReferenceEquals(CustomConsoleTheme.DarkTheme, ConsoleThemes.Dark).Should().BeTrue();
        ReferenceEquals(CustomConsoleTheme.LightTheme, ConsoleThemes.Light).Should().BeTrue();
    }
}
