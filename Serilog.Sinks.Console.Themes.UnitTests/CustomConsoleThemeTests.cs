namespace Serilog.Sinks.Console.Themes.UnitTests;

[TestFixture]
internal sealed class CustomConsoleThemeTests
{
    [Test]
    public void Dark_and_Light_are_console_themes_with_full_style_map()
    {
        CustomConsoleTheme.Dark.Should().BeAssignableTo<ConsoleTheme>();
        CustomConsoleTheme.Light.Should().BeAssignableTo<ConsoleTheme>();

        // ConsoleThemeStyle.Object is obsolete and shares its underlying value with Scalar, so the theme dictionary has one entry per distinct value.
        var expected = Enum.GetValues<ConsoleThemeStyle>().Cast<int>().Distinct().Count();
        ThemeTestHelpers.GetStyleCount(CustomConsoleTheme.Dark).Should().Be(expected);
        ThemeTestHelpers.GetStyleCount(CustomConsoleTheme.Light).Should().Be(expected);
    }

    [Test]
    public void Dark_and_Light_define_distinct_level_information_colors()
    {
        var dark = ThemeTestHelpers.GetStyle(CustomConsoleTheme.Dark, ConsoleThemeStyle.LevelInformation);
        var light = ThemeTestHelpers.GetStyle(CustomConsoleTheme.Light, ConsoleThemeStyle.LevelInformation);
        dark.Should().NotBe(light);
    }

    [Test]
    public void CustomConsoleTheme_aliases_reference_same_instances_as_ConsoleThemes()
    {
        ReferenceEquals(CustomConsoleTheme.Dark, ConsoleThemes.Dark).Should().BeTrue();
        ReferenceEquals(CustomConsoleTheme.Light, ConsoleThemes.Light).Should().BeTrue();
    }
}
